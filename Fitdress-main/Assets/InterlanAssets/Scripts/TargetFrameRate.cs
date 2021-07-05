using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    public int _frameRate = 60;

    void Start () 
    {
		QualitySettings.vSyncCount = 0;
    }

    void Update () 
    {
		if (_frameRate != Application.targetFrameRate)
			Application.targetFrameRate = _frameRate;
    }
}
