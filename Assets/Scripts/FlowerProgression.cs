using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerProgression : MonoBehaviour
{
    [ReadOnly] public int stage;
	[SerializeField] SpriteRenderer flowerSpriteRenderer;
	[SerializeField] string resourcesLink = "FlowerProgression/";
	
	[ContextMenu("ProgressUp")]
    public void nextStage()
	{
		stage++;
		if (stage < 10)
		{
			flowerSpriteRenderer.sprite = Resources.Load<Sprite>(resourcesLink + stage.ToString("d2"));
			BeesManager.instance.SpawnStart();
		}
		else if (stage == 10)
		{
			flowerSpriteRenderer.sprite = Resources.Load<Sprite>(resourcesLink + stage.ToString("d2"));
			BeesManager.instance.StopSpawn();
			GameManager.instance.FlowerIsReady();
		}
	}
	public void PreviousStage()
	{
		stage--;
		stage = Mathf.Clamp(stage, 1, 10);
		if (stage < 10)
		{
			flowerSpriteRenderer.sprite = Resources.Load<Sprite>(resourcesLink + stage.ToString("d2"));
		}
		else if (stage == 10)
		{
			flowerSpriteRenderer.sprite = Resources.Load<Sprite>(resourcesLink + stage.ToString("d2"));
			BeesManager.instance.StopSpawn();
			GameManager.instance.FlowerIsReady();
		}
	}
}
