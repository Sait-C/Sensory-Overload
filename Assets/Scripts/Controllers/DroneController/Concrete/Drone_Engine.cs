using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drone { 
    [RequireComponent(typeof(BoxCollider))]
    public class Drone_Engine : MonoBehaviour, IEngine
    {
        [Header("Engine Properties")]
        public float maxPower = 4f;

        [Header("Properller Properties")]
        [SerializeField] Transform propeller;
        [SerializeField] float propRotSpeed = 300f;

        [HideInInspector]
        public float currentPower;

        public bool powerOn;

        void Start(){
            currentPower = maxPower;
        }

        //Implementions
        public void InitEngine(){
            powerOn = true;
        }

        public void UpdateEngine(Rigidbody rb, Drone_Inputs input){
            if(!powerOn)
                return;

            Vector3 upVec = transform.up;
            upVec.x = 0f;
            upVec.z = 0f;
            float diff = 1 - upVec.magnitude;
            float finalDiff = Physics.gravity.magnitude * diff;


            Vector3 engineForce = Vector3.zero;
            /*
            The left side of the + statement allows the engine to apply a G force against the G force. 
            The right side provides an extra force up or down depending on the input.
            */
            engineForce = transform.up * ((rb.mass * Physics.gravity.magnitude +  finalDiff) 
                + (input.Throttle * currentPower)) / 4f;

            rb.AddForce(engineForce, ForceMode.Force);
            HandlePropellers();
        }

        void HandlePropellers(){
            if(!propeller || !powerOn){
                return;
            }

            propeller.Rotate(Vector3.up, propRotSpeed);
        }

        public void SetEnginePower(float power){
            currentPower = power;
        }
    }

}

