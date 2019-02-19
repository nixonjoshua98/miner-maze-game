using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private Animator anim;	// Animator state machine object

	[SerializeField]
	private SpriteRenderer sRenderer;   // Animator state machine object

	[SerializeField]
	private Rigidbody2D rb;   // Rigid body 2D component


	/* - - - - PUBLIC - - - - */
	public GameObject arrowPrefab; // Arrow prefab object


	/* - - - - PRIVATE - - - - */
	private GameObject directionArrow;	// Arrow which will be used by the player
	private float speedMulti = 4.5f;    // Multiplies the direction axis


	private void Start()
	{
		directionArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity) as GameObject;
	}


	private void Update()
	{
		bool mining = Input.GetKey("space");

		anim.SetBool("Mining", mining);
		anim.SetBool("Idle", !mining);

		MovePlayer(mining);
	}


	private void MovePlayer(bool currentMining)
	{
		float vert = Input.GetAxis("Vertical") * Time.deltaTime;
		float hori = Input.GetAxis("Horizontal") * Time.deltaTime;
		Vector3 endPos = transform.position + (new Vector3(hori, vert, 0) * speedMulti);

		// Not mining
		if (!currentMining)
		{
			// Place directional arrow
			PlaceArrow(endPos);

			// Flip to face direction
			sRenderer.flipX = (hori != 0.0f ? hori < 0.0f : sRenderer.flipX);

			// Move player
			rb.MovePosition(Vector2.Lerp(transform.position, endPos, 1.0f));
		}

		// Currently mining
		else
			directionArrow.SetActive(false);
	}


	private void PlaceArrow(Vector3 endPos)
	{
		// Arrow positions ( / 1.5f: Brings arrow closer to the player)
		Vector3 newArrowPos = transform.position + ((endPos - transform.position).normalized / 1.5f);
		Vector3 lookAtDir = endPos - transform.position;
		float angle = Mathf.Atan2(lookAtDir.y, lookAtDir.x) * Mathf.Rad2Deg;

		// If player is not standing still
		directionArrow.SetActive(newArrowPos != transform.position);

		// Set arrow position and rotation
		directionArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		directionArrow.transform.position = newArrowPos;
	}
}