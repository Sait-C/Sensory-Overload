using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we can just extend or inherit from this one particular script for all of our vehicles
[RequireComponent(typeof(Rigidbody))]
public abstract class Base_Rigidbody : MonoBehaviour
{
    [Header("Rigidbody Properties")]
    [SerializeField] private float weightInLbs = 1f; //weight(mass) in pounds

    const float LBSTOKG = 0.454f; //this is for converting pounds to kg

    protected Rigidbody rb;

    //for dynamic drag
    //for some sort of physics based object or a vehicle you want to update the drag
    //as something is going to faster obvisouly the faster we go and the more drag we're creating 
    protected float startDrag;
    protected float startAngularDrag;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        if(rb){
            rb.mass = weightInLbs * LBSTOKG;
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;
        }
    }

    void FixedUpdate(){
        if(!rb){
            return;
        }

        HandlePhysics();
    }

    protected virtual void HandlePhysics() { }
}

