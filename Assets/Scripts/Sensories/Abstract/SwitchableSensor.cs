using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public abstract class SwitchableSensor : Sensor
    {
        public virtual void Start(){
            Close();
        }

        public virtual void Open(){
            isOpen = true;
        }

        public virtual void Close(){
            isOpen = false;
        }

        protected virtual void Update(){
            ConsumeThePower(PowerConsumptionAmount);
        }
    }
}
