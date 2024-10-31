using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour
{
    [Tooltip("Use to show the maximum displacement that can be caused by the wind")]
    public Vector2 displacementEffect;
    public Vector2 forceVelocity = Vector2.zero;

    Vector3 addedDisplacement;

    void Update()
    {
        addedDisplacement.x = Random.Range(-displacementEffect.x, displacementEffect.x) + forceVelocity.x * Time.deltaTime;
        addedDisplacement.y = Random.Range(-displacementEffect.y, displacementEffect.y) + forceVelocity.y * Time.deltaTime;
        this.transform.position += addedDisplacement;
    }
}
