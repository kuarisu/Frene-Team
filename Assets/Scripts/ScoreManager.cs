using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int blueScore;
	public int redScore;

	public int scoreDifferenceToWin;

	private bool didSomeoneWin = false;

	[Space(20)]

	public GameObject blueGameOver1;
	public GameObject blueGameOver2;

	[Space(20)]

	public GameObject redGameOver1;
	public GameObject redGameOver2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		if (blueScore - redScore >= scoreDifferenceToWin && didSomeoneWin == false) {

			// GameOver pour Blue
			blueGameOver1.SetActive(true);
			blueGameOver2.SetActive(true);
			didSomeoneWin = true;
		}

		if (redScore - blueScore >= scoreDifferenceToWin  && didSomeoneWin == false) {
			// GameOver pour Red
			redGameOver1.SetActive(true);
			redGameOver2.SetActive(true);
			didSomeoneWin = true;
		}
	}

	public void addScore(string team, int amount)
	{
		if (team == "red") {
			redScore += amount;
		}
		if (team == "blue") {
			blueScore += amount;
		}

	}
}
