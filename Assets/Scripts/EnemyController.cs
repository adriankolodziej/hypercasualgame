using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyController : BuildingOwner
{
    [SerializeField]
    private List<BuildingController> buildings;
    [SerializeField]
    private float newTakeOverInterval = 5f;

    private float newTakeOverTimer;

    private void Update()
    {
        newTakeOverTimer += Time.deltaTime;
        if (newTakeOverTimer >= newTakeOverInterval && buildings.Count > 0)
        {
            int randomID = Random.Range(0, buildings.Count);
            var randomBuilding = buildings[randomID];
            if (randomBuilding != null)
            {
                MainBuilding.BuildingConnections.AddNewConnection(randomBuilding);
                buildings.RemoveAt(randomID);
            }
            newTakeOverTimer = 0;
        }
    }

#if UNITY_EDITOR

    [ContextMenu("Find all buildings")]
    private void FindAllBuildings()
    {
        buildings = FindObjectsOfType<BuildingController>().ToList();
        buildings.Remove(MainBuilding);
        EditorUtility.SetDirty(gameObject);
    }
#endif
}
