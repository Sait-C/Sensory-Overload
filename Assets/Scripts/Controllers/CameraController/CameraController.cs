using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0, 3.4f, 0);
        public float lookSmooth = 100f;
        public float distanceFromTarget = -8;
        public float zoomSmooth = 100;
        public float maxZoom = -2;
        public float minZoom = -15;
        public bool smoothFollow = true;
        public float smooth = 0.05f;

        [HideInInspector]
        public float newDistance = -8; //set by zoom input
        [HideInInspector]
        public float adjustmentDistance = -8;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = -20;
        public float yRotation = -180;
        public float maxXRotation = 25;
        public float minXRotation = -85;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = 150;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
        public string ORBIT_HORIZONTAL = "OrbitHorizontal";
        public string ORBIT_VERTICAL = "OrbitVertical";
        public string ZOOM = "Mouse ScrollWheel";
    }

    [System.Serializable]
    public class DebugSettings
    {
        public bool drawDesiredCollisionLines = true;
        public bool drawAdjustedCollisionLines = true;
    }

    public PositionSettings positionSettings = new PositionSettings();
    public OrbitSettings orbitSettings = new OrbitSettings();
    public InputSettings inputSettings = new InputSettings();
    public DebugSettings debugSettings = new DebugSettings();
    public CollisionHandler collisionHandler = new CollisionHandler();

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    Vector3 adjustedDestination = Vector3.zero; //which is going to be replacing our destination if we're colliding
    Vector3 camVel = Vector3.zero;
    IController movementController;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;
    private void Start()
    {
        SetCameraTarget(target);

        vOrbitInput = hOrbitInput = zoomInput = hOrbitSnapInput = 0f;

        MoveToTarget();

        collisionHandler.Initialize(Camera.main);
        collisionHandler.UpdateCameraClipPoints(transform.position, transform.rotation, ref collisionHandler.adjustedCameraClipPoints);
        collisionHandler.UpdateCameraClipPoints(destination, transform.rotation, ref collisionHandler.desiredCameraClipPoints);
    }

    //I think that for a lot of games you want to have the ability to give the camera a new target to look at there's a lot of different uses for this 
    public void SetCameraTarget(Transform t)
    {
        target = t;
        if (target != null)
        {
            if (target.GetComponent<IController>() != null)
            {
                movementController = target.GetComponent<IController>();
            }
            else
            {
                Debug.LogError("The camera's  target needs a character controller.");
            }
        }
        else
        {
            Debug.LogError("Your camera a need a target.");
        }
    }

    void GetInput()
    {
        vOrbitInput = Input.GetAxisRaw(inputSettings.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(inputSettings.ORBIT_HORIZONTAL);
        hOrbitSnapInput = Input.GetAxisRaw(inputSettings.ORBIT_HORIZONTAL_SNAP);
        zoomInput = Input.GetAxisRaw(inputSettings.ZOOM);
    }

    private void Update()
    {
        GetInput();
        OrbitTarget();
        ZoomInOnTarget();
    }

    private void FixedUpdate()
    {
        //moving 
        MoveToTarget();
        //rotating
        LookAtTarget();

        collisionHandler.UpdateCameraClipPoints(transform.position, transform.rotation, ref collisionHandler.adjustedCameraClipPoints);
        collisionHandler.UpdateCameraClipPoints(destination, transform.rotation, ref collisionHandler.desiredCameraClipPoints);

        //draw debug lines
        for (int i = 0; i < 5; i++)
        {
            if (debugSettings.drawDesiredCollisionLines)
            {
                Debug.DrawLine(targetPos, collisionHandler.desiredCameraClipPoints[i], Color.white);
            }
            if (debugSettings.drawAdjustedCollisionLines)
            {
                Debug.DrawLine(targetPos, collisionHandler.adjustedCameraClipPoints[i], Color.green);
            }
        }
        //Check if we colliding
        collisionHandler.CheckColliding(targetPos); //using raycasts here
        //if we ever start colliding so that we know the remember the adjustmentDistance is used to determine how far away from the camera we need to move or from the target that we need t move our camera 
        positionSettings.adjustmentDistance = collisionHandler.GetAdjustedDistanceWithRayFrom(targetPos);
    }

    void MoveToTarget()
    {
        targetPos = target.position + positionSettings.targetPosOffset; //offset is currently on the y axis
        destination = Quaternion.Euler(orbitSettings.xRotation, orbitSettings.yRotation + target.eulerAngles.y,
            0f) * -Vector3.forward * positionSettings.distanceFromTarget;
        destination += targetPos;

        //we are going to be moving our camera differently based on whether or not we are colliding and so we do that in that check of code right there all
        if (collisionHandler.colliding)
        {
            adjustedDestination = Quaternion.Euler(orbitSettings.xRotation, orbitSettings.yRotation + target.eulerAngles.y,
            0f) * Vector3.forward * positionSettings.adjustmentDistance;
            adjustedDestination += targetPos;
            if (positionSettings.smoothFollow)
            {
                //use smooth damp function
                transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination, ref camVel, positionSettings.smooth);
            }
            else
                transform.position = adjustedDestination;
        }
        else
        {
            if (positionSettings.smoothFollow)
            {
                //use smooth damp function
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, positionSettings.smooth);
            }
            else
                transform.position = destination;
        }
    }

    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position); //we substract our transform.position by the Vector3 point that we want to look at
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 
            positionSettings.lookSmooth * Time.deltaTime);
    }

    void OrbitTarget()
    {
        if(hOrbitSnapInput > 0)
        {
            orbitSettings.yRotation = -180; //camera behind our target
        }
        //what direction and what speed
        orbitSettings.xRotation += -vOrbitInput * orbitSettings.vOrbitSmooth * Time.deltaTime;
        orbitSettings.yRotation += -hOrbitInput * orbitSettings.hOrbitSmooth * Time.deltaTime;

        float clampedXRotation = Mathf.Clamp(orbitSettings.xRotation, 
            orbitSettings.minXRotation, orbitSettings.maxXRotation);
        orbitSettings.xRotation = clampedXRotation;
    }

    void ZoomInOnTarget()
    {
        positionSettings.distanceFromTarget += zoomInput * positionSettings.zoomSmooth * Time.deltaTime;
        float clampedZoom = Mathf.Clamp(positionSettings.distanceFromTarget,
            positionSettings.minZoom, positionSettings.maxZoom);
        positionSettings.distanceFromTarget = clampedZoom;
    }

    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer;

        [HideInInspector]
        public bool colliding = false;
        [HideInInspector]
        public Vector3[] adjustedCameraClipPoints; //this is going to be the clip points that surround the cameras current position so wherever the cameras position is going to be where the adjusted camera clip points are going to be clippoints that surround the cameras expected position if it wasnt colliding with anything  
        [HideInInspector]
        public Vector3[] desiredCameraClipPoints;

        Camera camera;

        public void Initialize(Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5]; //4 near clip point and camera position
            desiredCameraClipPoints = new Vector3[5];
        }
        public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            if (!camera) //there is a nothing that we can do with our clip points if we dont have a camera
                return;
            //we're going to be passing this array called intoArray whatever array we're going to be passing our new clip points to 
            //we want to go ahead and clear that out for new clip points because remember as the camera moves we're going to be getting new clip points 
            intoArray = new Vector3[5];

            float z = camera.nearClipPlane;
            float x = Mathf.Tan(camera.fieldOfView / 3.41f) * z; //you can modify this value(3.41)
            float y = x / camera.aspect;

            //top left
            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; //added and rotated the point relative to camera
            //top right
            intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition;
            //bottom left
            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition;
            //bottom right
            intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition;
            //camera's position
            intoArray[4] = cameraPosition - camera.transform.forward; // - camera.transform.position : the reason because it'll give us a little bit more room behind the camera to collide with 
        }

        bool CollisionDetectedAtClipPoint(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for (int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);  //we need to define a ray to cast our raycast in
                float distance = Vector3.Distance(clipPoints[i], fromPosition);
                if(Physics.Raycast(ray, distance, collisionLayer))
                {
                    return true;
                }
            }

            return false;
        }

        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            //return that the cameras needs to be from the target, 
            //this determines how far we move our camera forward whenever there is a collision
            float distance = -1;

            for (int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                //we want to do is find shortest distance between each one of those collisions and return that at the end of this method
                Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if (distance == -1)
                        distance = hit.distance;
                    else
                    {
                        if (hit.distance < distance)
                            distance = hit.distance;
                    }
                }
            }

            if (distance == -1)
                return 0;
            else
                return distance;
        }

        public void CheckColliding(Vector3 targetPosition)
        {
            //it is just going to update our colliding boolean and what we'll do 
            //from outside of this class is check to see if colliding is true or false
            if(CollisionDetectedAtClipPoint(desiredCameraClipPoints, targetPosition))
            {
                colliding = true;
            }
            else
            {
                colliding = false;
            }
        }
    }
}
