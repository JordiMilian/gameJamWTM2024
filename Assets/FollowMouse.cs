using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] Vector3 rawMousePosition;
    [SerializeField] Rigidbody ShipRb;
    //public Vector3 ShipPosition;
    Vector3 directionToMouse;
    public float ShipSpeed;
    public Vector2 minMaxSpeed;
    public float ShipAcceleration;
    float distanceToMouse;
    Vector3 lastValidPosition;
    [SerializeField] float maxCollisionForce;
    float normalizedSpeed;

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
        ShipSpeed = ShipSpeed * (percent / 100);
    }
    void restartShipSpeed()
    {
        ShipSpeed = minMaxSpeed.x;
    }
    private void Update()
    {
        Vector3 mousePositionInPlane = GetDirectionToMove();
        Vector3 vectorToMouse = mousePositionInPlane - transform.position;
        distanceToMouse = vectorToMouse.magnitude;
        
        directionToMouse = (vectorToMouse).normalized;

        ShipSpeed = Mathf.Lerp(ShipSpeed, minMaxSpeed.y, ShipAcceleration);
        normalizedSpeed = Mathf.InverseLerp(minMaxSpeed.x, minMaxSpeed.y, ShipSpeed);

        if (Input.GetKeyDown(KeyCode.Mouse0)) { restartShipSpeed(); } //Restart speed if click

        Debug.DrawLine(transform.position, transform.position + directionToMouse); 
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
        //Vector3 ShipMousePosition = new Vector3(rawMousePosition.x, rawMousePosition.y, transform.position.z);

    }
    private void FixedUpdate()
    {
        ShipRb.AddForce(directionToMouse * ShipSpeed);
    }
    private void OnCollisionEnter(Collision collision) //restart if collision with wall
    {
        if(collision.gameObject.tag == "Wall")
        {
            reduceSpeedPercent(50);
            Vector3 collisionNormal = collision.contacts[0].normal;
            ShipRb.AddForce(collisionNormal * maxCollisionForce * normalizedSpeed);
        }
       
    }
     
}
