using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sensories;

public class UI_SwitchableSensor : MonoBehaviour
{
    public Image buttonImg;
    private Color color;
    public Sensor sensor;

    void Start(){
        SetColor();
    }

    public void SensorBtn(){
        SetColor();
    }

    private void SetColor(){
        color = buttonImg.color;
        color.a = sensor.isOpen ? 1f : 0.5f;
        buttonImg.color = color;
    }
}
