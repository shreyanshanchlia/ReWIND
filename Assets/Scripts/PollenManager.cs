using Sirenix.OdinInspector;
using UnityEngine;

public class PollenManager : MonoBehaviour
{
    public GameObject pollenGroup;
    [SerializeField] Vector2 instantiatingRandomness;
    [SerializeField] int startWithPollenCount = 20;
    [SerializeField] Transform pollenTargetTransform;
	[SerializeField] GameObject pollenGameObject;
    [ReadOnly] [SerializeField] private int pollenCount;
	#region Singleton
	[HideInInspector] public static PollenManager instance;
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

    void Start()
	{
		//GeneratePollens();
		Destroy(pollenGroup.transform.GetChild(0).gameObject);
		nilPollens();
	}

    void nilPollens()
	{
		GenerateNPollens(0);
	}
	public void GenerateNPollens(int _SpawnpollenCount = 0)
	{
		for (int i = 0; i < _SpawnpollenCount; i++)
		{
			Vector3 instantiatingPosition = Vector3.zero;
			instantiatingPosition.x = pollenTargetTransform.position.x + Random.Range(-instantiatingRandomness.x, instantiatingRandomness.x);
			instantiatingPosition.y = pollenTargetTransform.position.y + Random.Range(-instantiatingRandomness.y, instantiatingRandomness.y);
			Instantiate(pollenGameObject, instantiatingPosition, Quaternion.identity, pollenGroup.transform);
		}
		pollenCount = pollenGroup.transform.childCount - 1;
	}
	public void GeneratePollens()
	{
		for (int i = 0; i < startWithPollenCount; i++)
		{
			Vector3 instantiatingPosition = Vector3.zero;
			instantiatingPosition.x = pollenTargetTransform.position.x + Random.Range(-instantiatingRandomness.x, instantiatingRandomness.x);
			instantiatingPosition.y = pollenTargetTransform.position.y + Random.Range(-instantiatingRandomness.y, instantiatingRandomness.y);
			Instantiate(pollenGameObject, instantiatingPosition, Quaternion.identity, pollenGroup.transform);
		}
		Destroy(pollenGameObject);
		pollenCount = pollenGroup.transform.childCount - 1;
	}

	public void DecrementPollens()
	{
		--pollenCount;
		if(pollenCount<=0)
		{
			if(pollenGroup.transform.childCount<=0)
				GameManager.instance.IsGameOver();
		}
	}

	public GameObject GetPollenGroup() => pollenGroup;
}
