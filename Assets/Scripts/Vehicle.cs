using System;
using System.Collections;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

public class Vehicle : MonoBehaviour
{
    public bool selfInit = false;
    public float moveSpeed;
    public float rotationSpeed;
    public float rotateAmount;
    private Vector2 _startPos;
    public Vector2 _minMaxWiggleTime;
    public Vector2 _minMaxYWiggleAmount;
    public Vector2 _minMaxXWiggleAmount;
    private Vector2 _targetPos;
    private float _maxDistance;
    private bool _flip;

    private void Start()
    {
        if (selfInit) Init(transform.position);
    }

    public void Init(Vector2 pos)
    {
        _startPos = pos;
        _targetPos = pos;
        _maxDistance = 0;
        _flip = Random.Range(0, 2) == 1;
        StartCoroutine(SetRandomOffset());
    }

    private IEnumerator SetRandomOffset()
    {
        yield return new WaitForEndOfFrame();
        
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(_minMaxWiggleTime.GetRandomInVector());
            var randomYWiggle = _minMaxYWiggleAmount.GetRandomInVector();
            var randomXWiggle = _minMaxXWiggleAmount.GetRandomInVector();
            var randomWiggle = new Vector2(randomXWiggle, randomYWiggle);
            var targetWiggle = _flip ? randomWiggle : -randomWiggle;
            _targetPos = _startPos + targetWiggle;
            UpdateMaxDistance();

            yield return new WaitUntil(HasReachedTargetPosition);
           _flip = Random.Range(0, 2) == 1;
        }
    }

    private void UpdateMaxDistance()
    {
        _maxDistance = Vector2.Distance(transform.position, _targetPos);
    }

    private bool HasReachedTargetPosition()
    {
        return (Vector2)transform.position == _targetPos;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPos, moveSpeed * Time.deltaTime);
        var currentDistance = Vector2.Distance(transform.position, _targetPos);
        var distanceAmount = currentDistance / _maxDistance;
        var zRot = rotateAmount * distanceAmount;
        zRot = _targetPos.y > transform.position.y ? zRot : -zRot;
        var rot = new Vector3(0, 0, zRot);
        var q = Quaternion.Euler(rot.normalized*rotateAmount);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationSpeed * Time.deltaTime);
    }
}
