using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CpuHeatIndicator : MonoBehaviour
{
    public Image fillImage;
    public ProcessorData data;

    void Update(){
        fillImage.fillAmount = data.RuntimeValue / data.MaxValue;
    }

}
