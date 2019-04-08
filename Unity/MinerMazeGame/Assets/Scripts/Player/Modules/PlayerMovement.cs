using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer sRenderer;

	[SerializeField]
	AudioSource movementSound;

	/* - - - - INSPECTOR PRIVATES - - - - */
	[SerializeField, Range(1.0f, 10.0f)]
	private float speedMulti;

	[HideInInspector]
	public bool isMoving = false;

	private Vector3 moveTarget;


	public void Move(bool tryingToMine)
	{
		if (isMoving)
		{
			MovePlayer();
		}
		else if (!isMoving)
		{
			if (!tryingToMine)
			{
				if (GameManager.instance.gameState == GameManager.GameState.ACTIVE)
					GetNewMovementTarget();
			}
		}
	}


	private void MovePlayer()
	{
		if (!movementSound.isPlaying)
			movementSound.Play();

		transform.position = Vector3.MoveTowards(transform.position, moveTarget, 1.0f * Time.deltaTime * speedMulti);

		CheckPositionToTarget();
	}


	private Vector3 GetInputVector()
	{
		Vector3 dir = Vector3.zero;

		if (Input.GetKey(KeyCode.W))
			dir.y = 1;

		else if (Input.GetKey(KeyCode.A))
			dir.x = -1;

		else if (Input.GetKey(KeyCode.S))
			dir.y = -1;

		else if (Input.GetKey(KeyCode.D))
			dir.x = 1;

		return dir;
	}


	private void GetNewMovementTarget()
	{
		Vector3 dir = GetInputVector();

		UpdateSprite(dir);

		if (dir != Vector3.zero && ValidPosition(dir))
		{		
			moveTarget = transform.position + dir;
			isMoving = true;
		}
	}


	private void UpdateSprite(Vector3 pos)
	{
		if (pos.x > 0f)
			sRenderer.flipX = false;
		else if (pos.x < 0.0f)
			sRenderer.flipX = true;
	}


	private void CheckPositionToTarget()
	{
		if (transform.position == moveTarget)
		{
			transform.position = new Vector3((float)Math.Round(transform.position.x, 1), (float)Math.Round(transform.position.y, 1), 0.0f);
			isMoving = false;
		}
	}


	private bool ValidPosition(Vector3 pos)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position + pos, Vector3.forward);

		return (!(hit.collider.CompareTag("WallTile") || hit.collider.CompareTag("BreakTile")));
	}
}