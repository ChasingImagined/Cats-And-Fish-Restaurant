using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : Being
{
    [Header(nameof(Resource))]
    [SerializeField] private Sprite _icon;

    public Sprite GetIcon()
    {
        return _icon;
    }
}
