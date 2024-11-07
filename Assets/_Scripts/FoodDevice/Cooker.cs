using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cooker : FoodDeviceBase,IQueueable
{

    private CentralBank _centralBank;

    [SerializeField] private List<RawMatrialAndConut> _rawMaterials = new();
    [SerializeField] private float _workingSpeed = 1f;
    [SerializeField] private Meal _meal;

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
        _centralBank       = SceneBaseDataBundel?.GetCentralBank();
       
    }


    public override void Run()
    {
        if (!_isRuning)
        {
            _isRuning = true;
            _startTime = Time.time;
            _sceneBaseDataBundel?.GetReadyToWorkQueueBank()?.DeQueueable<Cooker>();

            if (_centralBank == null)
            {
                Debug.LogWarning(" Central Bnak is null");
                _isRuning = false;
                return;
            }

            List<RawMaterial> notFoundRawMatrials = new List<RawMaterial>();

            for (int i = 0; i < _rawMaterials.Count; i++)
            {

                RawMaterial rawMaterial = _rawMaterials[i].rawMaterial;

                if (_centralBank?.GetAmount(_rawMaterials[i].rawMaterial) < _rawMaterials[i].count)
                {
                    notFoundRawMatrials.Add(rawMaterial);
                }

            }

            if (notFoundRawMatrials.Count > 0)
            {
                Debug.LogWarning($"{notFoundRawMatrials[0]} insufficient RawMatarial");
                _isRuning = false;
                return;
            }

            for (int i = 0; i < _rawMaterials.Count; i++)
            {

                RawMaterial rawMaterial = _rawMaterials[i].rawMaterial;

                bool chack = (bool)(_centralBank?.TakeOut(_rawMaterials[i].rawMaterial, _rawMaterials[i].count));
                Debug.Log(chack);

            }
  
        }
        else
        {
            if (_meal == null) {
                Debug.LogWarning("Null Meal");
                _isRuning = false;
                return;
            }

            _completionAmount = (Time.time - _startTime) * _workingSpeed / _meal.GetPreparationTime();
            if(_completionAmount >=1) {
                _completionAmount = 1;
                _foodReadyEvent?.Invoke();
                _isRuning = false;
                _sceneBaseDataBundel?.GetReadyToWorkQueueBank().EnQueueable(this);
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