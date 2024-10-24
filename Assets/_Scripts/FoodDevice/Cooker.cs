using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Events;

public class Cooker : FoodDevice,IQueueable
{
    
    private ResourceBankBase<Money>         _moneyBank         = null;
    private ResourceBankBase<FishMoney>     _fishMoneyBank     = null;
    private ResourceBankBase<FuritMoney>    _furitMoneyBank    = null;
    private ResourceBankBase<OtherRawMoney> _otherRawMoneyBank = null;

    [SerializeField] private List<RawMatrialAndConut> _rawMaterials = new();
    [SerializeField] private float _workingSpeed = 1f;
    [SerializeField] private Item _item;

    private SceneBaseDataBundel _sceneBaseDataBundel;

    private bool _isRuning = false;
    private float _startTime =0f;
    private float _completionAmount=0f;


    public UnityEvent _foodReadyEvent;

    private void Awake()
    {
        _sceneBaseDataBundel = ServiceLocator.Instance.GetService<SceneBaseDataBundel>();
        _sceneBaseDataBundel?.GetReadyToWorkQueueBank()?.EnQueueable(this);
    }

    private void Start()
    {
        SceneBaseDataBundel SceneBaseDataBundel = ServiceLocator.Instance.GetService<SceneBaseDataBundel>();
        _moneyBank         = SceneBaseDataBundel?.GetMoneyBank();
        _fishMoneyBank     = SceneBaseDataBundel?.GetFishBank();
        _furitMoneyBank    = SceneBaseDataBundel?.GetFuritBank();
        _otherRawMoneyBank = SceneBaseDataBundel?.GetOtherRawBank();

       


    }

    


    public override void Run()
    {
        if (!_isRuning)
        {
            _isRuning = true;
            _startTime = Time.time;
            _sceneBaseDataBundel?.GetReadyToWorkQueueBank()?.DeQueueable<Cooker>();

            if (_fishMoneyBank == null || _moneyBank == null || _furitMoneyBank == null || _otherRawMoneyBank == null)
            {
                Debug.LogWarning("Banks are null");
                _isRuning = false;
                return;
            }

            List<RawMaterial> notFoundRawMatrials = new List<RawMaterial>();

            for (int i = 0; i < _rawMaterials.Count; i++)
            {

                RawMaterial rawMaterial = _rawMaterials[i].rawMaterial;
                if (rawMaterial is FishMoney)
                {
                    if (_fishMoneyBank.GetResorgeCount(_rawMaterials[i].rawMaterial) < _rawMaterials[i].count)
                    {
                        notFoundRawMatrials.Add(rawMaterial);
                    }

                }
                else if (rawMaterial is FuritMoney)
                {
                    if (_furitMoneyBank.GetResorgeCount(_rawMaterials[i].rawMaterial) < _rawMaterials[i].count)
                    {
                        notFoundRawMatrials.Add(rawMaterial);
                    }

                }
                else if (rawMaterial is OtherRawMoney)
                {
                    if (_otherRawMoneyBank.GetResorgeCount(_rawMaterials[i].rawMaterial) < _rawMaterials[i].count)
                    {
                        notFoundRawMatrials.Add(rawMaterial);
                    }
                }

            }

            if (notFoundRawMatrials.Count > 0)
            {
                Debug.LogWarning("insufficient RawMatarial");
                _isRuning = false;
                return;
            }

            for (int i = 0; i < _rawMaterials.Count; i++)
            {

                RawMaterial rawMaterial = _rawMaterials[i].rawMaterial;
                if (rawMaterial is FishMoney)
                {
                    bool chack = (bool)(_fishMoneyBank?.ExtractResource(_rawMaterials[i].rawMaterial, _rawMaterials[i].count));
                    Debug.Log(chack);
                }
                else if (rawMaterial is FuritMoney)
                {
                    bool chack = (bool)(_furitMoneyBank?.ExtractResource(_rawMaterials[i].rawMaterial, _rawMaterials[i].count));
                    Debug.Log(chack);
                }
                else if (rawMaterial is OtherRawMoney)
                {
                    bool chack = (bool)(_otherRawMoneyBank?.ExtractResource(_rawMaterials[i].rawMaterial, _rawMaterials[i].count));
                    Debug.Log(chack);
                }

            }

            _foodReadyEvent?.Invoke();
        }
        else
        {
            if (_item == null) {
                Debug.LogWarning("Null item");
                _isRuning = false;
                return;
            }

            _completionAmount = (Time.time - _startTime) * _workingSpeed / _item.GetPreparationTime();
            if(_completionAmount >=1) {
                _completionAmount = 1;
                // ToDo: food ready event  invoke 
            }



        }

       


    }
        
    
}

[System.Serializable]
public class RawMatrialAndConut
{
    public RawMaterial rawMaterial;
    public double count;
}