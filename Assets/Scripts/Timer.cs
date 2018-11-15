using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public float timer = 60f;
	//hnghhhhhhh

	public Text timeRemaining; 
	
	void Start () {
		
	}
	
	void Update () {
		if (timer > 0)
		{
			timer -= Time.deltaTime;
			timeRemaining.text = "" + (int)timer;
		}
		else
		{
			Time.timeScale = 0;
		}
	}
}
