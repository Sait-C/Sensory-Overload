using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public abstract class LevelableSensor : Sensor
    {
        public float level { get; set; }
        protected LevelableSensorLogic logic;
        protected IPowerConsume powerConsume;

        protected virtual void SetLevel(float value){
            level = value;
        }

        public virtual void Start(){
            logic = new LevelableSensorLogic(this);
            logic.OnLevelIsZero += Close;
            logic.OnLevelIsNotZero += Open;
        }

        public virtual void Update(){
            logic.CheckIfLevelZero();
            float amount = powerConsume.ConsumptionThePower(PowerConsumptionAmount, level);
            ConsumeThePower(amount);
        }

        protected virtual void Open(){
            isOpen = true;
        }

        protected virtual void Close(){
            isOpen = false;
        }

        protected override void ConsumeThePower(float amount){
            base.ConsumeThePower(amount);
            if(isOpen && amountProcess > amount){
                processorData.RuntimeValue -= speedOfPowerConsumption;
                amountProcess -= speedOfPowerConsumption;
            }
        }
    }
}

