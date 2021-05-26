using UnityEngine;
using System.IO;

public class BinaryReaderWriterExample : MonoBehaviour
{
    // Resources:
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.binarywriter?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.stream?view=net-5.0

    #region Hit Count

    [SerializeField] private int hitCount = 0;

    private void OnMouseDown()
    {
        hitCount++;
    }

    #endregion

    #region Save / Load

    private readonly string fileName = "BinaryReaderWriterExample";

    private void Awake()
    {
        // Check if the file exists to avoid errors when opening a non-existing file.
        if (File.Exists(fileName))
        {
            // Open a stream to the file that the `BinaryReader` can use to read data.
            // They need to be disposed at the end, so `using` is good practice
            // because it does this automatically.
            using FileStream fileStream = File.Open(fileName, FileMode.Open);
            using BinaryReader binaryReader = new(fileStream);
            hitCount = binaryReader.ReadInt32(); // Exception if type is not correct.
        }
    }

    private void OnDestroy()
    {
        // Open a stream to the file that the `BinaryReader` can use to read data.
        // They need to be disposed at the end, so `using` is good practice
        // because it does this automatically.
        using (FileStream fileStream = File.Open(fileName, FileMode.Create))
        using (BinaryWriter binaryWriter = new(fileStream))
        {
            binaryWriter.Write(hitCount);
        }
    }

    #endregion

    #region Other examples

    private readonly string fileName2 = "OtherFileName.dat";

    private void Write()
    {
        // Starting with C# 8.0 you can omit the brackets around the `using` keyword.
        using FileStream fileStream = File.Open(fileName2, FileMode.Create);
        using BinaryWriter binaryWriter = new(fileStream);

        binaryWriter.Write(42);
        binaryWriter.Write(42f);
        binaryWriter.Write("42");
    }

    private void Read()
    {
        if (File.Exists(fileName2))
        {
            FileStream fileStream = File.Open(fileName2, FileMode.Open);
            BinaryReader binaryReader = new(fileStream);

            int foo = binaryReader.ReadInt32();
            float bar = binaryReader.ReadSingle();
            string baz = binaryReader.ReadString();

            int nextChar = binaryReader.PeekChar();
            int readResult = binaryReader.Read();

            // The alternative to `using` is to call `Dispose()` when done.
            binaryReader.Dispose();
            fileStream.Dispose();
        }
    }

    #endregion

}
