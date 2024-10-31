using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int index;
    public bool isReached = false;
    public GameObject levelUpSound;
    private static int checkpointCounter = 0;

    public void SetName() => name = "Checkpoint #" + index;
    public void SetIndex() => index = ++checkpointCounter;

    private void OnValidate()
    {
        SetName();
    }

    public void PollenReached()
	{
        GameManager.instance.Addfertilizer();
        if (!isReached)
        {
            WindManager.instance.stopWind();
            GameManager.instance.ReachedNextCheckpoint();
            isReached = true;
            Instantiate(levelUpSound);
        }
    }

    public static int CompareTo(Checkpoint c1, Checkpoint c2)
    {
        return c1.index.CompareTo(c2.index);
    }

}
