using Lean.Pool;
using PajamaNinja.Common;
using PajamaNinja.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : Singleton<CollectablesManager>
{
    [SerializeField] private List<Collectable> _collectablePrefabs;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private float _xSpawnRange = 10f;
    [SerializeField] private float _zSpawnRange = 10f;
    [SerializeField] private Transform _spawnCenter;

    private Coroutine _repeatedSpawnRoutine;
    private bool _isSpawning = true;

    public Collectable LastCollected { get; private set; }

    private void Start()
    {
        _repeatedSpawnRoutine = StartCoroutine(RepeatedSpawnRoutine());
    }

    private void OnEnable()
    {
        Collectable.OnPickedUp += HandleCollectablePickedUp;
    }

    private void OnDisable()
    {
        Collectable.OnPickedUp -= HandleCollectablePickedUp;
    }

    private IEnumerator RepeatedSpawnRoutine()
    {
        while (_isSpawning)
        {
            yield return new WaitForSeconds(_spawnRate);
            SpawnRandomCollectable();
        }
    }

    private void SpawnRandomCollectable()
    {
        Vector3 spawnPos = SpatialHelper.GetRandomPosInArea(_spawnCenter.position, 0f, _xSpawnRange, _zSpawnRange);
        Collectable randCollectable = _collectablePrefabs[Random.Range(0, _collectablePrefabs.Count)];
        LeanPool.Spawn(randCollectable, spawnPos, Quaternion.identity, transform);
    }

    private void HandleCollectablePickedUp(Collectable collectable)
    {
        LastCollected = collectable;
    }
}
