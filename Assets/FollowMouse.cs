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
        rawMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 ShipMousePosition = new Vector3 (rawMousePosition.x,rawMousePosition.y, transform.position.z);

        Vector3 vectorToMouse = ShipMousePosition - transform.position;
        distanceToMouse = vectorToMouse.magnitude;
        
        directionToMouse = (vectorToMouse).normalized;

        ShipSpeed = Mathf.Lerp(ShipSpeed, minMaxSpeed.y, ShipAcceleration);

        if (Input.GetKeyDown(KeyCode.Mouse0)) { restartShipSpeed(); } //Restart speed if click

        Debug.DrawLine(transform.position, transform.position + directionToMouse);

        //ShipPosition = Vector3.Lerp(ShipPosition, mousePosition, Time.deltaTime * ShipSpeed);
        //transform.position = new Vector3(ShipPosition.x,ShipPosition.y,transform.position.y);
 
    }
    private void FixedUpdate()
    {
        ShipRb.AddForce(directionToMouse * ShipSpeed);
    }
    private void OnCollisionEnter(Collision collision) //restart if collision with wall
    {
        reduceSpeedPercent(50);
    }
     
}
