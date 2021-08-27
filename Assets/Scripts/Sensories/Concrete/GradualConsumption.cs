using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradualConsumption : IPowerConsume
{
   public float ConsumptionThePower(float powerConsumptionAmount, float level){
        return powerConsumptionAmount * level;
   }
}
