using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	/* - - - - INSPECTOR GAMEOBJECTS - - - - */
	[SerializeField]
	private SpriteRenderer sRenderer;


	/* - - - - INSECOTR PRIVATES - - - - */
	[SerializeField, Range(0.5f, 5.0f)]
	private float speedMulti;

	[SerializeField, Range(1, 10)]
	private int maxMoveDistance;

	
	/* - - - - PRIVATES - - - - */
	private bool isMoving = false;
	private Vector3 moveTarget;


	private void Update()
	{
		if (isMoving)
			Move();
		else
			GetNewTarget();
	}


	private void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, moveTarget, 1.0f * Time.deltaTime * speedMulti);
		CheckPositionToTarget();
	}


	private void GetNewTarget()
	{
		List<Vector3> directions = new List<Vector3>()
		{
			Vector3.up,
			Vector3.right,
			Vector3.down,
			Vector3.left,
		};

		while (directions.Count > 0)
		{
			int i = UnityEngine.Random.Range(0, directions.Count);
			Vector3 target = directions[i];
			directions.RemoveAt(i);

			int distance = 1;

			if (ValidPosition(target, ref distance))
			{
				UpdateSprite(target);
				moveTarget = transform.position + (target * distance);
				isMoving = true;
				break;
			}
		}
	}

	private void UpdateSprite(Vector3 pos)
	{
		if (pos.x > 0f)
			sRenderer.flipX = true;
		else if (pos.x < 0.0f)
			sRenderer.flipX = false;
	}


	private bool ValidPosition(Vector3 pos, ref int distance)
	{
		bool foundValidPos = false;

		for (int i = 1; i <= maxMoveDistance; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position + (pos * i), Vector3.forward);

			if (!(hit.collider.CompareTag("WallTile") || hit.collider.CompareTag("ExitTile") || hit.collider.CompareTag("BreakTile")))
			{
				Debug.DrawLine(transform.position, transform.position + (pos * i), Color.green, 0.5f);
				distance = i;
				foundValidPos = true;
			}
			else
				break;
		}

		return foundValidPos;
	}


	private void CheckPositionToTarget()
	{
		if (transform.position == moveTarget)
		{
			isMoving = false;
		}
	}
}
