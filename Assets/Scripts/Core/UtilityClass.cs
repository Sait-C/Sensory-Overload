using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityClass
{
    public static Vector3 GetVectorFromAngle(float angle){
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(-Mathf.Sin(angleRad),0f, Mathf.Cos(angleRad));
    }
}
