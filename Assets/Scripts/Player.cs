using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //params
    [SerializeField] float moveSpeed = 16f;
    [SerializeField] float padding = 1f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
      SetUpMoveBoundaries();   
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /*  The Method SetUpMoveBoundaries determins the min and max on the x and y axis of the camera and
        saves that value to the respective variable.
    */
      private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;
    }

    // Move let's the player move around the game camera
    public void Move()
    {
        float newXPos = HorizontalMovement();
        float newYPos = VerticalMovement();

        transform.position = new Vector2(newXPos, newYPos);
    }

    /* VerticalMovement gets the current position on the Y axis and adds the change from deltaY
    * * Time.deltaTime makes the movement be the same speed on any computer and moveSpeed is a variable
    * * implemented to increse the speed of Time.deltaTime    
     */
    private float VerticalMovement()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        return newYPos;
    }
    
    /* HorizontalMovement gets the current position on the X axis and adds the change from deltaX
    * * Time.deltaTime makes the movement be the same speed on any computer and moveSpeed is a variable
    * * implemented to increse the speed of Time.deltaTime    
     */
    private float HorizontalMovement()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        return newXPos;
    }
}
