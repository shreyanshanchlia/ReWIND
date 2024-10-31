using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WindInputSliderControl : MonoBehaviour
{
    [SerializeField] private Slider windInputSlider;
	[SerializeField] float initialValue;
	[Tooltip("Time until slider reaches max value starting from min value")]
	[SerializeField] [Range(0, 1.5f)] float speedOfWindSlider = 0.5f;
    [SerializeField] LeanTweenType sliderUpdateMethod;
	[ReadOnly] [SerializeField] bool goingUp;

	float totalVariance;

    void Start()
    {
		windInputSlider.value = initialValue;
		totalVariance = windInputSlider.maxValue - windInputSlider.minValue;
#if !UNITY_EDITOR
		TweenWindSpeedSliderToMax();
#endif
	}

	public void HideSiders()
	{
		windInputSlider.gameObject.SetActive(false);
	}
	public void ShowSiders()
	{
		windInputSlider.gameObject.SetActive(true);
	}
	private void Update()
	{
#if UNITY_EDITOR
		if (windInputSlider.value == windInputSlider.maxValue)
		{
			goingUp = false;
			totalVariance = windInputSlider.maxValue - windInputSlider.minValue;
		}
		if (windInputSlider.value == windInputSlider.minValue)
		{
			goingUp = true;
			totalVariance = windInputSlider.maxValue - windInputSlider.minValue;
		}
		if (goingUp)
		{
			windInputSlider.value += totalVariance / speedOfWindSlider * Time.deltaTime;
		}
		else
		{
			windInputSlider.value -= totalVariance / speedOfWindSlider * Time.deltaTime;
		}
#endif
	}

	//TO DO: MAKE THIS WORK WITH TWEENING

	void TweenWindSpeedSliderToMax()
	{
		LeanTween.value(gameObject, windInputSlider.minValue, windInputSlider.maxValue, 0.5f).setEase(sliderUpdateMethod)
			.setOnUpdate((float val) =>
			{
				windInputSlider.value = val;
			}).setLoopPingPong();
		
	}
}
