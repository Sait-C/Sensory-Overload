using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles {
   public class Radiation : Obstacle
    {
        [Header("Check Physics")]
        [SerializeField] Transform checkPosition;
        [SerializeField] LayerMask whatIsPlayer;
        [SerializeField] float radius = 10f;

        [Header("Cpu Heat Action")]
        [SerializeField] ProcessorData cpuHeat;
        [SerializeField] float amount = 0.1f;

        private float process = 0f;

        void Awake(){
            checkPhysics = new SphereCheck(checkPosition, whatIsPlayer, radius);
            OnPlayerInside += IncreaseCpuHeat;
            OnPlayerExit += DecreaseCpuHeat;
            process = 0f;
        }

        public void IncreaseCpuHeat(){
            cpuHeat.RuntimeValue += amount;
            process += amount;
        }

        public void DecreaseCpuHeat(){
            if(process > 0){
                cpuHeat.RuntimeValue -= amount * 2;
                process -= amount;
            }
        }

        public void OnDrawGizmos(){
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(checkPosition.position, radius);
            //checkPhysics.DrawGizmos(Color.red);
        }
    } 
}

