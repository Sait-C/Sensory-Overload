using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles {
    public abstract class Obstacle : MonoBehaviour
    {
        public delegate void PlayerAction();
        public event PlayerAction OnPlayerInside;
        public event PlayerAction OnPlayerExit;


        public bool update = false;
        public ICheckPhysics checkPhysics;

        private bool playerInside = false;
        protected Collider player;

        public virtual void Start(){
            playerInside = false;
        }

        public virtual void Update(){
            var cols = checkPhysics.Check();
            if(cols.Length > 0){
                foreach(var col in cols){
                    player = col;
                    //Will it work one time or continually?
                    //You can seperate this logics to another script like Obstacle_Logics.cs
                    if(update && OnPlayerInside != null){
                        OnPlayerInside();
                    }else if(!playerInside && OnPlayerInside != null){
                        OnPlayerInside();
                    }
                    
                    playerInside = true;
                }
            }else if(cols.Length <= 0 && playerInside){
                playerInside = false;
                if(OnPlayerExit != null)
                    OnPlayerExit();
            }
        }
    }
}

