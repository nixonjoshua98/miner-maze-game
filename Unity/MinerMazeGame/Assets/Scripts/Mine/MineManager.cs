using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
	public static MineManager instance = null;

	/* - - - INSPECTOR - - - - */
	[SerializeField]
	private Vector3 topLeftCorner;

	[Space]
	public string mineFile;

	[Header("Tile GameObjects")]
	public GameObject floorTile;
	public GameObject wallTile;
	public GameObject breakTile;
    public GameObject exitTile;

	[Space, SerializeField]
	GameObject healthPotion;


	/* - - - - PRIVATES - - - - */
	private string[] mineData;


	private void Awake()
	{
		instance = this;

		mineData = (Resources.Load(mineFile) as TextAsset).text.Split('\n');
	}


	private void Start()
	{
		GenerateMine();

		SpawnNewPotion();
	}


	private void GenerateMine()
	{
		Vector3 pos = topLeftCorner;

		GameObject tile;

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

                    case 'E':
                        tile = Instantiate(exitTile, pos, Quaternion.identity, transform);
                        break;

				}

				pos.x += 1;
			}

			pos.y -= 1;
			pos.x = topLeftCorner.x;
		}
	}


	public void SpawnNewPotion()
	{
		StartCoroutine(ISpawnPotion());
	}


	private IEnumerator ISpawnPotion()
	{
		yield return new WaitForSeconds(3.0f);

		List<Transform> Children = new List<Transform>();

		foreach (Transform child in transform)
		{
			if (child.CompareTag("FloorTile"))
				Children.Add(child.transform);
		}

		GameObject potion = Instantiate(healthPotion, Children[Random.Range(0, Children.Count)].position, Quaternion.identity);
	}
}
