using UnityEngine;

public class Pollen : MonoBehaviour
{
	public GameObject destroyEffect;
	[SerializeField] GameObject collectEffect;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 9)	//Camera layer = 9
		{
			return;
		}
		if (collision.tag.Equals("Checkpoint"))
		{
			PollenCheckpointCollision(collision.GetComponent<Checkpoint>());
			if (collectEffect) Instantiate(collectEffect, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
		else
			PollenGroundCollision();
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.layer == 9)
		{
			PollenGroundCollision();
		}
	}

	private void PollenCheckpointCollision(Checkpoint checkpoint)
	{
		checkpoint.PollenReached();
	}

	private void PollenGroundCollision()
    {
		transform.parent = null;
		gameObject.GetComponent<SpriteRenderer>().color = Color.red;

		PollenManager.instance.DecrementPollens();

		Destroy(gameObject, 1.0f);

		if (destroyEffect)
			Invoke(nameof(SpawnDestroyEffect), 0.5f);
	}

	private void SpawnDestroyEffect()
	{
		Instantiate(destroyEffect, transform);
	}
}
