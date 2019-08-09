using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //params
    [SerializeField] float moveSpeed = 16f;

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

    // Move let's the player move around the game c
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
        var newYPos = transform.position.y + deltaY;
        return newYPos;
    }
    
    /* HorizontalMovement gets the current position on the X axis and adds the change from deltaX
    * * Time.deltaTime makes the movement be the same speed on any computer and moveSpeed is a variable
    * * implemented to increse the speed of Time.deltaTime    
     */
    private float HorizontalMovement()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = transform.position.x + deltaX;
        return newXPos;
    }
}
