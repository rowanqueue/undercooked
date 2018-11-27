using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Display : MonoBehaviour
{
	//public Image order1;
	///public Image order2;
	//public Image order3;
	public GameObject[] orders;
	public GameObject[] displayedImages;
	public static int ordertoDisplay;
	public float orderTime;
	public float ordersspace;
	public float orderHeight;
	public Canvas displayCanvas;
	private Vector3 SpawnPoint;
	
	//Every 10 seconds display 1/3 orders
		//Order 1- Bun and Meat
		//Order 2- Bun, Lettuce and Meat
		//Order 3 - Bun, Lettuce, Tomato, Meat
	
	
	// Use this for initialization
	void Start ()
	{
		SpawnPoint=new Vector3(Screen.height-31f,(Screen.width+ordersspace)-Screen.width,0);
		orderTime = Time.deltaTime;
		GameObject newBurger = Instantiate(orders[0],SpawnPoint, Quaternion.Euler(0,0,0));
		newBurger.transform.SetParent(this.transform,false);
		Debug.Log(Screen.height+"Ya");
	}
	
	// Update is called once per frame
	void Update ()
	{

		displayedImages = GameObject.FindGameObjectsWithTag("Order");
		
		foreach (GameObject order in displayedImages)
		{
			for (int i = 0; i < displayedImages.Length; i++)
			{
				ordersspace+= displayedImages[i].GetComponent<Renderer>().bounds.size.x;
				orderHeight = 31f;  //orders[i].GetComponent<Renderer>().bounds.size.y / (orders.Length * 2);
				SpawnPoint=new Vector3(Screen.height-31f,(Screen.width+ordersspace)-Screen.width,0);
				Debug.Log((ordersspace));
			}
		}
		
	}

	public void SetUi()
	{
		if (Time.deltaTime - orderTime >= 10f)
			{

					GameObject newBurger = Instantiate(orders[ordertoDisplay], SpawnPoint, Quaternion.Euler(0,0,0));
					newBurger.transform.SetParent(this.transform);
				orderTime = Time.deltaTime;

			}

		}

	}

