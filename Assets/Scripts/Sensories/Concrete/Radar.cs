using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public class Radar : SwitchableSensor
    {
        public GameObject radarMap;

        protected override void Open(){
            base.Open();
            radarMap.SetActive(true);
        }

        protected override void Close(){
            base.Close();
            radarMap.SetActive(false);
        }
    }
}

