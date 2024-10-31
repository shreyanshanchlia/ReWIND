using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeesManager : MonoBehaviour
{
    public bool isSpawning;
    public GameObject beePrefab;
	public GameObject targetToBeDestroyed;

	public FlowerProgression flowerProgression;

	[SerializeField] float radiusOfSpawning;
	[SerializeField] float timeBetweenSpawn;

	uint spawnBeesIndex = 0;
	uint currentRound = 0;
	#region singleton
	[HideInInspector] public static BeesManager instance;
	private void Awake()
	{
		if (instance != null)
		{
			Destroy(this.gameObject);
			return;
		}

		instance = this;
	}
	#endregion
	private void Start()
	{
		SpawnStart();
	}
	public void SpawnStart()
	{
		StopAllCoroutines();
		StartCoroutine(startSpawning());
	}
	public void StopSpawn()
	{
		StopAllCoroutines();
	}
	IEnumerator startSpawning()
	{
		while (true)
		{
			Vector3 position = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), 0);

			position = position.normalized;
			position *= radiusOfSpawning;

			position += targetToBeDestroyed.transform.position;

			GameObject bee = Instantiate(beePrefab, position, Quaternion.identity);
			bee.GetComponent<BeesAI>().targetToDestroy = targetToBeDestroyed.transform;

			if (flowerProgression.stage == 0)
				break;

			yield return new WaitForSeconds(timeBetweenSpawn /
				Mathf.Sqrt(Mathf.Clamp(flowerProgression.stage, 1, 10)) /
				Mathf.Log10((GameManager.instance.CheckpointReachedNumber + 10) *
				(50 - GameManager.instance.fertilizerAmount)));
		}
	}

}
