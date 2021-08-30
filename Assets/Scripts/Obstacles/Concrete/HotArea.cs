using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sensories;

namespace Obstacles {
    public class HotArea : Obstacle
    {
        [Header("Check Physics")]
        [SerializeField] Transform checkPosition;
        [SerializeField] LayerMask whatIsPlayer;
        [SerializeField] float radius = 10f;

        [Header("Player Action")]
        [SerializeField] GameEvent OnSensoryOverload;
        [SerializeField] float timeToDestroy = 3f;
        [SerializeField] Temperature temperatureSensor;


        private float time;
        private bool warningSpawned = false;
        private GameObject warning;
        void Awake(){
            warningSpawned = false;
            time = timeToDestroy;
            checkPhysics = new SphereCheck(checkPosition, whatIsPlayer, radius);
            OnPlayerInside += StartTimer;
            OnPlayerExit += Stop;
        }

        void StartTimer(){
            if(temperatureSensor.isOpen){
                time = timeToDestroy;
                Stop();
                return;
            }else if(timeToDestroy <= 0f){
                OnSensoryOverload.Raise();
            }else {
                time -= Time.deltaTime;
            }

            if(!warningSpawned){
                warning = HUDManager.Instance.CreateWarning(HUDManager.Instance.highTemperatureSprite);
                warningSpawned = true;
            }
        }

        void Stop(){
            if(warning != null)
                Destroy(warning);
            warningSpawned = false;
        }

        public void OnDrawGizmos(){
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(checkPosition.position, radius);
            //checkPhysics.DrawGizmos(Color.red);
        }
    }

}
