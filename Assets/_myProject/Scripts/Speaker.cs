using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    // Awake =====================================================================================================================================================
    private void Awake()
    {
        int speaker = FindObjectsOfType<Speaker>().Length;
        if (speaker > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
