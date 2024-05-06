using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] Rigidbody ShipRb;
    //public Vector3 ShipPosition;
    Vector3 directionToPoint;
    float overtimeSpeed;
    public Vector2 minMaxOvertimeSpeed;
    public Vector2 minMaxDistanceForBrakes;
    public Vector2 minMaxDistanceForAcceleration;
    
    [SerializeField] float SecondsForMaxSpeed;
    public float ShipConstantAcceleration;
    float distanceToPoint;
    Vector3 lastValidPosition;
    
    [SerializeField] float maxCollisionForce;
    float normalizedOvertimeSpeed;
    [SerializeField] float maxRotationSpeed;
    [HideInInspector] public Vector3 mousePositionInPlane;
    [SerializeField] LayerMask NaveLayerMask;
    public float maxSpeedReached;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] GameObject followMouseObject;
    float elapsedTime;
    [SerializeField] SpriteRenderer placeholderMouseSprite;
    [SerializeField] Color MinAccelerationColor, MaxAccelerationColor;
    
    public float normalizedTotalSpeed;
   

    [Header("Read only")]
    [SerializeField] float currentTotalSpeed;
    [SerializeField] float normalizedAcceleration;
    [SerializeField] float normalizedDistanceForBrake;
    [SerializeField] float normalizedDistanceForAcceleration;

    bool gameStared = false;
    private void Awake()
    {
        gameStared = false;
    }
    private void Start()
    {
        Cursor.visible = false;
        restartShipSpeed();
        elapsedTime = 0;
    }
    void reduceSpeedPercent(float percent)
    {
        //overtimeSpeed = overtimeSpeed * (1- (percent / 100));
        elapsedTime = elapsedTime * (1 - (percent / 100));

    }
    void restartShipSpeed()
    {
        overtimeSpeed = minMaxOvertimeSpeed.x;
    }
    Vector3 GetRaycastPoint()
    {
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

    private void Update()
    {
        if (gameStared) return;
        if(Input.GetMouseButtonDown(0))
        {
            gameStared = true;
        }
    }
    private void FixedUpdate()
    {
        if (!gameStared) return;
        ShipRb.AddForce(directionToPoint * currentTotalSpeed);
    }
    private void LateUpdate()
    {
        //Informacion basica para uso general 
        mousePositionInPlane = GetRaycastPoint();
        Vector3 vectorToPoint = mousePositionInPlane - transform.position;
        distanceToPoint = vectorToPoint.magnitude;
        directionToPoint = (vectorToPoint).normalized;

        //Follow mouse visual va ligeramente por encima del mousePosition
        Vector3 posicionMouseVisual = new Vector3(mousePositionInPlane.x, followMouseObject.transform.position.y, mousePositionInPlane.z);
        followMouseObject.transform.position = posicionMouseVisual;



        normalizedDistanceForBrake = Mathf.InverseLerp(minMaxDistanceForBrakes.x, minMaxDistanceForBrakes.y, distanceToPoint);
        normalizedDistanceForAcceleration = Mathf.InverseLerp(minMaxDistanceForAcceleration.x, minMaxDistanceForAcceleration.y, distanceToPoint);

        if (!gameStared) return;
        placeholderMouseSprite.color = Color.Lerp(MinAccelerationColor, MaxAccelerationColor, normalizedDistanceForAcceleration);

        elapsedTime += Time.deltaTime * normalizedDistanceForAcceleration;
        normalizedOvertimeSpeed = Mathf.Lerp(0, 1, elapsedTime / SecondsForMaxSpeed);

        overtimeSpeed = Mathf.Lerp(minMaxOvertimeSpeed.x, minMaxOvertimeSpeed.y, normalizedOvertimeSpeed); //Go to the corroutine where its calculated
         
        currentTotalSpeed = overtimeSpeed * normalizedDistanceForBrake;
        speedText.text = currentTotalSpeed.ToString();
        if(currentTotalSpeed > maxSpeedReached) { maxSpeedReached = currentTotalSpeed; } // get the max speed reached for the ranking

        Vector3 newTargetPos = (vectorToPoint * currentTotalSpeed * Time.deltaTime); // no recuerdo para que era esto

        normalizedTotalSpeed = Mathf.InverseLerp(minMaxOvertimeSpeed.x, minMaxOvertimeSpeed.y, currentTotalSpeed); //Esto es pal reloj de velocidad

        //Rotate towards mouse
        transform.forward = (Vector3.RotateTowards(transform.forward, directionToPoint, maxRotationSpeed * Time.deltaTime * normalizedOvertimeSpeed, 10f)); 

        //ShipRb.position = Vector3.Lerp(transform.position, transform.position + newTargetPos, 0.3f);
        //ShipRb.velocity = directionToPoint * currentTotalSpeed;
    }
    private void OnCollisionEnter(Collision collision) //reduce speed if collision with wall and bounce out with normal
    {
        if(collision.gameObject.tag == "Wall")
        {
            reduceSpeedPercent(3);
            Vector3 collisionNormal = collision.contacts[0].normal;
            ShipRb.AddForce(collisionNormal * maxCollisionForce * normalizedOvertimeSpeed);

            GameEvents.Instance.OnHitWall?.Invoke();
        }
        Debug.Log("hit: " + collision.gameObject.name);
    }
    IEnumerator overtimeAcceleration(float maxTime)
    {
        float elapsedTimer = 0;
        normalizedOvertimeSpeed = 0;
        while (elapsedTimer < maxTime)
        {
            elapsedTimer += Time.deltaTime * normalizedDistanceForAcceleration;
            normalizedOvertimeSpeed = Mathf.Lerp(0, 1, elapsedTimer / maxTime);
            yield return null;
        }
    }
}
