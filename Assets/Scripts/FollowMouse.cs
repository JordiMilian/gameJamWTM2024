using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] Vector3 rawMousePosition;
    [SerializeField] Rigidbody ShipRb;
    //public Vector3 ShipPosition;
    Vector3 directionToPoint;
    float overtimeSpeed;
    public Vector2 minMaxOvertimeSpeed;
    public Vector2 minMaxDistanceToMouse;
    public float ShipAcceleration;
    float distanceToPoint;
    Vector3 lastValidPosition;
    [SerializeField] float maxCollisionForce;
    float normalizedOvertimeSpeed;
    float normalizedDistanceToMouse;
    [SerializeField] float maxRotationSpeed;
    [HideInInspector] public Vector3 mousePositionInPlane;
    [SerializeField] LayerMask NaveLayerMask;

    [SerializeField] float currentTotalSpeed;
    Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        restartShipSpeed();
    }
    void reduceSpeedPercent(float percent)
    {
        overtimeSpeed = overtimeSpeed * (1- (percent / 100));
    }
    void restartShipSpeed()
    {
        overtimeSpeed = minMaxOvertimeSpeed.x;
    }
   /* private void Update()
    {
        mousePositionInPlane = GetRaycastPoint();
        Vector3 vectorToPoint = mousePositionInPlane - transform.position;

        distanceToPoint = vectorToPoint.magnitude;
        directionToPoint = (vectorToPoint).normalized;

        overtimeSpeed = Mathf.Lerp(overtimeSpeed, minMaxOvertimeSpeed.y, ShipAcceleration);
        //normalizedDistanceToMouse = Mathf.InverseLerp(minMaxDistanceToMouse.x, minMaxDistanceToMouse.y, distanceToPoint);
        normalizedOvertimeSpeed = Mathf.InverseLerp(minMaxOvertimeSpeed.x, minMaxOvertimeSpeed.y, overtimeSpeed);

        transform.forward = (Vector3.RotateTowards(transform.forward, directionToPoint, maxRotationSpeed * Time.deltaTime * normalizedOvertimeSpeed, 10f)); //Rotate towards mouse

        currentTotalSpeed = overtimeSpeed; //* normalizedDistanceToMouse;
        Vector3 newTargetPos = (directionToPoint * currentTotalSpeed * Time.deltaTime);
        ShipRb.position = Vector3.Lerp(transform.position, transform.position + newTargetPos, 0.3f);
       
    }*/
    Vector3 GetRaycastPoint()
    {
        rawMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) //Recuerda meter en la Layer "Ignore_Raycast" a los objetos que no son de suelo
        {
            Debug.Log( "hit: "+ hit.transform.name);
            lastValidPosition = hit.point;
        }
        else
        {
            return lastValidPosition;
        }
        return hit.point;


    }
    private void LateUpdate()
    {

        mousePositionInPlane = GetRaycastPoint();
        Vector3 vectorToPoint = mousePositionInPlane - transform.position;

        distanceToPoint = vectorToPoint.magnitude;
        directionToPoint = (vectorToPoint).normalized;

        overtimeSpeed = Mathf.Lerp(overtimeSpeed, minMaxOvertimeSpeed.y, ShipAcceleration);
        //normalizedDistanceToMouse = Mathf.InverseLerp(minMaxDistanceToMouse.x, minMaxDistanceToMouse.y, distanceToPoint);
        normalizedOvertimeSpeed = Mathf.InverseLerp(minMaxOvertimeSpeed.x, minMaxOvertimeSpeed.y, overtimeSpeed);

        transform.forward = (Vector3.RotateTowards(transform.forward, directionToPoint, maxRotationSpeed * Time.deltaTime * normalizedOvertimeSpeed, 10f)); //Rotate towards mouse

        //currentTotalSpeed = overtimeSpeed; //* normalizedDistanceToMouse;

        currentTotalSpeed = overtimeSpeed; // * normalizedDistanceToMouse;

        Vector3 newTargetPos = (vectorToPoint * currentTotalSpeed * Time.deltaTime);
        //ShipRb.position = Vector3.Lerp(transform.position, transform.position + newTargetPos, 0.3f);

        ShipRb.AddForce(directionToPoint * currentTotalSpeed);
        //ShipRb.velocity = directionToPoint * currentTotalSpeed;


    }
    private void OnCollisionEnter(Collision collision) //reduce speed if collision with wall and bounce out with normal
    {
        if(collision.gameObject.tag == "Wall")
        {
            reduceSpeedPercent(10);
            Vector3 collisionNormal = collision.contacts[0].normal;
            ShipRb.AddForce(collisionNormal * maxCollisionForce * normalizedOvertimeSpeed);
        }
        Debug.Log("hit: " + collision.gameObject.name);
    }
     
}
