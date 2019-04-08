using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	/* - - - - INSPECTOR GAMEOBJECTS - - - - */
	[Header("Components"), SerializeField]
	private Animator anim;

	[SerializeField]
	AudioSource itemSound;

	[Header("Scripts"), SerializeField]
	public PlayerMovement playerMovement;
	public PlayerHealth playerHealth;
	public PlayerScore playerScore;
	public PlayerMining playerMining;



	/* - - - - PUBLICS - - - - */
	[HideInInspector]
	public bool isDone = false;

	private void Update()
	{
		bool gameActive = GameManager.instance.gameState == GameManager.GameState.ACTIVE;
		bool tryingMine = Input.GetKey(KeyCode.Space);


		UpdateAnimations(tryingMine && gameActive, !tryingMine && gameActive);
		playerMovement.Move(tryingMine);



		if (gameActive)
		{
			if (tryingMine && !playerMovement.isMoving)
			{
				playerMining.Mine();
			}
			else if (!playerMovement.isMoving)
			{
				playerMining.PlaceBlock();
			}

			playerHealth.UpdateHealth();
			playerScore.UpdateScore();
		}


		//playerHealth.currentHealth = 100;
	}


	private void UpdateAnimations(bool mining, bool idle)
	{
		anim.SetBool("Idle", idle);
		anim.SetBool("Mining", mining);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("HealthPotion"))
		{
			itemSound.Play();
			playerScore.score += 25.0f;
			playerHealth.currentHealth += 20;

			Destroy(collision.gameObject);

			MineManager.instance.SpawnNewPotion();
		}

		else if (collision.CompareTag("ExitTile"))
		{
			if (playerHealth.currentHealth >= 90.0f)
			{
				isDone = true;
			}
		}
	}
}
