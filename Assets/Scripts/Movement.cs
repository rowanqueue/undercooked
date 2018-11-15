using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//use: moves player
//loc: on a player
public class Movement : MonoBehaviour
{

	private float speed;
	private float bouceSpeed;
	public float baseSpeed; //player movement speed
	public float boostMultiplier; //multiplies basespeed
	public float boostTime; //how long the player can boost 
	public float boostCooldownTime; //amount of time in between boosts 
	private Vector3 speedBoost; 
	private Vector3 playerPos;
	private Vector3 inputVector;
	private Rigidbody rb;
	private bool BoostUp;
	private bool boosting=false;
	private bool boostCooldown=false;
	private bool bounce=false;
	[FormerlySerializedAs("MyplayerName")] public string myPlayerName;
	private IEnumerable coolDown;
	
	
	//private bool P1Input=false;
	//private bool P2Input=false;

	void Start()
	{
		//myPlayerName = name;
		rb = GetComponent<Rigidbody>();
		coolDown = CoolingDown();
		speed = baseSpeed;
		

	}

	void Update()
	{
		
		playerPos = transform.position;
		
		//Get input values 
		float Horizontal = Input.GetAxis("Horizontal"+ myPlayerName);
		float Vertical = Input.GetAxis("Vertical" + myPlayerName);
        float Boost = Input.GetAxis("Boost" + myPlayerName);
		
		//Set input vector in relation to axis values 
		inputVector = (Vector3.forward * Vertical);
		inputVector += (Vector3.right * Horizontal);
		
		//have player face where they're moving 
		IsBoosting();
		if (inputVector != Vector3.zero)
		{


			transform.forward = inputVector;
		}
		
		
		



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
			speed = bouceSpeed;
			
		}
		/*
		if (boosting && speedBoost!=Vector3.zero)
		{
			rb.MovePosition(playerPos + inputVector * boostSpeed * Time.smoothDeltaTime);
				
		}*/



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
			bouceSpeed = -speed * boostMultiplier/3f; //if players collide while boosting speed will be set to this value 
			yield return new WaitForSeconds(boostTime); //duration of boost 
			boostCooldown = true;
			
		}

		if (boostCooldown)
		{
			bounce = false;
			boosting = false;
			bouceSpeed = baseSpeed;
			speed = baseSpeed;	//set speed back to basespeed after boosting 		
			Debug.Log("Done Boosting");	
			yield return new WaitForSeconds(boostCooldownTime); //duration of boost cooldown 
			boostCooldown = false;
		}
	


	}


	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player") && boosting )
		{
			bounce = true; //if players collide set bounce to true
						//this will only be used if the player is boosting 

		}
		
	}
	
	//(Player Boost Timer) 
	//Ienumerator One bool for p1 one for p2
	// Bools turn true a few seconds after boost 
	//After Click boost last 1-1.5 seconds then turns false 
	
	
	
	
	
	
	
	
	
	/* public string xAxis;
    public string yAxis; //will actually be z movement bc x,y,z is TRASH!!!!!
	public float maxSpeed;
	public float accelerationFactor;

	private Vector3 inputVector;
	private Rigidbody rb;
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		inputVector = HandleInput();
		//changes where the player is looking
		if (inputVector != Vector3.zero)
		{
            //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,inputVector,accelerationFactor*Time.deltaTime,0f));
            transform.LookAt(transform.position + inputVector);
		}
	}

	private void FixedUpdate()
	{
		if (inputVector != Vector3.zero)//input?? amazing
		{
			//rb.AddForce(inputVector*accelerationFactor,ForceMode.Force);//this moves the player
			

		}
		else
		{
			rb.velocity = new Vector3(0,rb.velocity.y,0);
		}
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
	}

	Vector3 HandleInput()
	{
		float x = Input.GetAxis(xAxis);
		float y = Input.GetAxis(yAxis);
		return new Vector3(x, 0, y);*/
	

}
