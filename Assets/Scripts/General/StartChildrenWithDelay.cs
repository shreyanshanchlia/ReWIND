using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChildrenWithDelay : MonoBehaviour
{
    public float minDelay, maxDelay;
    public GameObject[] children;
    void OnEnable()
    {
        foreach (var child in children)
		{
            child.SetActive(false);
		}
        StartCoroutine(startChildren());
    }
    IEnumerator startChildren()
	{
		foreach (var child in children)
		{
            child.SetActive(true);
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
		}
	}
}
