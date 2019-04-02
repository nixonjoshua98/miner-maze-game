using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameScreen : MonoBehaviour
{
	[SerializeField]
	PlayerController playerController;

	[SerializeField]
	Text scoreTxt;

	[SerializeField]
	Text timeSurvivedTxt;

	[SerializeField]
	Text healthTxt;

	void OnEnable()
	{
		scoreTxt.text = "Score: " + ((int)playerController.playerScore.score).ToString().PadLeft(5, '0');
		timeSurvivedTxt.text = "Time Survived: " + ((int)Time.timeSinceLevelLoad).ToString() + "s";
		healthTxt.text = "Final Health: " + ((int)playerController.playerHealth.currentHealth).ToString() + "/100";
	}
}
