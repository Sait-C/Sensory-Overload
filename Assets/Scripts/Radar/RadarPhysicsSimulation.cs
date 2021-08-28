using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Radar {

    public class RadarPhysicsSimulation
    {
        private DroneRadar radar;
        private Transform sweep;

        public RadarPhysicsSimulation(DroneRadar radar, Transform sweep){
            this.radar = radar;
            this.sweep = sweep;
        }

        public RaycastHit[] Simulate(float radarDistance, LayerMask radarPingLayer){
            Ray ray = new Ray(radar.gameObject.transform.position, UtilityClass.GetVectorFromAngle(sweep.localEulerAngles.z));
            //Debug.DrawLine(transform.position, UtilityClass.GetVectorFromAngle(sweep.localEulerAngles.z), new Color(1, 0, 0));
            //We don't use Phyics.Raycast because if object is behind the other object, it is not effect by raycast(because raycast hit the object and stop)
            //So we can use Physics.RaycastAll in this case
            RaycastHit[] hits = Physics.RaycastAll(ray, radarDistance, radarPingLayer);
            return hits;
        }
    }
}