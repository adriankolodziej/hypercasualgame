using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public event Action<int> AmountOfCitizensChanged;
    public event Action<BuildingOwner, int> NewCitizenArrived;
    public event Action OwnerChanged;

    [SerializeField]
    protected BuildingStats stats;
    [SerializeField]
    protected BuildingConnections buildingConnections;
    [SerializeField]
    protected SelectionIndicator selectionIndicator;
    [SerializeField]
    private CounterUI citizensCounterUI;

    protected int citizensCount;
    protected float spawnTimer = 0;
    protected BuildingOwner owner;
    private Material ownerMaterialColor;

    public Color OwnerMaterialColor
    {
        get
        {
            return ownerMaterialColor != null ? ownerMaterialColor.color : Color.white;
        }
    }

    public BuildingConnections BuildingConnections => buildingConnections;
    public BuildingStats Stats => stats;
    public BuildingOwner Owner
    {
        get
        {
            return owner;
        }
        protected set
        {
            owner = value;
            buildingConnections.Initialize(stats.CitizenSetOffInterval, stats.CitizenDamage, ownerMaterialColor, owner);
            OwnerChanged?.Invoke();
        }
    }

    protected virtual void Start()
    {
        citizensCount = stats.StartingCitizensCount;
        buildingConnections.Initialize(stats.CitizenSetOffInterval, stats.CitizenDamage, ownerMaterialColor, Owner);
        citizensCounterUI.SetCounterText(citizensCount, stats.MaxCitizensCount, OwnerMaterialColor);
    }

    private void Update()
    {
        UpdateCitizens();
        if (citizensCount <= 0)
        {
            buildingConnections.IsCitizenSpawnEnabled = false;
        }
        else
        {
            buildingConnections.IsCitizenSpawnEnabled = true;
        }
    }

    protected virtual void UpdateCitizens()
    {
        if (citizensCount < stats.MaxCitizensCount)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > stats.NewCitizenSpawnTime)
            {
                spawnTimer = 0;
                SpawnNewCitizen();
            }
        }
    }

    private void SpawnNewCitizen()
    {
        citizensCount++;
        citizensCounterUI.SetCounterText(citizensCount, stats.MaxCitizensCount, OwnerMaterialColor);
        AmountOfCitizensChanged?.Invoke(citizensCount);
        NewCitizenArrived?.Invoke(Owner, 1);
    }

    protected virtual void NewCitizenArrival(CitizenBehaviour citizen)
    {
        if (Owner == citizen.Owner || Owner == null)
        {
            citizensCount += citizen.Damage;
            if (citizensCount >= stats.MaxCitizensCount)
            {
                citizensCount = stats.MaxCitizensCount;
            }
            else
            {
                NewCitizenArrived?.Invoke(Owner, citizen.Damage);
            }
        }
        else
        {
            citizensCount--;
            if (citizensCount < 0)
            {
                citizensCount = 0;
            }
            else
            {
                NewCitizenArrived?.Invoke(Owner, -citizen.Damage);
            }
        }
        citizensCounterUI.SetCounterText(citizensCount, stats.MaxCitizensCount, OwnerMaterialColor);
        AmountOfCitizensChanged?.Invoke(citizensCount);
    }

    public void SetOwner(BuildingOwner owner, Material citizenMaterial)
    {
        this.ownerMaterialColor = citizenMaterial;
        this.Owner = owner;
    }

    public void SelectBuilding(bool isSelected, BuildingOwner player)
    {
        if (isSelected && owner == player)
        {
            selectionIndicator.Select();
        }
        else
        {
            selectionIndicator.Unselect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TagsComparer.Compare(other.tag, Tag.Citizen))
        {
            NewCitizenArrival(other.GetComponent<CitizenBehaviour>());
            Destroy(other.gameObject);
        }
    }
}
