using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class FishRefranceBank : RefranceBankBase<AsetrfranceFishMoney>
{
    Dictionary<AssetReference, FishMoney> _dic = new();
    
    private void Awake()
    {
        foreach(var a in _asetrfraces)
        {
            Addressables.LoadAssetAsync<FishMoney>(a).Completed +=
            (AsyncOperationHandle) =>
            {
                if(AsyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    _dic.Add(a ,AsyncOperationHandle.Result);
                    Debug.Log("loaded");
                }

            };

            
        }
    }
    private void Start()
    {
      Invoke(nameof(Test),5f);
    }

    private void Test()
    {
        Debug.Log(_dic[_asetrfraces[0]]?.name);
    }
}

[System.Serializable]
public class AsetrfranceFishMoney : AssetReferenceT<FishMoney>
{
    public AsetrfranceFishMoney(string guid) : base(guid)
    {
    }
}