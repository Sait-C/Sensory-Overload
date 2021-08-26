using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public class Sensor
    {
       protected bool isOpen { get; set; }
       protected float PowerConsumptionAmount;
       protected ProcessorData processorData;

       protected void ConsumeThePower(){
            if(isOpen){
                processorData.RuntimeValue -= PowerConsumptionAmount;
            }
       }
    }
}
