using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public abstract class LevelableSensor : Sensor
    {
        protected float AmountOfStep;
        public float level;
        LevelableSensorLogic logic;

        protected virtual void SetLevel(float value){
            level = value;
        }

        public virtual void Start(){
            logic.OnLevelIsZero += Close;
            logic = new LevelableSensorLogic(this);
        }

        public virtual void Update(){
            logic.CheckIfLevelZero();
        }

        protected virtual void Close(){
            isOpen = false;
        }
    }
}

