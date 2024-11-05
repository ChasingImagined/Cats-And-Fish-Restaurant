using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeBase : MonoBehaviour,IQueueable
{

    private SceneBaseDataBundel _sceneBaseDataBundel;
    private Cooker _cooker;
    protected void Awake()
    {
       
    }
    protected void Start()
    {
        _sceneBaseDataBundel = ServiceLocator.Instance.GetService<SceneBaseDataBundel>();
        _sceneBaseDataBundel?.GetReadyToWorkQueueBank()?.EnQueueable(this);
        _cooker = _sceneBaseDataBundel?.GetReadyToWorkQueueBank()?.DeQueueable<Cooker>();
        _cooker?._foodReadyEvent.AddListener(FoodReady);
    }

   
    protected void Update()
    {
        Work();
    }

    protected void Work()
    {
        _cooker?.Run();
    }

    private void FoodReady()
    {
        Debug.Log("Food ready");
        _cooker = null;
    }
}
