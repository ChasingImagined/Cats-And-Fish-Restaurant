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


    [Header("Banks")]
    [SerializeField] private  ResorceBank _moneyBank;
    [SerializeField] private  ResorceBank _furitBank;
    [SerializeField] private  ResorceBank _fishBank;



    [Header("Queues")]

    private QueueBank _queueBank = new();





    #region ResourceBank Get Funcitons
    public ResorceBank GetMoneyBank()
    {
        if(_moneyBank == null) Debug.LogWarning("Money Bank null");
        return _moneyBank;
    }

    public ResorceBank GetFuritBank()
    {
        if (_furitBank == null) Debug.LogWarning("Furit Bank null");
        return _furitBank;
    }

    public ResorceBank GetFishBank()
    {
        if (_fishBank == null) Debug.LogWarning("Fish Bank null");
        return _fishBank;
    }

    #endregion
    
    


    public QueueBank GetQueueBank()
    {
        return _queueBank;
    }
    




}
