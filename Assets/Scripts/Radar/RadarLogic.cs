using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Radar {
    public class RadarLogic
    {
        public delegate void HitAction(RaycastHit hit);
        public event HitAction OnRaycastHit;

        private DroneRadar radar;

        public RadarLogic(DroneRadar radar){
            this.radar = radar;
        }

        public void Hit(RaycastHit hit){
            if(hit.collider != null){
                if(!radar.colliderList.Contains(hit.collider)){
                    OnRaycastHit(hit);
                }  
            }
        }
    }
}