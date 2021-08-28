using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Radar {
    public class DroneRadar : MonoBehaviour
    {
        [Header("Radar Ping")]
        [SerializeField] Transform pfRadarPing;
        [SerializeField] LayerMask radarPingLayer;

        [Header("Sweep")]
        public Transform sweepTransform;
        public float rotationSpeed = 180f;

        [Header("Radar")]
        [SerializeField] private float radarDistance = 200f;

        [HideInInspector]
        public List<Collider> colliderList;

        private RadarPhysicsSimulation physicsSimulation;
        private RadarLogic radarLogic;

        private void Awake(){
            radarLogic = new RadarLogic(this);
            physicsSimulation = new RadarPhysicsSimulation(this, sweepTransform);
            radarLogic.OnRaycastHit += OnRayHitSomething;
            colliderList = new List<Collider>();
        }

        private void Update(){
            float previousRotation = (sweepTransform.localEulerAngles.z % 360) - 180;
            sweepTransform.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);
            float currentRotation = (sweepTransform.localEulerAngles.z % 360) - 180;

            if(previousRotation < 0 && currentRotation >= 0){
                //Half rotation
                colliderList.Clear();
            }

            var hits = physicsSimulation.Simulate(radarDistance, radarPingLayer);
            foreach(RaycastHit hit in hits){
                radarLogic.Hit(hit);
            }
        }

        private RadarPing CreateRadarPing(Vector3 pos){
            return Instantiate(pfRadarPing, pos, pfRadarPing.transform.rotation).GetComponent<RadarPing>();
        }

        private void OnRayHitSomething(RaycastHit hit){
            //Hit this one for the first time
            colliderList.Add(hit.collider);
            RadarPing radarPing = CreateRadarPing(hit.point);
        
            //LOGICS for creating and coloring the radar ping
            radarPing.SetColor(new Color(1, 0, 0));

            //radarPing.SetDisappearTimer(360f / rotationSpeed);
        }
    }
    //Put radar background on the map(terrain)
    //Put radar sweep on the map according to radar background
    //Set sweep's Pivot point to center of the radar background 
    //Now add an empty child to the object you want to appear on the radar
    //and add the 3D Box Collider component. Make its layer "RadarMap"
}