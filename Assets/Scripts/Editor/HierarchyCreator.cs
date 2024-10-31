using UnityEngine;
using UnityEditor;

public class HierarchyCreator : MonoBehaviour
{
    [MenuItem("GameObject/BrackeysJam2020/Checkpoint", false, 0)]
    public static void InstantiateCheckpoint()
    {
        GameObject checkpoint = Instantiate(Resources.Load<GameObject>("Flower Pot"), Vector3.zero, Quaternion.identity, Selection.activeTransform);
        Checkpoint component = checkpoint.AddComponent<Checkpoint>();
        component.SetIndex();
        component.SetName();
    }
}
