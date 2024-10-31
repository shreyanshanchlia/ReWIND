using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickDestroyBee : MonoBehaviour
{
	[SerializeField] GameObject beeDestroyEffect;
	Camera cam;
	public float TimeTillClicked = 0.05f;
	public Vector2 BoxCastSize = Vector2.one;
	bool clicked = false;
	private void OnMouseDown()
	{
		if (beeDestroyEffect) Instantiate(beeDestroyEffect, this.transform.position, Quaternion.identity);
		GameManager.instance.Addfertilizer(1);
		Destroy(this.gameObject);
	}
	private void OnMouseOver()
	{
		if (clicked)
		{
			if (beeDestroyEffect) Instantiate(beeDestroyEffect, this.transform.position, Quaternion.identity);
			GameManager.instance.Addfertilizer(1);
			Destroy(this.gameObject);
		}
	}
	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			var collisions = Physics2D.OverlapCircle(this.transform.position, 0.1f);
				if (collisions.CompareTag("Bee"))
				{
					if (collisions.gameObject == this.gameObject)
					{
						if (beeDestroyEffect) Instantiate(beeDestroyEffect, this.transform.position, Quaternion.identity);
						GameManager.instance.Addfertilizer(1);
						Destroy(this.gameObject);
					}
				}
			
			clicked = true;
			TimeTillClicked = 0.05f;
		}
		if(clicked)
		{
			TimeTillClicked -= Time.deltaTime;
			if(TimeTillClicked>=0)
			{
				clicked = true;
			}
			else
			{
				clicked = false;
			}
		}
	}
	//private void Start()
	//{
	//	cam = Camera.main;
	//}
	//private void Update()
	//{
	//	if (Input.GetMouseButtonDown(0))
	//	{
	//		RaycastHit2D boxed = Physics2D.BoxCast(Input.mousePosition, BoxCastSize, 0, Vector2.down, Mathf.Infinity, 14);
	//		if (boxed == true)
	//		{
	//			if (beeDestroyEffect) Instantiate(beeDestroyEffect, this.transform.position, Quaternion.identity);
	//			GameManager.instance.Addfertilizer(1);
	//			Destroy(this.gameObject);
	//		}
	//	}
	//	//if (Input.GetMouseButtonDown(0))
	//	//{
	//	//	RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

	//	//	if (hit.collider != null) { if (hit.collider.gameObject == gameObject) Destroy(gameObject); }
	//	//}

	//}
}
