using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles {
    public class Tornado : Obstacle
    {
        [Header("Player Check Physics")]
        [SerializeField] Transform centerOfTheBox;
        [SerializeField] Quaternion rotationOfTheBox;
        [SerializeField] Vector3 halfExtents;
        [SerializeField] LayerMask whatIsPlayer;

        [Header("Pull Settings")]
        [SerializeField] Transform pullingCenter;
        [SerializeField] AnimationCurve pullingCenterCurve;
        [SerializeField] AnimationCurve pullForceCurve;
        [SerializeField] float pullForce;

        private bool force = true;

        void Awake(){
            force = true;
            checkPhysics = new BoxCheck(centerOfTheBox, rotationOfTheBox, halfExtents, whatIsPlayer);
            OnPlayerInside += UpsetBalance;
        }

        void UpsetBalance(){
            StartCoroutine(IncreasePull());
        }

        IEnumerator IncreasePull(){
            //pull force controlled by curve
            //pullForce = pullForceCurve.Evaluate(((Time.time * 0.1f) % pullForceCurve.length));

            //get direction from tornado to object
            Vector3 forceDirection = pullingCenter.position - player.gameObject.transform.position;

            if(force){
                //apply force to object towards tornado
                player.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.deltaTime);
            }
            
            pullingCenter.position = new Vector3(pullingCenter.position.x, 
                 pullingCenterCurve.Evaluate(((Time.time * 1f) % pullForceCurve.length)), pullingCenter.position.z);
            yield return 1f;
            StartCoroutine(IncreasePull());
        }

        public void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(centerOfTheBox.position, halfExtents*2);
            //checkPhysics.DrawGizmos(Color.red);
        }

        public void SetForce(bool value){
            force = value;
        }
    }
}
