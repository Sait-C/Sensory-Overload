using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Drone;

namespace Sensories {
    public class Engine : LevelableSensor
    {
        public Slider slider;
        public List<Drone_Engine> engines = new List<Drone_Engine>();

        public override void Start(){
            base.Start();
            powerConsume = new GradualConsumption();
        }

        public void OnSliderValueChange(){
            SetLevel(slider.value);
            SetEnginePower();
        }

        public void SetEnginePower(){
            foreach(var engine in engines){
                float power = engine.maxPower * level;
                engine.SetEnginePower(power);
            }
        }

        public override void Close(){
            base.Close();
            foreach(var engine in engines){
                engine.powerOn = false;
            }
        }

        public override void Open(){
            base.Open();
            foreach(var engine in engines){
                engine.InitEngine();
            }
        }
    }
}
