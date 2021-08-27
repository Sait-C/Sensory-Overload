using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public class Radar : SwitchableSensor
    {
        public GameObject radarMap;

        public override void Open(){
            base.Open();
            radarMap.SetActive(true);
        }

        public override void Close(){
            base.Close();
            radarMap.SetActive(false);
        }
    }
}

