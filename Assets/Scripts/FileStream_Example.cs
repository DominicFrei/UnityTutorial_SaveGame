using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FileStream_Example : MonoBehaviour
{
    #region Hit Count

    [SerializeField] private int hitCount = 0;

    private void OnMouseDown()
    {
        hitCount++;
    }

    #endregion

    #region Save / Load

    private readonly string fileName = "FileStreamExample";

    private void Awake()
    {
        //Open the stream and read it back.
        using (FileStream fs = File.OpenRead(fileName))
        {
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);
            while (fs.Read(b, 0, b.Length) > 0)
            {
                Console.WriteLine(temp.GetString(b));
            }
        }
    }

    private void OnDestroy()
    {
        // For testing purposes we just delete the file and recreate it with new data.
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        // Create a new file.
        using FileStream fileStream = File.Create(fileName);
        //AddText(fileStream, hitCount);
        AddText(fileStream, "This is some text");
        AddText(fileStream, "This is some more text,");
        AddText(fileStream, "\r\nand this is on a new line");
        AddText(fileStream, "\r\n\r\nThe following is a subset of characters:\r\n");

        for (int i = 1; i < 120; i++)
        {
            AddText(fileStream, Convert.ToChar(i).ToString());
        }

        File.WriteAllText(fileName, "50");

        var myInt = 50;
        File.WriteAllText(fileName, myInt.ToString());

        using (var writer = new StreamWriter(fileName))
            writer.Write(myInt.ToString());

        using (var writer = new StreamWriter(new FileStream(fileName, FileMode.CreateNew)))
            writer.Write(myInt.ToString());

        using (var stream = new FileStream(fileName, FileMode.CreateNew))
        {
            var bytes = Encoding.UTF8.GetBytes(myInt.ToString());
            stream.Write(bytes, 0, bytes.Length);
        }
    }

    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }

    #endregion

}
