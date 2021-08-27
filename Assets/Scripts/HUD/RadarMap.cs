using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarMap : MonoBehaviour
{
    [Header("Sweep")]
    public Transform sweepTransform;
    public float rotationSpeed = 180f;

    [Header("Minimap Camera")]
    public Transform player;
    public Camera minimapCamera;


    private void Update(){
        sweepTransform.eulerAngles -= new Vector3(0, 0, rotationSpeed * Time.deltaTime);

        FollowPlayer();
    }

    public void FollowPlayer(){
        Vector3 newPosition = player.position;
        newPosition.y = minimapCamera.gameObject.transform.position.y;
        minimapCamera.gameObject.transform.position = newPosition;
        minimapCamera.gameObject.transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
