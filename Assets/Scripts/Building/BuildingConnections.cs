using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConnections : MonoBehaviour
{
    [Serializable]
    private class Connection
    {
        private float timer;
        [SerializeField]
        private BuildingController buildingController;

        public float Timer { get; set; }
        public BuildingController BuildingController => buildingController;

        public Connection(float timer, BuildingController buildingController)
        {
            this.timer = timer;
            this.buildingController = buildingController;
        }
    }

    [SerializeField]
    private GameObject citizenPrefab;
    [SerializeField]
    private List<Connection> connections = new List<Connection>();

    private float citizenSetOffInterval;
    private int citizenDamage;
    private BuildingOwner owner;
    private Material citizenMaterial;
    private bool isCitizenSpawnEnabled;

    public bool IsCitizenSpawnEnabled
    {
        get { return isCitizenSpawnEnabled; }
        set { isCitizenSpawnEnabled = value; }
    }

    public void Initialize(float setOffInterval, int citizenDamage, Material citizenMaterial, BuildingOwner owner)
    {
        citizenSetOffInterval = setOffInterval;
        this.citizenDamage = citizenDamage;
        this.owner = owner;
        this.citizenMaterial = citizenMaterial;
    }

    private void Update()
    {
        if (IsCitizenSpawnEnabled && owner != null)
        {
            foreach (var connection in connections)
            {
                if (connection.Timer >= citizenSetOffInterval)
                {
                    SetCitizenOff(connection.BuildingController.transform);
                    connection.Timer = 0;
                }
                connection.Timer += Time.deltaTime;
            }
        }
    }

    public void RemoveAllConections()
    {
        connections.Clear();
    }

    public void AddNewConnection(BuildingController buildingController)
    {
        var connection = connections.Find(x => x.BuildingController == buildingController);
        if (connection == null)
        {
            connections.Add(new Connection(0, buildingController));
        }
    }

    private void SetCitizenOff(Transform target)
    {
        var citizen = Instantiate(citizenPrefab, transform);
        citizen.GetComponent<MeshRenderer>().material = citizenMaterial;
        citizen.GetComponent<CitizenBehaviour>().Initialize(target, citizenDamage, owner, citizenMaterial);
    }
}
