using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepImage : MonoBehaviour
{
    [SerializeField] Transform sweepTransform; 
    void Update()
    {
        Vector3 rot = sweepTransform.localEulerAngles;
        rot.x = transform.localEulerAngles.x;
        rot.y = transform.localEulerAngles.y;
        transform.eulerAngles = rot;
    }
}
