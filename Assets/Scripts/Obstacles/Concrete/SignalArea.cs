using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles {
    public class SignalArea : Obstacle
    {
        [Header("Check Physics")]
        [SerializeField] Transform checkPosition;
        [SerializeField] LayerMask whatIsPlayer;
        [SerializeField] float radius = 10f;

        public void Awake(){
            checkPhysics = new SphereCheck(checkPosition, whatIsPlayer, radius);
            OnPlayerInside += BreakSensors;
            OnPlayerExit += BackupSensors;
        }

        public void BreakSensors(){
            Debug.Log("Sensors is broke");
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

