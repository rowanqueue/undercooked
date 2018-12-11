using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public float timer = 241f;
	//hnghhhhhhh
	private int minutes;
	private int seconds;
	

	public Text timeRemaining; 
	
	void Start () {
		
	}
	
	void Update () {
		if (timer > 0)
		{
			timer -= Time.deltaTime;
			seconds = (int)timer % 60;
			minutes = (int) timer / 60;
            timeRemaining.text = minutes + ":";
            if(seconds < 10f)
            {
                timeRemaining.text += "0";
            }
            timeRemaining.text += seconds;
		}
		else
		{
			Time.timeScale = 0;
			
		}
	}
}
