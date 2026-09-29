using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingStats", menuName = "ScriptableObjects/CreateNewBuildingStats", order = 1)]
public class BuildingStats : ScriptableObject
{
    [SerializeField]
    private float newCitizenSpawnTime = 1;
    [SerializeField]
    private float citizenSetOffInterval = 1;
    [SerializeField]
    private int startingCitizensCount = 10;
    [SerializeField]
    private int maxCitizensCount = 50;
    [SerializeField]
    private int citizenDamage = 1;

    public float NewCitizenSpawnTime => newCitizenSpawnTime;
    public float CitizenSetOffInterval => citizenSetOffInterval;
    public int StartingCitizensCount => startingCitizensCount;
    public int MaxCitizensCount => maxCitizensCount;
    public int CitizenDamage => citizenDamage;
}
