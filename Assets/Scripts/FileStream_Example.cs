using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FileStream_Example : MonoBehaviour
{
    // Resources:
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.binarywriter?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-5.0

    #region Hit Count

    [SerializeField] private int hitCountFileWriteAllText = 0;
    [SerializeField] private int hitCountFileWriteAllLines = 0;
    [SerializeField] private int hitCountFileStream = 0;
    [SerializeField] private int hitCountFileStreamWriter = 0;
    [SerializeField] private int hitCountFileStreamWriterFileStream = 0;

    private void OnMouseDown()
    {
        hitCountFileWriteAllText++;
        hitCountFileWriteAllLines++;
        hitCountFileStream++;
        hitCountFileStreamWriter++;
        hitCountFileStreamWriterFileStream++;
    }

    #endregion

    #region Save / Load

    private readonly string fileWriteAllText = "fileWriteAllText.txt";
    private readonly string fileWriteAllLines = "fileWriteAllLines.txt";
    private readonly string fileFileStream = "fileFileStream.txt";
    private readonly string fileStreamWriter = "fileStreamWriter.txt";
    private readonly string fileStreamWriterFileStream = "fileStraemWriter2.txt";

    private void Awake()
    {
        string textFileWriteAllText = File.ReadAllText(fileWriteAllText);
        hitCountFileWriteAllText = Int32.Parse(textFileWriteAllText);

        string textFileWriteAllLines = File.ReadAllText(fileWriteAllLines);
        hitCountFileWriteAllLines = Int32.Parse(textFileWriteAllLines);

        using FileStream fileStream = File.OpenRead(fileFileStream);
        byte[] byteArray = new byte[1024];
        UTF8Encoding utf8Encoding = new(true);
        while (fileStream.Read(byteArray, 0, byteArray.Length) > 0)
        {
            hitCountFileStream = Int32.Parse(utf8Encoding.GetString(byteArray));
        }

        using StreamReader streamReader = new(fileStreamWriter);
        string textStreamReader = streamReader.ReadLine();
        hitCountFileStreamWriter = Int32.Parse(textStreamReader);

        using StreamReader streamReader2 = new(fileStreamWriterFileStream);
        string textStreamReader2 = streamReader2.ReadLine();
        hitCountFileStreamWriterFileStream = Int32.Parse(textStreamReader2);
    }

    private void OnDestroy()
    {
        // Easiest way: using `File` directly.
        // Overwrites the file. Opens and closes directly.
        File.WriteAllText(fileWriteAllText, hitCountFileWriteAllText.ToString());

        string[] stringArray = { hitCountFileWriteAllLines.ToString() };
        // Overwrites the file. Overwrites the file. Opens and closes directly.
        // Writes one line per array element.
        File.WriteAllLines(fileWriteAllLines, stringArray);

        // FileStream
        // Can write multiple times. Streams are kept open until closed.
        // Writes only bytes.
        using FileStream fileStream = File.Create(fileFileStream);
        byte[] byteArray = new UTF8Encoding(true).GetBytes(hitCountFileStream.ToString());
        fileStream.Write(byteArray, 0, byteArray.Length);

        // StreamWriter
        // Can write multiple times. Streams are kept open until closed.
        using StreamWriter streamWriter = new(fileStreamWriter);
        streamWriter.Write(hitCountFileStreamWriter.ToString());

        // StreamWriter with a FileStream
        // Can write multiple times. Streams are kept open until closed.
        // Offers more configuration possibilities for the StreamWriter.
        using StreamWriter straemWriter2 = new(new FileStream(fileStreamWriterFileStream, FileMode.Create));
        straemWriter2.Write(hitCountFileStreamWriterFileStream.ToString());

        // BinaryWriter

    }

    #endregion

}
