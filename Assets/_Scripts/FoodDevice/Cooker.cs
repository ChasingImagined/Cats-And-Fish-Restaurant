using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class Cooker : FoodDevice,IQueueable
{
    
    private ResorceBank _bank = null;



    [SerializeField] private Resource _resorge;
    [SerializeField] private double count = 100d;

    private SceneBaseDataBundel _sceneBaseDataBundel;
    private void Awake()
    {
        _sceneBaseDataBundel = ServiceLocator.Instance.GetService<SceneBaseDataBundel>();
    }

    private void Start()
    {

        _sceneBaseDataBundel?.GetQueueBank()?.EnQueueable(this);
        Run();


    }

    


    public override void Run()
    {
        _sceneBaseDataBundel?.GetQueueBank()?.DeQueueable<Cooker>();
        SceneBaseDataBundel gm = ServiceLocator.Instance.GetService<SceneBaseDataBundel>();
        _bank = gm?.GetMoneyBank();

        if (_bank != null)
        {
            _bank.ExcartResorge(_resorge, count);
            
        }
        else
        {
            if(gm != null)
            {
                Debug.LogWarning("GameManager null");
            }
            Debug.LogWarning("bank null");
        }
        _sceneBaseDataBundel?.GetQueueBank()?.EnQueueable(this);
    }

    


}
