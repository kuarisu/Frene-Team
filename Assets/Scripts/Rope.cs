using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Rope : MonoBehaviour {

	private int blueScore;
	public Text blueScore_UI;


	private int redScore;
	public Text redScore_UI;

	private float scoreDifference = 0;

	private ScoreManager scoreManager;

	void Start () {

		scoreManager = FindObjectOfType<ScoreManager>();


	}
	

	void Update () {

		blueScore = scoreManager.blueScore;
		redScore = scoreManager.redScore;

		// Mise à jour de l'UI textuelle pour les scores des équipes
		blueScore_UI.text = "Score : " + blueScore;
		redScore_UI.text = "Score : " + redScore;

		// Déplacement de la corde vers la gauche ou la droite en fonction de quelle équipe prends l'avantage
		scoreDifference = redScore - blueScore;





		gameObject.transform.localPosition = new Vector3(scoreDifference * (512f/scoreManager.scoreDifferenceToWin), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);


		// blocage du mouvement de la corde si elle va trop à gauche/droite

		if (gameObject.transform.localPosition.x > 512) {
			gameObject.transform.localPosition = new Vector3(512, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
		}

		if (gameObject.transform.localPosition.x < -512) {
			gameObject.transform.localPosition = new Vector3(-512, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
		}
	}
}
