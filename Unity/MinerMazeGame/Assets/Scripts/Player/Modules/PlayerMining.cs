using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMining : MonoBehaviour
{
	public GameObject mineRoot;
	public GameObject floorTile;
	public GameObject wallTile;

	public Text blockCountTxt;

	private int blockCount = 0;

	public void Awake()
	{
		blockCountTxt.text = "BLOCKS LEFT: " + blockCount;
	}


	public void Mine()
	{
		GameObject tile = null;

		if (GetTile(ref tile))
		{
			TileHealth tileHP = tile.GetComponent<TileHealth>();

			tileHP.TakeDamage(Time.deltaTime);

			if (tileHP.IsDestroyed())
			{
				blockCount++;
				Instantiate(floorTile, tile.transform.position, Quaternion.identity, mineRoot.transform);
				Destroy(tile);

				blockCountTxt.text = "BLOCKS LEFT: " + blockCount;
			}
		}
	}


	public void PlaceBlock()
	{
		if (blockCount == 0)
			return;

		Vector3 pos = Vector3.zero;

		if (GetTilePlacementPos(ref pos))
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position + pos, Vector3.forward, 1, LayerMask.GetMask("FloorTiles"));

			Debug.DrawLine(transform.position, transform.position + pos);

			if (hit.collider && hit.collider.CompareTag("FloorTile"))
			{
				blockCount--;
				Instantiate(wallTile, hit.collider.gameObject.transform.position, Quaternion.identity, mineRoot.transform);
				Destroy(hit.collider.gameObject);

				blockCountTxt.text = "BLOCKS LEFT: " + blockCount;
			}
		}
	
	}

	private bool GetTile(ref GameObject tile)
	{
		List<Vector3> directions = new List<Vector3>()
		{
			Vector3.up,
			Vector3.right,
			Vector3.down,
			Vector3.left,
		};

		GameObject breakTile;

		foreach (Vector3 v in directions)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, v, 1, LayerMask.GetMask("BreakableTiles"));

			Debug.DrawLine(transform.position, transform.position + v);

			if (hit.collider && hit.collider.CompareTag("BreakTile"))
			{
				tile = hit.collider.gameObject;
				return true;
			}
		}

		return false;
	}


	private bool GetTilePlacementPos(ref Vector3 pos)
	{
		KeyCode[] keys = { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow };

		foreach (KeyCode k in keys)
		{
			if (Input.GetKeyDown(k))
			{
				Vector3 dir = Vector3.zero;

				switch (k)
				{
					case KeyCode.LeftArrow:
						dir = Vector3.left;
						break;

					case KeyCode.RightArrow:
						dir = Vector3.right;
						break;

					case KeyCode.UpArrow:
						dir = Vector3.up;
						break;

					case KeyCode.DownArrow:
						dir = Vector3.down;
						break;
				}

				pos = dir;
				return true;
			}
		}
		return false;
	}
}
