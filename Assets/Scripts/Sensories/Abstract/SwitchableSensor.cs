using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public abstract class SwitchableSensor : Sensor
    {
        protected virtual void Open(){
            isOpen = true;
        }

        protected virtual void Close(){
            isOpen = false;
        }
    }
}
