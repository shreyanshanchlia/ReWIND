using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    public Vector3 initialRotation;
    [Tooltip("Normally should be 1, \njust a speed multiplier")]
    public float rotationSpeed = 1;
    public bool ScaledRotation = false;
    [Header("Randomization")]
    public bool RandomX;
    public bool RandomY, RandomZ;
    public Vector3 minimumRandomization, maximimumRandomization;
    [Header("Ignore if random")]
    public Vector3 RotationPower;

    private Vector3 currentRotation;
    private Vector3 currentRotationPower;
    private Transform toRotate;
    private float dtime = 0.0f;

    void Start()
    {
        currentRotation = initialRotation;
        toRotate = this.transform;
        toRotate.eulerAngles = currentRotation;
        currentRotationPower.x += RandomX ? Random.Range(minimumRandomization.x, maximimumRandomization.x) : RotationPower.x;
        currentRotationPower.y += RandomY ? Random.Range(minimumRandomization.y, maximimumRandomization.y) : RotationPower.y;
        currentRotationPower.z += RandomZ ? Random.Range(minimumRandomization.z, maximimumRandomization.z) : RotationPower.z;
    }

    // Update is called once per frame
    void Update()
    {
        dtime = ScaledRotation ? Time.deltaTime : Time.unscaledDeltaTime;
        toRotate.eulerAngles += currentRotation + currentRotationPower * rotationSpeed * dtime;
    }
}
