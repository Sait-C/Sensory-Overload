using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Drone {   //Defensive programming
    [RequireComponent(typeof(PlayerInput))] //Defensive programming
    public class Drone_Inputs : MonoBehaviour
    {
        private Vector2 cyclic;
        private float pedals;
        private float throttle;

        //Properties
        public Vector2 Cyclic { get => cyclic; }
        public float Pedals { get => pedals; }
        public float Throttle { get => throttle; }

        void Update(){

        }
        

        //Input Methods
        private void OnCyclic(InputValue value){
            cyclic = value.Get<Vector2>();
        }

        private void OnPedals(InputValue value){
            pedals = value.Get<float>();
        }

        private void OnThrottle(InputValue value){
            throttle = value.Get<float>();
        }
    }
}


