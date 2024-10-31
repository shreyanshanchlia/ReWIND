using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Fertilizer Information")]
    public int fertilizerAmount;
    [SerializeField] int maxFertilizerAllowed;
    [SerializeField] TextMeshProUGUI FertilizerShownText;
    [SerializeField] TextMeshProUGUI CheckpointReachedText;
    [ReadOnly] public int CheckpointReachedNumber = 0;
    [ReadOnly] [SerializeField] bool currentlyIsAtCheckpoint = true;
    [SerializeField] public GameObject pollenTarget;
    [SerializeField] WindInputSliderControl XwindInputSliderControl, YwindInputSliderControl;
    public GameObject GameOverPanel;
    [ReadOnly] public List<Checkpoint> checkpoints;
    #region singleton
    [HideInInspector] public static GameManager instance;
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
        checkpoints = new List<Checkpoint>();
        checkpoints = FindObjectsOfType<Checkpoint>().ToList();

        checkpoints.Sort(Checkpoint.CompareTo);

        checkpoints[CheckpointReachedNumber].GetComponent<BoxCollider2D>().enabled = true;
        
        UpdateCheckpointUI();

        UpdateFertilizerUI();
        CheckState();
    }

    public void FlowerIsReady()
    {
        currentlyIsAtCheckpoint = false;
        checkpoints[CheckpointReachedNumber].GetComponent<BoxCollider2D>().enabled = false;
        pollenTarget.transform.position = checkpoints[CheckpointReachedNumber].transform.position + new Vector3(0, 2.15f, 0);
        PollenManager.instance.GenerateNPollens(25);
        CheckState();
    }
    public void IsGameOver()
	{
        if(!currentlyIsAtCheckpoint)
		{
            GameOverPanel.SetActive(true);
		}
	}
    public void ReachedNextCheckpoint()
    {
        CheckpointReachedNumber++;
        UpdateCheckpointUI();
        currentlyIsAtCheckpoint = true;
        CheckState();
    }
    void UpdateCheckpointUI()
	{
        CheckpointReachedText.text = $"Checkpoint\n     #{CheckpointReachedNumber+1}/{checkpoints.Count}";
	}
    void CheckState()
    {
        if (currentlyIsAtCheckpoint)
        {
            GrowFlowerAtCheckpoint();
        }
        else
        {
            if (CheckpointReachedNumber < checkpoints.Count)
            {
                FlowToNextCheckpoint();
                WindManager.instance.RandomWinds(CheckpointReachedNumber);
            }
			else
			{
                CheckpointReachedText.text = "FLOWERS WILL NOT GO EXTINCT!";

            }
        }
    }

    void GrowFlowerAtCheckpoint()
    {
        XwindInputSliderControl.HideSiders();
        YwindInputSliderControl.HideSiders();
    }
    void FlowToNextCheckpoint()
    {
        XwindInputSliderControl.ShowSiders();
        YwindInputSliderControl.ShowSiders();
        BeesManager.instance.StopSpawn();
        ArrowToNextCheckpoint.instance.nextCheckpoint = checkpoints[CheckpointReachedNumber + 1].gameObject.transform;
        checkpoints[CheckpointReachedNumber + 1].GetComponent<BoxCollider2D>().enabled = true;
    }
    public void Addfertilizer(int amount = 1)
    {
        fertilizerAmount += amount;
        fertilizerAmount = Mathf.Clamp(fertilizerAmount, 0, maxFertilizerAllowed);
        UpdateFertilizerUI();
    }
    public void UseFertilizer(int amount = 5)
    {
        if (fertilizerAmount < 5)
        {
            return;
        }
        fertilizerAmount -= amount;
        UpdateFertilizerUI();
        BeesManager.instance.targetToBeDestroyed = checkpoints[CheckpointReachedNumber].gameObject;
        checkpoints[CheckpointReachedNumber].gameObject.GetComponent<FlowerProgression>().nextStage();
    }
    public void BeeDestroyedFlower()
	{
        BeesManager.instance.targetToBeDestroyed = checkpoints[CheckpointReachedNumber].gameObject;
        checkpoints[CheckpointReachedNumber].gameObject.GetComponent<FlowerProgression>().PreviousStage();
    }
    [ContextMenu("Complete Flower")]
    void UseFertilizerMax()
	{
        while(fertilizerAmount >= 5)
		{
            UseFertilizer(5);
		}
	}
    void UpdateFertilizerUI()
	{
        FertilizerShownText.text = $"Fertilizer: {fertilizerAmount}/{maxFertilizerAllowed}";
	}
}
