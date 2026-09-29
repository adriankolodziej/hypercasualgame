using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOwner: MonoBehaviour
{
    [SerializeField]
    private MainBuilding mainBuilding;
    [SerializeField]
    private Material citizenMaterial;

    private void Awake()
    {
        MainBuilding.SetOwner(this, CitizenMaterial);
    }

    protected MainBuilding MainBuilding => mainBuilding;
    protected Material CitizenMaterial => citizenMaterial;
}
