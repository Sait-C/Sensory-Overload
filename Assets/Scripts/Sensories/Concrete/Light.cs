using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sensories {
    public class Light : LevelableSensor
    {
        public Slider slider;
        public UnityEngine.Light light;

        public override void Start(){
            base.Start();
            powerConsume = new GradualConsumption();
        }

        public override void Close(){
            base.Close();
            slider.value = 0;
            SetLevel(slider.value);
        }

        public void OnSliderValueChange(){
            SetLevel(slider.value);
            light.intensity = level;
        }
    }
}