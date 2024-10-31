using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public Image blackScreen;
    public float fadeCoefficient = 1f;
    public List<GameObject> MainMenuElements, SettingsMenuElements, CreditsElements;

    private delegate void SceneChangerDelegate();

    private void Start() {
        SetupAllElements();
        ScreenFadeIn();
    }

    private void SetupAllElements()
    {
        AddChildrenOfElementsToList(MainMenuElements);
        AddChildrenOfElementsToList(SettingsMenuElements);
        AddChildrenOfElementsToList(CreditsElements);

        TurnOffElements(SettingsMenuElements);
        TurnOffElements(CreditsElements);
        TurnOnElements(MainMenuElements);
    }

    private void AddChildrenOfElementsToList(List<GameObject> elementsToFade)
    {
        if (elementsToFade == null) return;

        foreach (GameObject element in elementsToFade.ToList())
            foreach (Transform childElement in element.transform)
                elementsToFade.Add(childElement.gameObject);
    }

    private void TurnOnElements(List<GameObject> elements)
    {
        foreach (GameObject element in elements)
            element.SetActive(true);
    }

    private void TurnOffElements(List<GameObject> elements)
    {
        foreach (GameObject element in elements)
            element.SetActive(false);
    }

    public void PlayGame()
    {
        ElementsFadeOut(MainMenuElements, null);
        ScreenFadeOut();
    }

    public void MainToSettings() => ElementsFadeOut(MainMenuElements, SettingsMenuElements);

    public void SettingsToMain() => ElementsFadeOut(SettingsMenuElements, MainMenuElements);

    public void MainToCredits() => ElementsFadeOut(MainMenuElements, CreditsElements);

    public void CreditsToMain() => ElementsFadeOut(CreditsElements, MainMenuElements);

    public void QuitGame() => Application.Quit();

    private void ScreenFadeOut()
    {
        LeanTween.value(blackScreen.gameObject, 0, 1, fadeCoefficient)
        .setOnStart(() =>
        {
            blackScreen.gameObject.SetActive(true);
        })
        .setOnUpdate((float value) =>
        {
            blackScreen.color = new Color(0, 0, 0, value);
        })
        .setOnComplete(() =>
        {
            SceneManager.LoadScene(1);
        });
    }
    
    private void ScreenFadeIn()
    {
        LeanTween.value(blackScreen.gameObject, 1, 0, fadeCoefficient)
        .setOnStart(() =>
        {
            blackScreen.gameObject.SetActive(true);
        })
        .setOnUpdate((float value) =>
        {
            blackScreen.color = new Color(0, 0, 0, value);
        })
        .setOnComplete(() =>
        {
            blackScreen.gameObject.SetActive(false);
        });
    }

    private void ElementsFadeOut(List<GameObject> elementsToFadeOut, List<GameObject> elementsToFadeInAfter)
    {
        foreach(GameObject element in elementsToFadeOut)
        {
            Image elementImage;
            if (element.TryGetComponent<Image>(out elementImage))
            {
                LeanTween.value(element, 1, 0, fadeCoefficient)
                .setOnUpdate((float value) =>
                {
                    elementImage.color = new Color(elementImage.color.r, elementImage.color.g, elementImage.color.b, value);
                })
                .setOnComplete(() =>
                {
                    element.SetActive(false);
                });
            }

            StartCoroutine(ElementsFadeIn(elementsToFadeInAfter));
        }
    }

    private IEnumerator ElementsFadeIn(List<GameObject> elementsToFade)
    {
        yield return new WaitForSeconds(1f);
        if (elementsToFade != null)
        {
            foreach (GameObject element in elementsToFade)
            {
                Image elementImage;
                if (element.TryGetComponent<Image>(out elementImage))
                {
                    LeanTween.value(element, 0, 1, fadeCoefficient)
                    .setOnStart(() =>
                    {
                        element.SetActive(true);
                    })
                    .setOnUpdate((float value) =>
                    {
                        elementImage.color = new Color(elementImage.color.r, elementImage.color.g, elementImage.color.b, value);
                    });
                }
            }
        }
    }
}
