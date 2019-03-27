using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
	/* - - - - INSPECTOR GAMEOBJECTS - - - - */
	[SerializeField]
	private SpriteRenderer sRenderer;


    [SerializeField]
    private Animator anim;


    /* - - - - INSPECTOR PRIVATES - - - - */
    [SerializeField, Range(0.5f, 5.0f)]
	private float speedMulti;

	[SerializeField, Range(1, 10)]
	private int maxMoveDistance;

	[SerializeField, Range(1, 16)]
	int visionDistance;

	
	/* - - - - PRIVATES - - - - */
	private bool isMoving = false;
	private Vector3 moveTarget;
    bool frozen = false;
	bool isTouchingPlayer = false;
    float frozenTimer = 5.0f;


	private void Update()
	{
        if (!isTouchingPlayer && frozen)
        {
            frozenTimer -= Time.deltaTime;
            if (frozenTimer <= 0.0f)
            {
                sRenderer.color = Color.white;
                frozen = false;
            }
        }

        else if (isMoving)
            Move();

        else if (!isMoving)
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

			// Enemy vision
			if (IsFacingPlayer(ref moveTarget))
			{
				UpdateSprite(moveTarget - transform.position);
				isMoving = true;
				break;
			}


			// Random movement
			else if (ValidPosition(target, ref distance))
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


	private bool ValidPosition(Vector3 dir, ref int distance)
	{
		bool foundValidPos = false;

		for (int i = 1; i <= maxMoveDistance; i++)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position + (dir * i), Vector3.forward, 1, LayerMask.GetMask("WallTiles", "FloorTiles", "BreakableTiles"));

            if (!hit.collider) return false;

			if (hit.collider.CompareTag("FloorTile"))
			{
				Debug.DrawLine(transform.position, transform.position + (dir * i), Color.green, 0.5f);
				distance = i;
				foundValidPos = true;
			}
			else
				break;
		}

		return foundValidPos;
	}


	private bool IsFacingPlayer(ref Vector3 playerPos)
	{
		List<Vector3> directions = new List<Vector3>()
		{
			Vector3.up,
			Vector3.right,
			Vector3.down,
			Vector3.left,
		};

		foreach (Vector3 dir in directions)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, visionDistance, LayerMask.GetMask("Player", "BreakableTiles", "WallTiles"));

			if (hit.collider && hit.collider.CompareTag("Player"))
			{
				Vector3 hitPos = hit.collider.transform.position;

				if (dir == Vector3.up || dir == Vector3.down)
					playerPos = new Vector3(transform.position.x, hit.collider.transform.position.y, transform.position.z);
				else if (dir == Vector3.left || dir == Vector3.right)
					playerPos = new Vector3(hit.collider.transform.position.x, transform.position.y, transform.position.z);

				Debug.DrawLine(transform.position, playerPos, Color.red, 0.5f);

				return true;
			}
		}

		return false;
	}


	private void CheckPositionToTarget()
	{
		if (transform.position == moveTarget)
		{
			isMoving = false;
		}
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!frozen && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().currentHealth -= 10;
            anim.SetBool("EnemyAttacking", true);
			isTouchingPlayer = true;
			frozenTimer = 5.0f;
			frozen = true;

		}
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("EnemyAttacking", false);
            sRenderer.color = Color.blue;
			isTouchingPlayer = false;

		}
    }
}
