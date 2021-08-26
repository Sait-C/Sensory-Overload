using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public class SensorMachine : MonoBehaviour
    {
        public List<Sensor> sensors = new List<Sensor>();

        void Start()
        {
            foreach(var sensor in sensors){
                sensor.Start();
            }
        }

        void Update()
        {
            foreach(var sensor in sensors){
                sensor.Update();
            }
        }
    }
}

