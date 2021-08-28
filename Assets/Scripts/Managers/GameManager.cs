using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image sensoryWarningImage;
    public ProcessorData cpuHeat;


    void Update(){
        if(cpuHeat.RuntimeValue >= cpuHeat.MaxValue){
            sensoryWarningImage.enabled = true;
        }else{
            sensoryWarningImage.enabled = false;
        }
    }
}
