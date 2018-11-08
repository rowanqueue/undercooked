using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//use: moves player
//loc: on a player
public class Movement : MonoBehaviour
{

	public float speed;
	public float boostMult;
	private Vector3 speedBoost; 
	private Vector3 playerPos;
	private Vector3 inputVector;
	private Rigidbody rb;
	[FormerlySerializedAs("MyplayerName")] public string myPlayerName;
	
	
	
	//private bool P1Input=false;
	//private bool P2Input=false;

	void Start()
	{
		//myPlayerName = name;
		rb = GetComponent<Rigidbody>();
	
	}

	void Update()
	{
		playerPos = transform.position;
		
		float Horizontal = Input.GetAxis("Horizontal"+ myPlayerName);
		float Vertical = Input.GetAxis("Vertical" + myPlayerName);
		float Boost = Input.GetAxis("Boost" + myPlayerName);
		
		inputVector = (Vector3.forward * Vertical);
		inputVector += (Vector3.right * Horizontal);
		speedBoost = (inputVector* Boost);	
		
		
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

			if (speedBoost != Vector3.zero)
			{
				rb.MovePosition(playerPos+speedBoost*boostMult*Time.smoothDeltaTime);
				
			}
			

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
