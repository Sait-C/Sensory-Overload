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

        [Header("Random Movement")]
        [SerializeField] float stopDistance = 1f;
        [SerializeField] Transform minimum, maximum;
        [SerializeField] float movementSpeed = 5f;

        private bool force = true;

        private Vector3 randomPosition;
        private bool targetPosReached = false;
        private float distanceFromTargetPos = 0f;

        private bool warningSpawned;
        private GameObject warning;

        void Awake(){
            force = true;
            checkPhysics = new BoxCheck(centerOfTheBox, rotationOfTheBox, halfExtents, whatIsPlayer);
            OnPlayerInside += UpsetBalance;
            OnPlayerExit += DeleteWarning;
            //randomPosition = Random.insideUnitSphere * areaRadius;
            randomPosition = GetNewRandomPosition();
        }

        public override void Update(){
            base.Update();
            RandomMovement();
        }

        public void DeleteWarning(){
            if(warning != null)
                Destroy(warning);
            warningSpawned = false;
        }

        private void RandomMovement(){
            distanceFromTargetPos = Vector3.Distance(transform.position, randomPosition);
            if(targetPosReached){
                targetPosReached = false;
                //randomPosition = Random.insideUnitSphere * areaRadius;
                randomPosition = GetNewRandomPosition();
            }
            if(distanceFromTargetPos <= stopDistance){
                targetPosReached = true;
            }else{
                transform.position = Vector3.MoveTowards(transform.position, randomPosition,
                    Time.deltaTime * movementSpeed);
            }
        }

        Vector3 GetNewRandomPosition(){
            float x = Random.Range(minimum.position.x, maximum.position.x);
            float z = Random.Range(minimum.position.z, maximum.position.z);

            return new Vector3(x, transform.position.y, z);
        }

        void UpsetBalance(){
            if(!force){
                DeleteWarning();
                return;
            }else if(force && !warningSpawned){
                warning = HUDManager.Instance.CreateWarning(HUDManager.Instance.controlLostSprite);
                warningSpawned = true;
            }
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
