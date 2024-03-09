using UnityEngine;
using UnityEngine.AI;

public class VehicleSpriteRotation : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform spriteParentTransform;
    public float maxAngle;
    public float rotationSpeed;
    
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_agent == null || !_agent.hasPath) RotateToDefault();
        else SlerpTowards(_agent.destination);
    }

    private void RotateToDefault()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
        
        spriteParentTransform.rotation = Quaternion.Slerp(spriteParentTransform.rotation, targetRotation,
            rotationSpeed * 2 * Time.deltaTime);
    }

    private void SlerpTowards(Vector3 target)
    {
        float desiredAngle = target.z >= transform.position.z ? -maxAngle : maxAngle;

        var dist = Mathf.Clamp01(Vector3.Distance(transform.position, target));

        Quaternion targetRotation = Quaternion.Euler(0, 90+desiredAngle*dist, 0);

        spriteParentTransform.rotation = Quaternion.Slerp(spriteParentTransform.rotation, targetRotation,
            rotationSpeed * Time.deltaTime);
    }
}
