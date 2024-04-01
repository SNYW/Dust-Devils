using System;
using System.Collections;
using SystemEvents;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] spawnAreas;
    public GameObject[] spawnableEnemies;
    public float spawnDelay;

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(spawnDelay);
            var pos = GetRandomPositionInSpawnArea(spawnAreas.Random());
        
            var spawned = Instantiate(spawnableEnemies.Random(), pos, quaternion.identity).GetComponent<Vehicle>();
            spawned.Init(pos);
            SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.EnemySpawned, spawned);
        }
    }

    private Vector2 GetRandomPositionInSpawnArea(GameObject spawnArea)
    {
        var spawnAreaDimensions = GetSpawnAreaDimensions(spawnArea);
        var randPos = new Vector2(Random.Range(-spawnAreaDimensions.x, spawnAreaDimensions.x),
            Random.Range(-spawnAreaDimensions.y, spawnAreaDimensions.y));
        return (Vector2)spawnArea.transform.position + randPos;
    }

    private Vector2 GetSpawnAreaDimensions(GameObject spawnArea)
    {
        return new Vector2(spawnArea.transform.localScale.x /2, spawnArea.transform.localScale.y / 2);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
