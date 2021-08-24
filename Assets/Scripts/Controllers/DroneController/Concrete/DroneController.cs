using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Drone {
    [RequireComponent(typeof(Drone_Inputs))] //so it will be easy to create a drone
    public class DroneController : Base_Rigidbody, IController
    {
        [Header("Control Properties")]
        [SerializeField] float minMaxPitch = 30f;
        [SerializeField] float minMaxRoll = 30f;
        [SerializeField] float yawPower = 4f;
        [SerializeField] float lerpSpeed = 2f;

        private Drone_Inputs input;
        private List<IEngine> engines = new List<IEngine>();

        private float finalPitch;
        private float finalRoll;
        private float finalYaw;

        private float yaw;

        void Start(){
            input = GetComponent<Drone_Inputs>(); //we can properties(Cyclic, Pedals, Throttle)
            engines = GetComponentsInChildren<IEngine>().ToList<IEngine>();
        }

        protected override void HandlePhysics(){
            HandleEngines();
            HandleControls();
        }

        protected virtual void HandleEngines(){
            //rb.AddForce(Vector3.up * (rb.mass * Physics.gravity.magnitude));
            foreach(IEngine engine in engines){
                engine.UpdateEngine(rb, input);
            }
        }

        protected virtual void HandleControls(){
            float pitch = input.Cyclic.y * minMaxPitch;//-30-30
            float roll = -input.Cyclic.x * minMaxRoll;//-30-30
            yaw += input.Pedals * yawPower; 

            finalPitch = Mathf.Lerp(finalPitch, pitch, Time.deltaTime * lerpSpeed);
            finalRoll = Mathf.Lerp(finalRoll, roll, Time.deltaTime * lerpSpeed);
            finalYaw = Mathf.Lerp(finalYaw, yaw, Time.deltaTime * lerpSpeed);

            Quaternion rot = Quaternion.Euler(finalPitch, finalYaw, finalRoll);
            rb.MoveRotation(rot);
        }
    }
}

