using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonFeedback : MonoBehaviour {

	public KeyCode linkedKey;
	public string buttonText;

	private Text mytext;

	// Use this for initialization
	void Start () {

		mytext = GetComponentInChildren<Text> ();
		mytext.text = buttonText;
	}

	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(linkedKey)) {
			mytext.color = Color.gray;
			gameObject.transform.localScale = new Vector3 (0.9f,0.9f,1f);
		} 
		else
		{
			mytext.color = Color.white;
			gameObject.transform.localScale = new Vector3 (1f,1f,1f);
		}
	}
}
