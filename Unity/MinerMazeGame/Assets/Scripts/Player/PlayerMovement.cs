using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerMovement : MonoBehaviour
{
    /* - - - - INSPECTOR GAMEOBJECTS - - - - */
	[SerializeField]
	private Animator anim;              // Animator state machine object

	[SerializeField]
	private SpriteRenderer sRenderer;


    /* - - - - PRIVATES - - - - */
	private float speedMulti = 2.0f;
	private bool isMoving = false;
	private Vector3 moveTarget;


	private void Update()
	{
		bool spaceHeldDown = Input.GetKey(KeyCode.Space);


		if (spaceHeldDown)
		{
			StartMining();
		}
		else
		{
			StopMining();

			if (isMoving)
			{
				MovePlayer();
			}
			else
				GetNewMovementTarget();
		}
	}


	private void MovePlayer()
	{
		transform.position = Vector3.MoveTowards(transform.position, moveTarget, 1.0f * Time.deltaTime * speedMulti);
		CheckPositionToTarget();
	}


	private Vector3 GetInputVector()
	{
		Vector3 dir = new Vector3(0, 0, 0);

		if (Input.GetKey(KeyCode.W)) dir.y = 1;
		else if (Input.GetKey(KeyCode.A)) dir.x = -1;
		else if (Input.GetKey(KeyCode.S)) dir.y = -1;
		else if (Input.GetKey(KeyCode.D)) dir.x = 1;

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


	private void StartMining()
	{
		anim.SetBool("Idle", false);
		anim.SetBool("Mining", true);
	}


	private void StopMining()
	{
		anim.SetBool("Idle", true);
		anim.SetBool("Mining", false);
	}


	private void MovePlayerUsingAxis()
	{
		//float vert = Input.GetAxis("Vertical") * Time.deltaTime;
		//float hori = Input.GetAxis("Horizontal") * Time.deltaTime;


		//// Player movement direction
		//Vector3 dir = new Vector3(hori, vert, 0);


		//// Debugging...
		//Debug.DrawRay(transform.position, dir.normalized, Color.green);


		//// Flip to face direction
		//sRenderer.flipX = (hori != 0.0f ? hori < 0.0f : sRenderer.flipX);


		//// Move player
		//rb.MovePosition(transform.position + (dir * speedMulti));
	}
}