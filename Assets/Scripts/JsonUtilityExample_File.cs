using System.Collections;
using UnityEngine;
using System.IO;

public class JsonUtilityExample_File : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/JsonUtility.html
    // https://docs.unity3d.com/Manual/script-Serialization.html
    // https://docs.unity3d.com/ScriptReference/Serializable.html

    [SerializeField] private int hitCount = 0;

    private readonly string fileName = "JsonUtilityExample_File.txt";

    private void Awake()
    {
        // Check if the file exists to avoid errors when opening a non-existing file.
        if (File.Exists(fileName))
        {
            string jsonString = File.ReadAllText(fileName);
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
        File.WriteAllText(fileName, jsonString);
    }

    private void OnMouseDown()
    {
        hitCount++;
    }
}
