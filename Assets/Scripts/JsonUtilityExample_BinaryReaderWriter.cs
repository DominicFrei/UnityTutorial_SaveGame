using System.Collections;
using UnityEngine;
using System.IO;

public class JsonUtilityExample_BinaryReaderWriter : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/JsonUtility.html
    // https://docs.unity3d.com/Manual/script-Serialization.html
    // https://docs.unity3d.com/ScriptReference/Serializable.html

    [SerializeField] private int hitCount = 0;

    private readonly string fileName = "JsonUtilityExample_BinaryReaderWriter.bin";

    private void Awake()
    {
        // Check if the file exists to avoid errors when opening a non-existing file.
        if (File.Exists(fileName))
        {
            FileStream fileStream = File.Open(fileName, FileMode.Open);
            string jsonString;
            using (BinaryReader binaryReader = new(fileStream))
            {
                jsonString = binaryReader.ReadString();
            }
            // Always close a FileStream when you're done with it.
            fileStream.Close();
            HitCountWrapper hitCountEntity = JsonUtility.FromJson<HitCountWrapper>(jsonString);
            if (hitCountEntity != null)
            {
                hitCount = hitCountEntity.value;
            }
        }
    }

    private void OnDestroy()
    {
        HitCountWrapper hitCountEntity = new();
        hitCountEntity.value = hitCount;
        string jsonString = JsonUtility.ToJson(hitCountEntity);
        FileStream fileStream = File.Open(fileName, FileMode.Create);
        using (BinaryWriter binaryWriter = new(fileStream))
        {
            binaryWriter.Write(jsonString);
        }
        // Always close a FileStream when you're done with it.
        fileStream.Close();
    }

    private void OnMouseDown()
    {
        hitCount++;
    }
}
