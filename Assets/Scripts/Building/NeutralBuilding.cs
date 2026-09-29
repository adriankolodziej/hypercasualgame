using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralBuilding : BuildingController
{
    protected override void UpdateCitizens()
    {
        return;
    }

    protected override void NewCitizenArrival(CitizenBehaviour citizen)
    {
        base.NewCitizenArrival(citizen);
        if (citizensCount == 0)
        {
            owner = null;
            buildingConnections.RemoveAllConections();
        }
        if (Owner == null)
        {
            SetOwner(citizen.Owner, citizen.GetComponent<MeshRenderer>().material);
        }
    }
}
