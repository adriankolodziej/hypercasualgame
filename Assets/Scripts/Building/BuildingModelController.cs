using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModelController : MonoBehaviour
{
    [SerializeField]
    private Transform buildingBase;
    [SerializeField]
    private Transform buildingRoof;
    [SerializeField]
    private List<GameObject> possibleBuildingAttachements;
    [SerializeField]
    private BuildingController buildingController;

    private int currentSegmentsCount = 1;
    private int newSegmentFactor = 10;
    private Transform lastAddedAttachment;
    private float segmentHeight;
    private List<Transform> segments = new List<Transform>();

    private void Awake()
    {
        CreateInitialAmountOfAttachements(buildingController.Stats.StartingCitizensCount);
        buildingController.AmountOfCitizensChanged += OnAmountOfCitizensChanged;
    }

    private void OnDestroy()
    {
        buildingController.AmountOfCitizensChanged -= OnAmountOfCitizensChanged;
        buildingController.OwnerChanged -= OnOwnerChanged;
    }

    public void CreateInitialAmountOfAttachements(int startingCitizensCount)
    {
        segmentHeight = buildingBase.GetComponentInChildren<Collider>().bounds.extents.y;
        lastAddedAttachment = buildingBase;
        for (int i = currentSegmentsCount; i < startingCitizensCount / newSegmentFactor; i++)
        {
            AttachNewElement();
        }
        buildingController.OwnerChanged += OnOwnerChanged;
    }

    private void OnAmountOfCitizensChanged(int citizensCount)
    {
        if ((citizensCount / newSegmentFactor) >= currentSegmentsCount)
        {
            AttachNewElement();
        }
        else if (citizensCount < ((currentSegmentsCount - 1) * newSegmentFactor))
        {
            RemoveElement();
        }
    }

    private void RemoveElement()
    {
        buildingRoof.position = buildingRoof.position - new Vector3(0, segmentHeight * 2, 0);
        segments.Remove(lastAddedAttachment);
        currentSegmentsCount--;
        Destroy(lastAddedAttachment.gameObject);
        if (segments.Count > 0)
        {
            lastAddedAttachment = segments.Count > 0 ? segments[currentSegmentsCount - 1] : buildingBase;
        }
    }

    private void AttachNewElement()
    {
        int elementRandomId = Random.Range(0, possibleBuildingAttachements.Count);
        if (!lastAddedAttachment)
        {
            return;
        }
        var instantiationPosition = lastAddedAttachment.position + new Vector3(0, segmentHeight * 2, 0);
        var newElement = Instantiate(possibleBuildingAttachements[elementRandomId], instantiationPosition, lastAddedAttachment.rotation, transform);
        buildingRoof.position = buildingRoof.position + new Vector3(0, segmentHeight * 2, 0);
        currentSegmentsCount++;
        lastAddedAttachment = newElement.transform;
        segments.Add(lastAddedAttachment);
    }

    private void OnOwnerChanged()
    {
        buildingRoof.GetComponent<MeshRenderer>().material.color = buildingController.OwnerMaterialColor;
        buildingBase.GetComponent<MeshRenderer>().material.color = buildingController.OwnerMaterialColor;
        foreach (var segment in segments)
        {
            segment.GetComponent<MeshRenderer>().material.color = buildingController.OwnerMaterialColor;
        }
    }
}
