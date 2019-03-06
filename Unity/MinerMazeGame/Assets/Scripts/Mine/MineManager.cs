using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
	public Vector3 topLeftCorner;

	[Space]

	public string mineFile;

	[Space]

	public GameObject floorTile;
	public GameObject wallTile;
	public GameObject breakTile;

	/* - - - - - - - - - - - - - - - - - - */

	private string[] mineData;


	private void Awake()
	{
		mineData = (Resources.Load(mineFile) as TextAsset).text.Split('\n');
	}


	private void Start()
	{
		GenerateMine();
	}


	private void GenerateMine()
	{
		Vector3 pos = topLeftCorner;

		GameObject tile = new GameObject();

		// Line by line
		for (int i = 0; i < mineData.Length; i++)
		{
			// Char by char
			foreach (char c in mineData[i])
			{
				switch (c)
				{
					case 'F':
						tile = Instantiate(floorTile, pos, Quaternion.identity, transform);
						break;

					case 'W':
						tile = Instantiate(wallTile, pos, Quaternion.identity, transform);
						break;

					case 'B':
						tile = Instantiate(breakTile, pos, Quaternion.identity, transform);
						break;

				}

				pos.x += 1;
			}

			pos.y -= 1;
			pos.x = topLeftCorner.x;
		}
	}

}
