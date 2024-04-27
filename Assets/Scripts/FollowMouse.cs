using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] Vector3 rawMousePosition;
    [SerializeField] Rigidbody ShipRb;
    //public Vector3 ShipPosition;
    Vector3 directionToMouse;
    float overtimeSpeed;
    public Vector2 minMaxOvertimeSpeed;
    public Vector2 minMaxDistanceToMouse;
    public float ShipAcceleration;
    float distanceToMouse;
    Vector3 lastValidPosition;
    [SerializeField] float maxCollisionForce;
    float normalizedOvertimeSpeed;
    float normalizedDistanceToMouse;
    [SerializeField] float maxRotationSpeed;
    [HideInInspector] public Vector3 mousePositionInPlane;

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
    private void Update()
    {
        mousePositionInPlane = GetDirectionToMove();
        Vector3 vectorToMouse = mousePositionInPlane - transform.position;

        distanceToMouse = vectorToMouse.magnitude;
        directionToMouse = (vectorToMouse).normalized;

        overtimeSpeed = Mathf.Lerp(overtimeSpeed, minMaxOvertimeSpeed.y, ShipAcceleration);
        normalizedDistanceToMouse = Mathf.InverseLerp(minMaxDistanceToMouse.x, minMaxDistanceToMouse.y, distanceToMouse);
        normalizedOvertimeSpeed = Mathf.InverseLerp(minMaxOvertimeSpeed.x, minMaxOvertimeSpeed.y, overtimeSpeed);

        Debug.DrawLine(transform.position, transform.position + directionToMouse);

        transform.forward = (Vector3.RotateTowards(transform.forward, directionToMouse, maxRotationSpeed * Time.deltaTime * normalizedOvertimeSpeed, 10f)); //Rotate towards mouse
    }
    Vector3 GetDirectionToMove()
    {
        rawMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) //Recuerda meter en la Layer "Ignore_Raycast" a los objetos que no son de suelo
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
    private void FixedUpdate()
    {
        currentTotalSpeed = overtimeSpeed * normalizedDistanceToMouse;
        ShipRb.AddForce(directionToMouse * currentTotalSpeed);
        
    }
    private void OnCollisionEnter(Collision collision) //reduce speed if collision with wall and bounce out with normal
    {
        if(collision.gameObject.tag == "Wall")
        {
            reduceSpeedPercent(10);
            Vector3 collisionNormal = collision.contacts[0].normal;
            ShipRb.AddForce(collisionNormal * maxCollisionForce * normalizedOvertimeSpeed);
        }
    }
     
}
