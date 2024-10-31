using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeesAI : MonoBehaviour
{
    public GameObject destroyEffect;

    [SerializeField] float minStartingSpeed, maxStartingSpeed;
    [SerializeField] float minLaterSpeed, maxLaterSpeed;
    [SerializeField] float minStopDuration, maxStopDuration;
    [SerializeField] float minStopAfterDuration, maxStopAfterDuration;

    float startingSpeed, LaterSpeed, stopDuration, stopAfterDuration;

    public Transform targetToDestroy;

    Vector3 target;
    public Vector2 minTargetOffset, maxTargetOffset;
    
    enum MovementState
	{
        starting, stop, later
	};

    [ReadOnly] [SerializeField] MovementState movementState;
    void Start()
    {
        target = targetToDestroy.transform.position;
        target += new Vector3(Random.Range(minTargetOffset.x, maxTargetOffset.x), 
            Random.Range(minTargetOffset.y, maxTargetOffset.y), 0);

        startingSpeed = Random.Range(minStartingSpeed, maxStartingSpeed);
        LaterSpeed = Random.Range(minLaterSpeed, maxLaterSpeed);
        stopDuration = Random.Range(minStopDuration, maxStopDuration);
        stopAfterDuration = Random.Range(minStopAfterDuration, maxStopAfterDuration);
        if(this.transform.position.x < target.x)
		{
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
		}
    }

    void Update()
    {
        if (movementState == MovementState.starting)
        {
            this.transform.position = 
                Vector3.MoveTowards(this.transform.position, target, startingSpeed * Time.deltaTime);
            stopAfterDuration -= Time.deltaTime;
        }
        else if(movementState == MovementState.stop)
		{
            this.transform.position =
                Vector3.MoveTowards(this.transform.position, target, startingSpeed * Time.deltaTime / 10);
            stopDuration -= Time.deltaTime;
		}
		else
		{
            this.transform.position =
                Vector3.MoveTowards(this.transform.position, target, LaterSpeed * Time.deltaTime);
        }
        if ((this.transform.position - target).sqrMagnitude < 0.5f)
		{
            GameManager.instance.BeeDestroyedFlower();
            Instantiate(destroyEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (stopDuration <= 0f)
		{
            movementState = MovementState.later;
		}
        else if(stopAfterDuration <= 0f)
		{
            movementState = MovementState.stop;
		}
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.collider.CompareTag("Checkpoint"))
		{
            GameManager.instance.BeeDestroyedFlower();
        }
        Instantiate(destroyEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
	}
}
