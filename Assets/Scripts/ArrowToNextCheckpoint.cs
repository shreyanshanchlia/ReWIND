using Sirenix.OdinInspector;
using UnityEngine;

public class ArrowToNextCheckpoint : MonoBehaviour
{
    #region singleton
    [HideInInspector] public static ArrowToNextCheckpoint instance;
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
    public Transform nextCheckpoint;
    public float angleOffset;
    void Update()
    {
        Vector2 direction = nextCheckpoint.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);
        transform.rotation = rotation;
    }
}
