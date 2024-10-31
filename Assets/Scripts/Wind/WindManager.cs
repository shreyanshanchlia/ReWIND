using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindManager : MonoBehaviour
{
	[SerializeField] GameObject pollens;

	[SerializeField] BuoyancyEffector2D buoyancy;

	[SerializeField] TextMeshProUGUI WindDetailsText;

	[Tooltip("Use for default speeds")]
    [SerializeField] Vector2 WindSpeed;

	[Tooltip("Speed at which Wind Affects increase speed")]
	[SerializeField] float YcontrolSpeed = 10f;

	[ReadOnly] [SerializeField] Vector2 extraWindSpeed;
	#region singleton
	[HideInInspector] public static WindManager instance;
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
		stopWind();
	}
	void UpdateWindUI()
	{
		WindDetailsText.text =
			$"Wind Speed X: {WindSpeed.x} mph + <color=\"green\"> {extraWindSpeed.x} mph </color>\n" +
			$"Wind Speed Y: {WindSpeed.y} mph + <color=\"green\"> {extraWindSpeed.y} mph </color>\n";
	}
	public void ExtraWindHorizontal(float _extraHorizontalSpeed)
	{
		extraWindSpeed.x += _extraHorizontalSpeed;
		buoyancy.flowMagnitude = WindSpeed.x;
		buoyancy.flowVariation = extraWindSpeed.x;
		UpdateWindUI();
	}
	public void RandomWinds(int checkpointIndex = 0)
	{
		float n = checkpointIndex;
		WindSpeed.x = Random.Range(-n, n);
		WindSpeed.y = Random.Range(-n / 2f, n / 2f);
		UpdateWindUI();
	}
	public void stopWind()
	{
		foreach (Transform pollen in pollens.transform)
		{
			pollen.gameObject.GetComponent<WindEffect>().forceVelocity = (extraWindSpeed + WindSpeed) / 10;
			Destroy(pollen.gameObject, 2.5f);
		}
		extraWindSpeed = Vector2.zero;
		WindSpeed = Vector2.zero;
		ExtraWindHorizontal(0);
		pollens.GetComponent<Rigidbody2D>().drag = 1;
	}
	public void StartWindControllability()
	{
		pollens.GetComponent<Rigidbody2D>().drag = 0;
	}
	public void ExtraWindVertical(float _extraVerticalSpeed, float _sliderMaxValue)
	{
		extraWindSpeed.y += _extraVerticalSpeed;
		//extraWindSpeed.y = Mathf.Clamp(extraWindSpeed.y, -WindSpeed.y, _sliderMaxValue);

		//TO DO: TWEEN THIS VALUE TO FIX SUDDEN BURSTS IN SPEED.
		//NOT REQUIRED USING UPDATE INSTEAD
		//tween existing radius -> 0.14f * x * x + 0.587f * x + 0.2678f;


		//pollens.GetComponent<CircleCollider2D>().radius = Mathf.Lerp(pollens.GetComponent<CircleCollider2D>().radius, 0.14f * x * x + 0.587f * x + 0.2678f, 1f);

		UpdateWindUI();
	}
	private void Update()
	{

		float x = (WindSpeed.y + extraWindSpeed.y) / YcontrolSpeed;
		pollens.GetComponent<CircleCollider2D>().radius += x * Time.deltaTime;
		
	}
}
