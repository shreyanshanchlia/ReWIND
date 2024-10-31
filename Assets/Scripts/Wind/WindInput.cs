using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class WindInput : MonoBehaviour
{
    [SerializeField] Slider windInputSliderX;
    [SerializeField] Slider windInputSliderY;
    [SerializeField] KeyCode AcceptWindKeyX;
    [SerializeField] KeyCode AcceptWindKeyY;
    [SerializeField] WindManager windManager;
    [SerializeField] [ReadOnly] Vector2 addWind = Vector2.zero;

	private void Start()
	{
        UpdateWind();
	}
	void Update()
    {
        if (Input.GetKeyDown(AcceptWindKeyX))
        {
            addWind.x = windInputSliderX.value;
            UpdateWind();
        }
        if (Input.GetKeyDown(AcceptWindKeyY))
        {
            addWind.y = windInputSliderY.value;
            UpdateWind();
        }
    }

    void UpdateWind()
    {
        windManager.ExtraWindHorizontal(addWind.x);
        windManager.ExtraWindVertical(addWind.y, windInputSliderY.maxValue);
        addWind = Vector2.zero;
    }
}
