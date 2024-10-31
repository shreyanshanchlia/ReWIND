using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Transform pollenGroup;
    private List<Checkpoint> checkpoints;

    private void Start()
    {
        InitializePollenGroup();
        InitializeCheckpoints();
    }

    private void InitializePollenGroup() => pollenGroup = PollenManager.instance.GetPollenGroup().transform;

    private void InitializeCheckpoints()
    {
        checkpoints = FindObjectsOfType<Checkpoint>().ToList();
        checkpoints.Sort();
    }

    public void RespawnPollenGroup() => pollenGroup.position = GetLatestCheckpointPosition();

    private int GetLatestCheckpointIndex() => GetLatestCheckpoint().index;

    private Vector3 GetLatestCheckpointPosition() => GetLatestCheckpoint().transform.position;

    private Checkpoint GetLatestCheckpoint()
    {
        for (int i = checkpoints.Count - 1; i > 0; --i)
            if (checkpoints[i].isReached)
                return checkpoints[i];

        return checkpoints[0];
    }

    public void SaveGame() => PlayerPrefs.SetInt("LatestCheckpoint", GetLatestCheckpointIndex());
    public void LoadGame()
    {
        SetLatestCheckpointAsReached();
        RespawnPollenGroup();
    }

    private void SetLatestCheckpointAsReached()
    {
        int latestIndex = PlayerPrefs.GetInt("LatestCheckpoint", 1);

        foreach (Checkpoint checkpoint in checkpoints)
            if (checkpoint.index == latestIndex)
                checkpoint.isReached = true;
    }
}
