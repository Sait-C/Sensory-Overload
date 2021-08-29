using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles {
    public class BoxCheck : ICheckPhysics
    {
        private Transform centerOfTheBox;
        private Quaternion rotationOfTheBox;
        private Vector3 halfExtents;
        private LayerMask whatIsPlayer;

        public BoxCheck(Transform centerOfTheBox, Quaternion rotationOfTheBox, 
            Vector3 halfExtents, LayerMask whatIsPlayer){
            this.centerOfTheBox = centerOfTheBox;
            this.rotationOfTheBox = rotationOfTheBox;
            this.halfExtents = halfExtents;
            this.whatIsPlayer = whatIsPlayer;
        }

        public Collider[] Check(){
            Collider[] colliders = Physics.OverlapBox(centerOfTheBox.position, 
                halfExtents, rotationOfTheBox, whatIsPlayer);
            return colliders;
        }
    }
}

