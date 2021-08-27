using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameEvent OnExitDroneCamera;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            OnExitDroneCamera.Raise();
        }
    }
}
