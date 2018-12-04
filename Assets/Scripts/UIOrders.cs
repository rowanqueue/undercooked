using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

//Usage: Put this on a canvas, Canvas: Screen Space Camera, Scale With Screen Size
//Intent: Display Orders at the top of the screen and Only 5 possible orders at any given time
		//Every 15 seconds display 
		//Order 1- Bun and Meat
		//Order 2- Bun, Lettuce and Meat
		//Order 3 - Bun, Lettuce, Tomato, Meat	
//Tips for me:
			//Check list and tag of object 
			//if tag is Small order add Small order , if medium order add x
			//Have a for and foreach loop iterate through the list of displayed UI and calculate the X displacement
//Future Planned Additions:
			//Ui elements will tween to correct pos
			//bun will drop down after Ui elements is in correct spot 
public class UIOrders : MonoBehaviour
{
	[Header("Order Lists")]
	public RectTransform[] OrderRects; //Assign UI Displays
	public List<Transform> DisplayedOrders =new List<Transform>(); //All orders currently onscreen 
	public List<GameObject> CompletedOrders = new List<GameObject>(); //Orders that have been fulfilled 
	[Header("GameObjects to Assign")]
	public GameObject Canvas;
	[Header("Public Variables")]
	public float timeToNextOrder;
	public float smallOrderTime;
	public float mediumOrderTime;
	public float largeOrderTime;
	public int testComplete;
	
	public static int numOrder;
	public static int missedOrder;
	public static int CompletedOrderNum;
	public static int PositionInList;
	
	private bool newOrder = false;
	private bool first;
	private bool shrink;
	public static bool Completed=false;
	private float orderXdisplacement;
	
	 void Start()
	 {
		 missedOrder = 0;
		 first = true;
		 testComplete = 0;
		RectTransform newBurger=Instantiate(OrderRects[0],new Vector3(28.91f,-18.9f,0f), Quaternion.Euler(0, 0, 0));
		 newBurger.transform.SetParent(Canvas.transform,false);
		 DisplayedOrders.Add(newBurger);
		 StartCoroutine(NewOrder());
		 
	 }

	void Update()
	{
		InstantiateOrder();
		CompletedOrder();
		//ShrinkUI();
		
	}

	void InstantiateOrder() //This will spawn a UI element and eventually lerp it to the position. Also set its tag to "Order"
	{
		if (newOrder)
		{
			
			Order.GenerateBurger(Random.Range(0, 2));
			

			for (int i = 0; i <= DisplayedOrders.Count; i++)
			{
				orderXdisplacement = 0; //Reset x offset each time 
				foreach (Transform g in DisplayedOrders) //This adds the width of all displayed Ui elements so the new one is in the correct x pos 
				{
					if (g.CompareTag("SmallOrder"))
					{
						orderXdisplacement += 57;
					}

					if (g.CompareTag("MediumOrder"))
					{
						orderXdisplacement += 72;
					}

					if (g.CompareTag("LargeOrder"))
					{
						orderXdisplacement += 89;
					}
				}
				
//				Debug.Log("X displaced" + orderXdisplacement);
			}

			if (DisplayedOrders.Count <= 4)
			{
				RectTransform newBurger = Instantiate(OrderRects[numOrder],
					new Vector3(30 + orderXdisplacement, -18.9f, 0f), Quaternion.Euler(0, 0, 0));
				newBurger.transform.SetParent(Canvas.transform, false); 
				DisplayedOrders.Add(newBurger);//Adds new order to Displayed Ui for completed order comparison 
			}

			if (DisplayedOrders.Count == 5)
			{
				missedOrder += 1;//For end game screen. How many orders completed Vs Potential if we want to vary level time 
			}

			newOrder = false;
//			Debug.Log("New Order False");
		}
	}

	/*void ShrinkUI()
	{
		
			foreach (Transform t in DisplayedOrders)
			{
				Transform TimeBar = t.FindChild("Sko");

				Vector3 originalScale = TimeBar.transform.localScale;
				Vector3 targetScale = new Vector3(0, TimeBar.transform.localScale.y, TimeBar.transform.localScale.z);
				float timer = Time.unscaledTime;
				//float objecttime = Time.unscaledTime;
				
				if (originalScale.x >= targetScale.x)
				{
					if (TimeBar.parent.CompareTag("SmallOrder"))
					{
						TimeBar.transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / smallOrderTime);


					}

					if (TimeBar.parent.CompareTag("MediumOrder"))
					{

						
					}

					if (TimeBar.parent.CompareTag("LargeOrder"))
					{

					}
				}
			}
		

	}*/
	
	void CompletedOrder()
	{
		if (Completed)
		{
			for (int i = PositionInList; i <= DisplayedOrders.Count; i++)
			{
				foreach (Transform t in DisplayedOrders)
				{
					if (CompletedOrderNum == 0)
					{
						t.position -= new Vector3(57, 0, 0);

					}
				}
			}
			Completed = false;
		}
	}
	
	IEnumerator NewOrder()
	{
		//Every 15 seconds an order will show on screen or be added to queue 
		while (true)
		{

			if (!newOrder)
			{
//				Debug.Log("start");
				yield return new WaitForSeconds(timeToNextOrder);//Edit this to 15f for playtest

				newOrder = true;//Starts Instantiate order Script
			}

			yield return true;
		}
	}
}
