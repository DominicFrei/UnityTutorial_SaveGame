using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/PlayerPrefs.html

    #region Events

    [SerializeField] private int hitCount = 0;

    private void OnMouseDown()
    {
        // Increase the hit count by one each time we click the game object.
        hitCount++;
    }

    #endregion

    #region Save / Load

    private readonly string hitCountKey = "HitCountKey";

    private void Awake()
    {
        // Check if the key exists. If not, we never saved the hit count before.
        if (PlayerPrefs.HasKey(hitCountKey))
        {
            // Read the hit count from the PlayerPrefs.
            hitCount = PlayerPrefs.GetInt(hitCountKey);
        }
    }

    private void OnDestroy()
    {
        // Set and save the hit count before destroying this game object.
        PlayerPrefs.SetInt(hitCountKey, hitCount);
        PlayerPrefs.Save();
    }

    #endregion

}
