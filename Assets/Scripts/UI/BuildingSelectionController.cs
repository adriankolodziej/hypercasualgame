using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildingSelectionController : MonoBehaviour
{
    private PlayerController playerController;
    private List<BuildingController> buildings;
    private BuildingController selectedBuilding;

    private void Start()
    {
        PlayerInput.Instance.PlayerClicked += OnPlayerClicked;
    }

    private void OnDestroy()
    {
        if (PlayerInput.Instance != null)
        {
            PlayerInput.Instance.PlayerClicked -= OnPlayerClicked;
        }
    }

    public void OnLevelLoaded()
    {
        playerController = FindObjectOfType<PlayerController>();
        buildings = FindObjectsOfType<BuildingController>().ToList();
    }

    private void OnPlayerClicked(GameObject objectClicked)
    {
        var buildingToSelect = buildings.Find(x => x.gameObject == objectClicked);
        if (buildingToSelect != null)
        {
            if (selectedBuilding != null && buildingToSelect != null && buildingToSelect != selectedBuilding)
            {
                selectedBuilding.BuildingConnections.AddNewConnection(buildingToSelect);
                selectedBuilding.SelectBuilding(false, playerController);
                selectedBuilding = null;
            }
            if (selectedBuilding == null && buildingToSelect.Owner == playerController)
            {
                selectedBuilding = buildingToSelect;
                selectedBuilding.SelectBuilding(true, playerController);
            }
        }
        else
        {
            selectedBuilding?.SelectBuilding(false, playerController);
            selectedBuilding = null;
        }
    }
}
