using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector2 targetPosition;
    public float rotationSpeed = 5f;

    void Update()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 direction = targetPosition - (Vector2)transform.position;

        if (direction != Vector2.zero)
        { 
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
            Quaternion targetRotation = Quaternion.AngleAxis(angle-90, Vector3.forward); 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
