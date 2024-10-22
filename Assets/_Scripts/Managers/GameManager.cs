using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour,IService
{
    [SerializeField]public  Bank bank;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(this);
    }
    private void OnDestroy()
    {
        ServiceLocator.Instance.UnRegisterService(this);
    }
}
