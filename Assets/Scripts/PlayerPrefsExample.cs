using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/PlayerPrefs.html

    [SerializeField] private int hitCount = 0;

    private readonly string hitCountKey = "HitCountKey";

    private void Awake()
    {
        // Check if the key exists. If not, we never saved to it.
        if (PlayerPrefs.HasKey(hitCountKey))
        {
            hitCount = PlayerPrefs.GetInt(hitCountKey);
        }

        int foo = PlayerPrefs.GetInt("int_key2");
        Debug.Log(foo);

        float bar = PlayerPrefs.GetFloat("float_key2");
        Debug.Log(bar);

        string doo = PlayerPrefs.GetString("string_key2");
        Debug.Log(doo);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(hitCountKey, hitCount);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("string_key2", 55);
        PlayerPrefs.SetFloat("string_key2", 55);
        PlayerPrefs.SetString("string_key2", "55");
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        
    }

    private void OnMouseDown()
    {
        hitCount++;
    }

}
