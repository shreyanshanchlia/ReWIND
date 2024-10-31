using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using BayatGames.SaveGameFree;

public class sceneManager : MonoBehaviour
{
	public Animator sceneTransitionAnim;
	private void Start()
	{
		SaveGame.Save<int>("sceneOffset", 3);
		if(sceneTransitionAnim != null)
		{
			sceneTransitionAnim.gameObject.SetActive(true);
		}
	}
	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void ContinueScene()
	{
		int maxLevelReached = SaveGame.Load<int>("maxDayIndex", 2);
		LoadScene(maxLevelReached + 1);
	}
	public void LoadScene(int index)
	{
		if (sceneTransitionAnim == null)
		{
			SceneManager.LoadScene(index);
		}
		else
		{
			StartCoroutine(LoadNextScene(index));
		}
	}
	public void LoadSceneNoAnim(int index)
	{
		SceneManager.LoadScene(index);
	}
	public void LoadNextLevel()
	{
		if (sceneTransitionAnim == null)
		{
			LoadSceneNoAnim(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
			StartCoroutine(LoadNextScene());
		}
	}
	IEnumerator LoadNextScene()
	{
		sceneTransitionAnim.SetTrigger("Start");
		yield return new WaitForSeconds(1.0f);
		LoadSceneNoAnim(SceneManager.GetActiveScene().buildIndex + 1);
	}
	IEnumerator LoadNextScene(int buildIndex)
	{
		sceneTransitionAnim.SetTrigger("Start");
		yield return new WaitForSeconds(1.0f);
		LoadSceneNoAnim(buildIndex);
	}
	public void Exit()
	{
		Application.Quit();
	}
}
