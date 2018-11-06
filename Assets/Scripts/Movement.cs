using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use: moves player
//loc: on a player
public class Movement : MonoBehaviour
{

	public float P1Speed;
	public float P2Speed;
	public float SpeedBoost;
	private Vector3 Player1pos;
	private Vector3 Player2pos;
	private Vector3 P1InputVector;
	private Vector3 P2InputVector;
	private Rigidbody P1Rb;
	private Rigidbody P2Rb;
	private GameObject P1;
	private GameObject P2;
	private bool P1Input=false;
	private bool P2Input=false;

	void Start()
	{
		P1 = GameObject.FindGameObjectWithTag("Player1");
		P2 = GameObject.FindGameObjectWithTag("Player2");
		P1Rb = P1.GetComponent<Rigidbody>();
		P2Rb = P2.GetComponent<Rigidbody>();
		
	}

	void Update()
	{
		Player1pos = P1.transform.position;
		Player2pos = P2.transform.position;
		
		

		float P1Horizontal = Input.GetAxis("HorizontalP1");
		float P1Vertical = Input.GetAxis("VerticalP1");
		
		float P2Horizontal = Input.GetAxis("HorizontalP2");
		float P2Vertical = Input.GetAxis("VerticalP2");

		P1InputVector = (P1.transform.forward * P1Vertical);
		P1InputVector += (P1.transform.right * P1Horizontal).normalized;
		
		P2InputVector= (P2.transform.forward * P2Vertical);
		P2InputVector += (P2.transform.right * P2Horizontal).normalized;
		
		Vector3 P1Move= Player1pos+P1InputVector;
		

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) ||
		    Input.GetKey(KeyCode.D))
		{
			
			P1Input = true;
		}
		else
		{
			P1Input = false;
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) ||
		    Input.GetKey(KeyCode.RightArrow))
		{
			P2Input = true;
		}
		else
		{
			P2Input = false;
		}

		
		

	
	}

	void FixedUpdate()
	{
		if (P1Input)
		{
			P1Rb.MovePosition(Player1pos+P1InputVector * P1Speed * Time.deltaTime);
		
			if (Input.GetKey(KeyCode.E))
			{
				P1Rb.MovePosition(Player1pos+P1InputVector * SpeedBoost * Time.deltaTime);
			}
			
		}
		 if (P2Input)
		{
			P2Rb.MovePosition(Player2pos+P2InputVector * P2Speed * Time.deltaTime);
		
			if (Input.GetKey(KeyCode.RightShift))
			{	
				P2Rb.MovePosition(Player2pos+P2InputVector * SpeedBoost * Time.deltaTime);
			}
		}
		

	}


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
