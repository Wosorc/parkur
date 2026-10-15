using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private int _chunkSize;
    
    [SerializeField] private GameObject _firstPlain;
    [SerializeField] private GameObject _plainPrefab;
    [SerializeField] private GameObject _zoneRespawn;
    
    [SerializeField] private GameObject[] _chunkPrefabs;

    private List<GameObject> _spawnedPlain = new List<GameObject>();
    private List<GameObject> _spawnedChunks = new List<GameObject>();

    public void Start()
    {
        _spawnedPlain.Add(_firstPlain);
    }

    public void SpawnChunk()
    {
        var lastPlain = _spawnedPlain.Last();
        
        var plain = Instantiate(_plainPrefab, lastPlain.transform.position + Vector3.forward * 30, lastPlain.transform.rotation);
        var chunk = Instantiate(_chunkPrefabs[Random.Range(0, _chunkPrefabs.Length)], lastPlain.transform.position + Vector3.forward * 15, lastPlain.transform.rotation);
        
        _spawnedPlain.Add(plain);
        _spawnedChunks.Add(chunk);
        
        _zoneRespawn.transform.position = lastPlain.transform.position + Vector3.down * 5;

        if (_spawnedPlain.Count > _chunkSize)
        {
            Destroy(_spawnedPlain[0]);
            
            _spawnedPlain.RemoveAt(0);
        }

        if (_spawnedChunks.Count > _chunkSize - 1)
        {
            Destroy(_spawnedChunks[0]);
            
            _spawnedChunks.RemoveAt(0);
        }
    }
}