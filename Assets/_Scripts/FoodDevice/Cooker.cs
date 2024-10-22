using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooker : FoodDevice
{

    private Bank _bank = null;

    [SerializeField] private Resource _resorge;
    [SerializeField] private double count = 100d;
    public override void Run()
    {
        GameManager gm = ServiceLocator.Instance.GetService<GameManager>();
        _bank = gm?.bank;

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
    }

    private void Start()
    {
        Run();
    }


}
