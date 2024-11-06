using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEditor.AddressableAssets.Settings;

public abstract class RefranceBankBase<T> : MonoBehaviour where T : AssetReference
{
    [Header(nameof(RefranceBankBase<T>))]
    [SerializeField] protected List<T> _asetrfraces;

}