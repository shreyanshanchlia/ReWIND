using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sliderText;
    [SerializeField] TMP_InputField inputField;
    public void ValueChange(float value)
	{
        sliderText.text = ((int)value).ToString();
	}

    public void UpdateInputFieldValue(float sliderValue)
	{
		inputField.text = sliderValue.ToString();
	}
    public void UpdateSliderValue(string inputValue)
	{
		try
		{
			GetComponent<Slider>().value = int.Parse(inputValue);
			inputField.text = GetComponent<Slider>().value.ToString();
		}
		catch { }
	}
}
