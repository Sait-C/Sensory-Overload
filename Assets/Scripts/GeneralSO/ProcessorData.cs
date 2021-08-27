using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class ProcessorData : ScriptableObject, ISerializationCallbackReceiver
{
    public float MaxValue;

    [NonSerialized]
    public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = 0f;
    }

    public void OnBeforeSerialize() { }
}
