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
    [SerializeField] private  ResourceBankBase<Money>         _moneyBank;
    [SerializeField] private  ResourceBankBase<FuritMoney>    _furitBank;
    [SerializeField] private  ResourceBankBase<FishMoney>     _fishBank;
    [SerializeField] private  ResourceBankBase<OtherRawMoney> _otherRawBank;


    [Header("Queues")]

    //Çlýþmaya hazýrurmadki makinleri ve çlýþanlarý depolar.
    private QueueBank _readyToWorkQueueBank = new();





    #region ResourceBank Get Funcitons
    public ResourceBankBase<Money> GetMoneyBank()
    {
        if(_moneyBank == null) Debug.LogWarning("Money Bank null");
        return _moneyBank;
    }

    public ResourceBankBase<FuritMoney> GetFuritBank()
    {
        if (_furitBank == null) Debug.LogWarning("Furit Bank null");
        return _furitBank;
    }

    public ResourceBankBase<FishMoney> GetFishBank()
    {
        if (_fishBank == null) Debug.LogWarning("Fish Bank null");
        return _fishBank;
    }

    public ResourceBankBase<OtherRawMoney> GetOtherRawBank()
    {
        if (_otherRawBank == null) Debug.LogWarning("Other Raw Bank null");
        return _otherRawBank ;
    }

    #endregion




    public QueueBank GetReadyToWorkQueueBank()
    {
        return _readyToWorkQueueBank;
    }
    




}
