using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRadar : MonoBehaviour
{
    [Header("Radar Ping")]
    [SerializeField] Transform pfRadarPing;
    [SerializeField] LayerMask radarPingLayer;

    [Header("Sweep")]
    public Transform sweepTransform;
    public float rotationSpeed = 180f;

    private List<Collider> colliderList;
    private float radarDistance;

    private void Awake(){
        radarDistance = 150f;
        colliderList = new List<Collider>();
    }

    private void Update(){
        float previousRotation = (sweepTransform.localEulerAngles.z % 360) - 180;
        sweepTransform.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        float currentRotation = (sweepTransform.localEulerAngles.z % 360) - 180;

        if(previousRotation < 0 && currentRotation >= 0){
            //Half rotation
            colliderList.Clear();
        }      

        Ray ray = new Ray(transform.position, UtilityClass.GetVectorFromAngle(sweepTransform.localEulerAngles.z));
        RaycastHit hit;
        Debug.DrawLine(transform.position, UtilityClass.GetVectorFromAngle(sweepTransform.localEulerAngles.z), new Color(1, 0, 0));
       
        if(Physics.Raycast(ray, out hit, radarDistance, radarPingLayer)){
            if(hit.collider != null){
                //Hit something
                if(!colliderList.Contains(hit.collider)){
                    //Hit this one for the first time
                    colliderList.Add(hit.collider);
                    RadarPing radarPing = Instantiate(pfRadarPing, hit.point, pfRadarPing.transform.rotation).GetComponent<RadarPing>();
                    
                    //LOGICS for creating and coloring the radar ping
                    //radarPing.SetColor(new Color(0, 1, 0));
                }
            }
        }
    }
}
//Put radar background on the map(terrain)
//Put radar sweep on the map according to radar background
//Set sweep's Pivot point to center of the radar background 
//Now add an empty child to the object you want to appear on the radar
//and add the 3D Box Collider component. Make its layer "RadarMap"