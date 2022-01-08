using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{

	[Header("Object To Spawn")]
	public GameObject[] objectSpawnList;

	[Header("Spawn Y-Position")]
	public float[] spawnPositionList;

	private GameObject ground;

	// Use this for initialization
	private void Start()
	{
		assignObjectVariables();
		StartCoroutine(spawnAtRandomLocation());
	}

	private void assignObjectVariables()
	{
		ground = GameObject.Find("Ground");
	}
	
	public Vector3 getRandomPositionOnPlane(int randomValue)
	{
		Mesh planeMesh = ground.GetComponent<MeshFilter>().mesh;
		Bounds bounds = planeMesh.bounds;

		float minX = ground.transform.position.x - ground.transform.localScale.x * bounds.size.x * 0.5f;
		float minZ = ground.transform.position.z - ground.transform.localScale.z * bounds.size.z * 0.5f;

		Vector3 vector = new Vector3(Random.Range(minX, -minX), spawnPositionList[randomValue], Random.Range(minZ, -minZ));

		return vector;
	}

	private IEnumerator spawnAtRandomLocation()
	{
		while (true)
		{
			int randomValue = Random.Range(0, objectSpawnList.Length);
			GameObject go = Instantiate(objectSpawnList[randomValue]) as GameObject;
			go.transform.parent = gameObject.transform;

			go.transform.position = getRandomPositionOnPlane(randomValue);

			yield return new WaitForSeconds(3.0f);
		}
	}
}
