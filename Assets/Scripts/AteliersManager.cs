using UnityEngine;
using System.Collections;

public class AteliersManager : MonoBehaviour {


	public bool hasGameStarted;
	[Space(20)]
	public int minScoreGain;
	public int maxScoreGain;

	[Space(20)]

	public float baseTimeBetweenEvents;
	public float acceleration;
	private float timeUntilNextEvent;

	private Atelier_Conditions[] ateliersList;
	private int randomInt;
	private int previousRandomInt;

	void Start () {
	
		timeUntilNextEvent = baseTimeBetweenEvents;

		ateliersList = GetComponentsInChildren<Atelier_Conditions> (true);


	}

	void Update () {
	// If double input des leaders + bool
		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.Z) && !hasGameStarted)
		{
			Debug.Log ("GAME STARTED");
			hasGameStarted = true;
			Invoke ("LaunchAtelier", timeUntilNextEvent);
		}




	}

	void LaunchAtelier()
	{
		//Debug.Log ("Atelier !!! Prochain dans : " + timeUntilNextEvent + " sec");
	

		timeUntilNextEvent -= Random.Range (0, acceleration);
		if (timeUntilNextEvent <= 1f) {
			timeUntilNextEvent = 1f;
		}

		// Choix parmis tout les ateliers
		previousRandomInt = randomInt;
		while (previousRandomInt == randomInt) {
			randomInt = Random.Range (0, ateliersList.Length - 1);
		}



		for (int i = 0; i < ateliersList.Length - 1; i++) {
		//	ateliersList[i].gameObject.SetActive (false);
		}
	
	//	ateliersList[randomInt].gameObject.SetActive (true);
		GameObject instance = Instantiate(ateliersList[randomInt].gameObject);
		instance.SetActive (true);

		instance.transform.SetParent (gameObject.transform);
		instance.transform.localPosition = new Vector3 (0f, 0f, 0f);
		instance.transform.localScale = new Vector3 (1f, 1f, 1f);




		// Lancement du prochain atelier dans x secondes après la fin de celui ci
	//	Invoke ("LaunchAtelier", timeUntilNextEvent + ateliersList[randomInt].m_MaxTime);


	}

	public void LaunchNext()
	{
		Invoke ("LaunchAtelier", timeUntilNextEvent);
	}
		
}
