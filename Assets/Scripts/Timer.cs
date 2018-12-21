using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
	public float timer = 20f; //241f;
	//hnghhhhhhh
	private int minutes;
	private int seconds;

	public AudioSource countdown;
	public AudioClip endofRound;
	private bool boolend = false;
	public Text timeRemaining;
    public Text timesUp;
	
	void Start ()
	{
		countdown = GetComponent<AudioSource>();
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

		if (timer >= 11.51)
		{
			
		}

		if (timer<=11.5f && timer> 1f)
		{
			if (countdown.isPlaying==false)
			{
				countdown.Play();
				countdown.volume = .65f;
			}
		}

		if (timer<.5f && boolend==false)
		{
			countdown.Stop();
			countdown.clip = endofRound;
			countdown.PlayOneShot(endofRound);
            timesUp.gameObject.SetActive(true);
			boolend = true;
		}

        if (boolend && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("TestLevel");
        }
	}
}
