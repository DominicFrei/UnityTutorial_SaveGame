using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTemplate : MonoBehaviour
{
    // Resources:
    // http://www......

    [SerializeField] private int hitCount = 0;

    private void Awake()
    {
        // Load hitCount.
    }

    private void OnDestroy()
    {
        // Save hitCount.
    }

    private void OnMouseDown()
    {
        hitCount++;
    }

}
