using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFPS : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
