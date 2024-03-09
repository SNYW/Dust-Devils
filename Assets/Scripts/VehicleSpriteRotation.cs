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
        var angle = Mathf.Clamp( maxAngle * _agent.remainingDistance, 0, maxAngle);
        var desiredAngle = target.z >= transform.position.z ? -angle : angle;
        
        Quaternion targetRotation = Quaternion.Euler(0, 90+desiredAngle, 0);

        spriteParentTransform.rotation = Quaternion.Slerp(spriteParentTransform.rotation, targetRotation,
            rotationSpeed * Time.deltaTime);
    }
}
