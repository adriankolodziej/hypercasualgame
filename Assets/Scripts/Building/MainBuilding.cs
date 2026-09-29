using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : BuildingController
{
    [SerializeField]
    [RequireInterface(typeof(BuildingOwner))]
    private UnityEngine.Object buildingsOwner;

    protected override void Start()
    {
        Owner = buildingsOwner as BuildingOwner;
        base.Start();
    }
}
