using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles {
    [System.Serializable]
    public class SphereCheck : ICheckPhysics
    {
        private Transform checkPosition;
        private LayerMask whatIsPlayer;
        private float radius;

        public SphereCheck(Transform checkPosition, LayerMask whatIsPlayer, float radius){
            this.checkPosition = checkPosition;
            this.whatIsPlayer = whatIsPlayer;
            this.radius = radius;
        }

        public Collider[] Check(){
            Collider[] colliders = Physics.OverlapSphere(checkPosition.position, radius, whatIsPlayer);
            return colliders;
        }
    }
}

