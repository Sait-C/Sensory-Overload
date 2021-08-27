using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public class LevelableSensorLogic
    {
        LevelableSensor sensor;

        public LevelableSensorLogic(LevelableSensor sensor){
            this.sensor = sensor;
        }

        public delegate void LevelAction();
        public event LevelAction OnLevelIsZero;
        public event LevelAction OnLevelIsNotZero;
        
        public void CheckIfLevelZero(){
            if(sensor.level <= 0){
                OnLevelIsZero();
            }else{
                OnLevelIsNotZero();
            }
        }
    }
}


