using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public class Sensor : MonoBehaviour 
    {
       public bool isOpen { get; set; }
       [SerializeField] protected float PowerConsumptionAmount;
       public ProcessorData processorData;
       public float speedOfPowerConsumption;

       protected float amountProcess = 0f;

       protected virtual void ConsumeThePower(float amount){
            if(isOpen && amountProcess < amount){
                processorData.RuntimeValue += speedOfPowerConsumption;
                amountProcess += speedOfPowerConsumption;
            }else if(!isOpen && amountProcess > 0f){
                processorData.RuntimeValue -= speedOfPowerConsumption;
                amountProcess -= speedOfPowerConsumption;
            }
       }
    }
}
