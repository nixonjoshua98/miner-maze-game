using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	public enum GameState {ACTIVE, LOSE, WON};

	[HideInInspector]
	public GameState gameState = GameState.ACTIVE;

	/* - - - - INSPECTOR PRIVATES - - - - */
	[SerializeField]
	PlayerController playerController;

	[SerializeField]
	GameObject postscreen;


	private void Awake()
	{
		instance = this;
	}


	private void Update()
	{
		
		if (playerController.isDone)
		{
			gameState = GameState.WON;

			postscreen.SetActive(true);
		}
		else if (playerController.playerHealth.IsDead())
		{
			gameState = GameState.LOSE;

			postscreen.SetActive(true);
		}
	}
}
