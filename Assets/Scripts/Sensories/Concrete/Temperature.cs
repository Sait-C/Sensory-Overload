using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories {
    public class Temperature : SwitchableSensor
    {
        public void OpenClose(){
            if(isOpen){
                Close();
            }else{
                Open();
            }
        }
    }
}

