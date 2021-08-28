using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelableSensor : MonoBehaviour
{
    public GameObject levelControl;
    private bool isControlOpen = false;

    void Start(){
        isControlOpen = false;
        levelControl.SetActive(false);
    }

    public void SensorBtn(){
        isControlOpen = !isControlOpen;
        levelControl.SetActive(isControlOpen);
    }
}
