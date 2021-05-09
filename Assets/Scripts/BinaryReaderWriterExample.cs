using System.Collections;
using UnityEngine;
using System.IO;

public class BinaryReaderWriterExample : MonoBehaviour
{
    // Resources:
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.binarywriter?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-5.0

    [SerializeField] private int hitCount = 0;

    private readonly string fileName = "BinaryReaderWriterExample";

    private void Awake()
    {
        // Check if the file exists to avoid errors when opening a non-existing file.
        if (File.Exists(fileName))
        {
            FileStream fileStream = File.Open(fileName, FileMode.Open);
            using (BinaryReader binaryReader = new(fileStream))
            {
                hitCount = binaryReader.ReadInt32();
            }
            // Always close a FileStream when you're done with it.
            fileStream.Close();
        }
    }

    private void OnDestroy()
    {
        FileStream fileStream = File.Open(fileName, FileMode.Create);
        using (BinaryWriter binaryWriter = new(fileStream))
        {
            binaryWriter.Write(hitCount);
        }
        // Always close a FileStream when you're done with it.
        fileStream.Close();
    }

    private void OnMouseDown()
    {
        hitCount++;
    }
}
