using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    public static UIOrders me;
	[Header("Order Lists")]
	public RectTransform[] OrderRects; //Assign UI Displays
	public List<Transform> DisplayedOrders =new List<Transform>(); //All orders currently onscreen 
	public List<GameObject> CompletedOrders = new List<GameObject>(); //Orders that have been fulfilled 
    public List<Order> orders = new List<Order>();
	[Header("GameObjects to Assign")]
	public GameObject Canvas;
	[Header("Public Variables")]
	public float timeToNextOrder;
    public float orderTime;
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
    public List<float> orderTimes;//when order was created
	 void Awake()
	 {
        orderTime = 240;
         me = this;
		 missedOrder = 0;
		 first = true;
		 testComplete = 0;
		 
	 }

	void Update()
	{
		for(int i = 0; i < orderTimes.Count; i++)
        {
            Image image = DisplayedOrders[i].GetChild(1).GetComponent<Image>();
            float lerpAmount = 1-((Time.time - orderTimes[i])/orderTime);
            image.fillAmount = lerpAmount;
            image.color = Color.Lerp(Color.red,Color.green, lerpAmount);
        }
		
	}

	public void InstantiateOrder(int orderNum, Order order) //This will spawn a UI element and eventually lerp it to the position. Also set its tag to "Order"
	{
        for (int i = 0; i <= DisplayedOrders.Count; i++)
        {
            orderXdisplacement = 0; //Reset x offset each time 
            foreach (Transform g in DisplayedOrders) //This adds the width of all displayed UI elements so the new one is in the correct x pos 
            {
                if (g.CompareTag("SmallOrder"))
                {
                    orderXdisplacement += 92 + 5;//old 57, whatever x scale is + 3
                }

                if (g.CompareTag("MediumOrder"))
                {
                    orderXdisplacement += 119 + 5;//old 72
                }

                if (g.CompareTag("LargeOrder"))
                {
                    orderXdisplacement += 148 + 5;//old 89
                }
            }

            //				Debug.Log("X displaced" + orderXdisplacement);
        }
        if (DisplayedOrders.Count <= 4)
        {
            orders.Add(order);
            orderTimes.Add(Time.time);
            RectTransform newBurger = Instantiate(OrderRects[orderNum],
                new Vector3(40 + orderXdisplacement, -31.4f, 0f), Quaternion.Euler(0, 0, 0));
            newBurger.transform.SetParent(Canvas.transform, false);
            DisplayedOrders.Add(newBurger);//Adds new order to Displayed Ui for completed order comparison 
        }

        if (DisplayedOrders.Count == 5)
        {
            missedOrder += 1;//For end game screen. How many orders completed Vs Potential if we want to vary level time 
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
	
	public void CompletedOrder(Order order)
	{
        CompletedOrderNum = orders.IndexOf(order);
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
        orderTimes.RemoveAt(CompletedOrderNum);
    }
}
