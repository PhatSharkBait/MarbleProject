using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCameraBased : MonoBehaviour
{

    public Transform camPivot;
    float heading = 0;
    public Transform cam;
    
    public float maxSpeed;
    public float speed;
    public float acceleration;
    public float deceleration;

    Vector2 input;

    // Update is called once per frame
    void Update()
    {
       heading += Input.GetAxis("Mouse X")*Time.deltaTime*180;
       camPivot.rotation = Quaternion.Euler(0,heading,0);

       input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
       input = Vector2.ClampMagnitude(input, 1);

       Vector3 camF = cam.forward;
       Vector3 camR = cam.right;

       camF.y = 0;
       camR.y = 0;
       camF = camF.normalized;
       camR = camR.normalized;

       accelerate(); 
       transform.position += (camF*input.y + camR*input.x)*Time.deltaTime*speed; 
    }

    public void accelerate()
    {
        if((Input.GetAxis("Horizontal")>0) || (Input.GetAxis("Vertical")>0))
            if(speed < maxSpeed)
                speed = speed + acceleration;
        else
        {
            if(speed > deceleration)
                speed = speed - deceleration;
            else if(speed < -deceleration)
                speed = speed + deceleration;
            else
                speed = 0;
        }
    }
}
