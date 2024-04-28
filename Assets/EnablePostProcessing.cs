using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnablePostProcessing : MonoBehaviour
{
    PostProcessVolume postProcessing;

    void Awake()
    {
        postProcessing = GetComponent<PostProcessVolume>();
        postProcessing.enabled = true;
    }
}
