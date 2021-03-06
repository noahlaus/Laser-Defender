﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] float moveSpeed = 16f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0,1)] float shootVolume = 0.25f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    Coroutine firingCoroutine;

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
        Fire();
    }

    private void Fire()
    {
       if(Input.GetButtonDown("Fire1")){
           firingCoroutine = StartCoroutine(FireContinuously());   
        }
       if(Input.GetButtonUp("Fire1")){
           StopCoroutine(firingCoroutine);
       }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer){
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die(){
        FindObjectOfType<LevelController>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
    }

    public int GetHealth(){
        return health;
    }


    IEnumerator FireContinuously(){ 
        while (true){
            GameObject laser = Instantiate(
                laserPrefab, 
                transform.position, 
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
        yield return new WaitForSeconds(projectileFiringPeriod);
        }
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
