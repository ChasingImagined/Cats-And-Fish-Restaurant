using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBaseDataBundel : MonoBehaviour,IService
{

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(this);
    }
    private void OnDestroy()
    {
        ServiceLocator.Instance.UnRegisterService(this);
    }


   
    [SerializeField] private CentralBank _centralBank;

    [Header("Queues")]

    //�l��maya haz�rurmadki makinleri ve �l��anlar� depolar.
    private QueueBank _readyToWorkQueueBank = new();
    


    public CentralBank GetCentralBank()
    {
        return _centralBank;
    }

    

    public QueueBank GetReadyToWorkQueueBank()
    {
        return _readyToWorkQueueBank;
    }
    




}
