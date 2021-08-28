using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Radar {
    public class RadarCamera : MonoBehaviour
    {
        public Transform player;

        private void Update(){
            FollowPlayer();
        }

        public void FollowPlayer(){
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
            //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }
}