using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sensories;

namespace Obstacles {
    public class SignalArea : Obstacle
    {
        [Header("Check Physics")]
        [SerializeField] Transform checkPosition;
        [SerializeField] LayerMask whatIsPlayer;
        [SerializeField] float radius = 10f;

        [Header("Player Inside Action")]
        [SerializeField] LevelableSensor[] levelableSensors;
        [SerializeField] SwitchableSensor[] switchableSensors;

        public void Awake(){
            checkPhysics = new SphereCheck(checkPosition, whatIsPlayer, radius);
            OnPlayerInside += BreakSensors;
            OnPlayerExit += BackupSensors;
        }

        public void BreakSensors(){
           foreach(var sensor in levelableSensors){
                sensor.Close();
           }

           foreach(var sensor in switchableSensors){
                sensor.Close();
           }
        }

        public void BackupSensors(){
            Debug.Log("Player exit");
        }

        public void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(checkPosition.position, radius);
            //checkPhysics.DrawGizmos(Color.red);
        }
    }
}

