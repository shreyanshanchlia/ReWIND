using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsActivationHandler : MonoBehaviour
{
    public bool enableAtStart = false;
    public bool disableAtStart = false;
    public GameObject[] ToHandleObjects;
    public void activateObjects()
    {
        foreach (var item in ToHandleObjects.ToArray())
        {
            if (item != null)
                item.SetActive(true);
        }
        foreach (var @object in ToHandleObjects)
        {
            if (@object != null)
                @object.SetActive(false);
        }
    }
    public void deactivateObjects()
    {
        foreach (var item in ToHandleObjects)
        {
            if (item != null)
                item.SetActive(false);
        }
        foreach (var @object in ToHandleObjects)
        {
            if (@object != null)
                @object.SetActive(true);
        }
    }
    private void Start()
    {
        if(disableAtStart)
        {
            deactivateObjects();
        }
        if(enableAtStart)
        {
            activateObjects();
        }
    }
}
