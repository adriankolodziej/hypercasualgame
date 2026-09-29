using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenBehaviour : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer renderer;
    [SerializeField]
    private Collider collider;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float velocity = 2;

    private Vector3 target;
    private int damage;
    private BuildingOwner owner;

    public BuildingOwner Owner => owner;
    public int Damage => damage;

    private void Update()
    {
        Move();
    }

    public void Initialize(Transform target, int citizenDamage, BuildingOwner owner, Material material)
    {
        SnapToGround();
        this.target = new Vector3(target.position.x, transform.position.y, target.position.z);
        damage = citizenDamage;
        this.owner = owner;
        renderer.material = material;

    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * velocity);
    }

    private void SnapToGround()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, Vector3.down, out hit, float.MaxValue, layerMask))
        {
            if (hit.collider != null)
            {
                transform.position = hit.point + new Vector3(0, collider.bounds.extents.y / 2, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TagsComparer.Compare(other.tag, Tag.Citizen))
        {
            if (other.GetComponent<CitizenBehaviour>().Owner != Owner)
            {
                Destroy(gameObject);
            }
        }
    }
}
