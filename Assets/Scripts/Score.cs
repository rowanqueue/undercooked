using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public static Score me;
    public int score;
    public Text display;
	// Use this for initialization
	void Awake () {
        me = this;
	}
	
	// Update is called once per frame
	void Update () {
        display.text = score.ToString();
	}
}
