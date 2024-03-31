using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class Vehicle : MonoBehaviour
{
    public float detectionRange;
    public LayerMask targetOverlap;
    private VehicleMovement _movement;
    private List<Weapon> _weapons;

    private void Awake()
    {
        _movement = GetComponent<VehicleMovement>();
        GetWeapons();
        StartCoroutine(GetTargets());
    }

    public void Init(Vector2 pos)
    {
        _movement.Init(pos);
    }

    private IEnumerator GetTargets()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, detectionRange, targetOverlap);
        
        if (targets.Length > 0 && _weapons.Count > 0)
        {
            Vector2 myPos = transform.position;
            var target = myPos.GetClosestCollider2D(targets);
            
            foreach (var weapon in _weapons)
            {
                if (target.gameObject.activeSelf && target.TryGetComponent<Vehicle>(out var component))
                    weapon.target = component;
            }
        }

        yield return new WaitForSeconds(1);
    }

    private void GetWeapons()
    {
        _weapons = new List<Weapon>();
        _weapons = GetComponentsInChildren<Weapon>().ToList();
    }
}
