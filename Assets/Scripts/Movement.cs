using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Serialization;

//use: moves player
//loc: on a player
public class Movement : MonoBehaviour
{
	
    [Header("Boost Duration and Cooldown time")]
	public float boostTime; //how long the player can boost 
	public float boostCooldownTime; //amount of time in between boosts 
	[Header("Speed Variables")]
	[FormerlySerializedAs("baseSpeed")] public float walkingSpeed; //player movement speed
	public float boostMultiplier; //multiplies basespeed
	private float speed; //current speed of player 
	private float bounceSpeed;  //reverses player direction on impact while boosting 
	private Vector3 playerPos;
    private float yPos;
	private Vector3 inputVector;
	private Rigidbody rb;
	public Animator animator;
	private bool BoostUp;
	private bool boosting=false;
	private bool boostCooldown=false;
	private bool bounce=false;
	[Header("Input Player name below")]
	[FormerlySerializedAs("MyplayerName")] public string myPlayerName;
	private IEnumerable coolDown; //Using IEnumerable instead of IEnumerator so the method can be called multiple times


	void Start()
	{
		//myPlayerName = name;
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		coolDown = CoolingDown();
		speed = walkingSpeed;
        yPos = transform.position.y;
		
		
	}

	void Update()
	{	
		playerPos = transform.position;
        playerPos.y = yPos;
        transform.position = playerPos;
		//Get input values 
		float Horizontal = Input.GetAxis("Horizontal"+ myPlayerName);
		float Vertical = Input.GetAxis("Vertical" + myPlayerName);
        float Boost = Input.GetAxis("Boost" + myPlayerName);
		
		//Set input vector in relation to axis values 
		inputVector = (Vector3.forward * Vertical);
		inputVector += (Vector3.right * Horizontal);
		LockPosition();//This will prevent the players from pushing players when they're not boosting 
		//have player face where they're moving 
		//IsBoosting();
		if (inputVector != Vector3.zero)
		{
			animator.SetBool("Walking", true);
			transform.forward = inputVector;
			inputVector.y = 0;
		}
		else{animator.SetBool("Walking", false);}
	}


	void FixedUpdate()
	{
		//if there is input move player
		if (inputVector != Vector3.zero)
		{
			
			rb.MovePosition(playerPos + inputVector * speed * Time.deltaTime);
			
		}
		// if players collide and are boosting speed will change 
		if (bounce)
		{
			speed = bounceSpeed;
			
		}

	}
	
	
	void IsBoosting()
	{
		//if player boosts and boost is not in cooldown do the following 
		if ( (myPlayerName=="Player1" && Input.GetKeyDown(KeyCode.E)) || (myPlayerName=="Player2" && Input.GetKeyDown(KeyCode.RightShift)) && !boostCooldown)
		{
			boosting = true;
			StartCoroutine(coolDown.GetEnumerator());
			Debug.Log("Boost Time");
			
		}
		
	}
	
	IEnumerable CoolingDown()
	{
		//code will run if the player is boosting and is allowed to boost 
		if (boosting && !boostCooldown)
		{
			Debug.Log("Boosting");
			speed = speed * boostMultiplier; //change the players speed to boost speed
			bounceSpeed = -speed * boostMultiplier/3f; //if players collide while boosting speed will be set to this value 
			yield return new WaitForSeconds(boostTime); //duration of boost 
			boostCooldown = true;
			
		}

		if (boostCooldown)
		{
			bounce = false;
			boosting = false;
			bounceSpeed = walkingSpeed;
			speed = walkingSpeed;	//set speed back to basespeed after boosting 		
			Debug.Log("Done Boosting");	
			yield return new WaitForSeconds(boostCooldownTime); //duration of boost cooldown 
			boostCooldown = false;
		}
	}

	void LockPosition()
	{
		Ray LookRay = new Ray(transform.position,transform.forward);

		float maxrayDist = .65f;
		RaycastHit otherPlayer; //RayCast hit, this script only checks if it is hitting the other player
		
		Debug.DrawRay(LookRay.origin,LookRay.direction* maxrayDist ,Color.yellow);

		if (Physics.Raycast(LookRay, maxrayDist)&& !bounce)
		{
			if (CompareTag("Player") ||  CompareTag("Counter"))
			{
				rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | 
                                 RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezeRotationZ; //If player is colliding with the other player freeze position 
				boosting = false;
			}	
			
		}
		else
		{  //Turn all constraints off then rotation constraints back on
			rb.constraints = RigidbodyConstraints.None;
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
			                 RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
		}
		
	}
	
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (boosting)
			{
				bounce = true; //if players collide set bounce to true
				//this will only be used if the player is boosting 
			}

			if (!boosting)
			{
				
			}
		}	
	}
}
