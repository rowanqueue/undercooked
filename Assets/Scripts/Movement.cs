using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use: moves player
//loc: on a player
public class Movement : MonoBehaviour {
    public string xAxis;
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
			rb.AddForce(inputVector*accelerationFactor,ForceMode.Force);//this moves the player

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
		return new Vector3(x, 0, y);
	}
}
