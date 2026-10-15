using System;
using UnityEngine;

public class PlainSetChunk : MonoBehaviour
{
    [SerializeField] private ChunkPlacer _chunkPlacer;

    public void Start()
    {
        _chunkPlacer = FindFirstObjectByType<ChunkPlacer>();
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _chunkPlacer.SpawnChunk();
        }
        
        Destroy(this);
    }
}
