using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sensories {
    public class Light : LevelableSensor
    {
        public Slider slider;
        public UnityEngine.Light light;

        public void OnSliderValueChange(){
            SetLevel(slider.value);
            light.intensity = level;
        }
    }
}

