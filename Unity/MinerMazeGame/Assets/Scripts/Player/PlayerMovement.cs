using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private Animator anim;				// Animator state machine object

	[SerializeField]
	private SpriteRenderer sRenderer;   // Animator state machine object

	[SerializeField]
	private Rigidbody2D rb;             // Rigid body 2D component


	private float speedMulti = 3.5f;    // Multiplies the direction axis


	private void Update()
	{
		bool spaceHeldDown = Input.GetKey("space");

		if (spaceHeldDown)
		{
			StartMining();
		}
		else
		{
			StopMining();
			MovePlayer();
		}
	}


	private void MovePlayer()
	{
		float vert = Input.GetAxis("Vertical") * Time.deltaTime;
		float hori = Input.GetAxis("Horizontal") * Time.deltaTime;

		// Player movement direction
		Vector3 dir = new Vector3(hori, vert, 0);

		// Debugging...
		Debug.DrawRay(transform.position, dir.normalized, Color.green);

		// Flip to face direction
		sRenderer.flipX = (hori != 0.0f ? hori < 0.0f : sRenderer.flipX); 

		// Move player
		rb.MovePosition(transform.position + (dir * speedMulti));
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
}