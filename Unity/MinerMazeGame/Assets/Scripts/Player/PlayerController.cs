using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	/* - - - - INSPECTOR GAMEOBJECTS - - - - */
	[Header("Components"), SerializeField]
	private Animator anim;

	[Header("Scripts"), SerializeField]
	public PlayerMovement playerMovement;

	[SerializeField]
	public PlayerHealth playerHealth;

	[SerializeField]
	public PlayerScore playerScore;

	/* - - - - PUBLICS - - - - */
	[HideInInspector]
	public bool isDone = false;

	private void Update()
	{
		bool spaceHeld = Input.GetKey(KeyCode.Space) && GameManager.instance.gameState == GameManager.GameState.ACTIVE;

		UpdateAnimations(spaceHeld, !spaceHeld);
		playerMovement.Move(spaceHeld);

		if (GameManager.instance.gameState == GameManager.GameState.ACTIVE)
		{
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
