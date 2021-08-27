using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public class Camera : SwitchableSensor
    {
        [SerializeField] GameObject camera;
        [SerializeField] GameObject hudCanvas;
        [SerializeField] GameObject droneCameraCanvas;

        public override void Open(){
            base.Open();
            //Don't use SetActive
            camera.SetActive(true);
            droneCameraCanvas.SetActive(true);
            hudCanvas.SetActive(false);
        }

        public override void Close(){
            if(isOpen){
                base.Close();
                //Don't use SetActive
                camera.SetActive(false);
                droneCameraCanvas.SetActive(false);
                hudCanvas.SetActive(true);
            }
            
        }
        public void OpenClose(){
            if(isOpen){
                Close();
            }else{
                Open();
            }
        }
    }
}

