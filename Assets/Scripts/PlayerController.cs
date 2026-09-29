using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BuildingOwner
{ 
    private List<BuildingController> takenOverBuildings;

    public void TakeOverBuilding(BuildingController buildingController)
    {
        buildingController.SetOwner(this, CitizenMaterial);
        takenOverBuildings.Add(buildingController);
    }
}
