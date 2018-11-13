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
	public float baseSpeed;
	public float boostMultiplier;
	public float boostTime;
	public float boostCooldownTime;
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
		
		float Horizontal = Input.GetAxis("Horizontal"+ myPlayerName);
		float Vertical = Input.GetAxis("Vertical" + myPlayerName);
        float Boost = Input.GetAxis("Boost" + myPlayerName);
		
		inputVector = (Vector3.forward * Vertical);
		inputVector += (Vector3.right * Horizontal);
		
		speedBoost = Vector3.back * (Vertical);
		speedBoost += Vector3.left * (Horizontal);
		
		IsBoosting();
		if (inputVector != Vector3.zero)
		{


			transform.forward = inputVector;
		}
		
		
		



	}


	void FixedUpdate()
	{
		if (inputVector != Vector3.zero)
		{
			
			rb.MovePosition(playerPos + inputVector * speed * Time.deltaTime);
			
		}

		
		
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
		
		if ( (myPlayerName=="Player1" && Input.GetKeyDown(KeyCode.E)) || (myPlayerName=="Player2" && Input.GetKeyDown(KeyCode.RightShift)) && !boostCooldown)
		{
			boosting = true;
			StartCoroutine(coolDown.GetEnumerator());
			Debug.Log("Boost Time");
			
		}
		
	}
	
	


	IEnumerable CoolingDown()
	{
		
		if (boosting && !boostCooldown)
		{
			Debug.Log("Boosting");
			speed = speed * boostMultiplier;
			bouceSpeed = -speed * boostMultiplier/3f;
			yield return new WaitForSeconds(boostTime);
			boostCooldown = true;
			
		}

		if (boostCooldown)
		{
			bounce = false;
			boosting = false;
			bouceSpeed = baseSpeed;
			speed = baseSpeed;			
			Debug.Log("Done Boosting");	
			yield return new WaitForSeconds(boostCooldownTime);
			boostCooldown = false;
		}
	


	}


	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player") && boosting )
		{
			bounce = true;

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
