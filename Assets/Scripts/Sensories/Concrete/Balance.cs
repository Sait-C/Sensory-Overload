using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensories{
    
    public class Balance : SwitchableSensor
    {
        [SerializeField] GameEvent OnBalanceOpen;
        [SerializeField] GameEvent OnBalanceClose;

        public override void Open(){
            base.Open();
            OnBalanceOpen.Raise();
        }

        public override void Close(){
            base.Close();
            OnBalanceClose.Raise();
        }
    }

}
