using System;
using UnityEngine;

public class PlainSetChunk : MonoBehaviour
{
    private ChunkPlacer _chunkPlacer;

    public void Start()
    {
        _chunkPlacer = FindFirstObjectByType<ChunkPlacer>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _chunkPlacer.SpawnChunk();
        }
        
        Destroy(this);
    }
}
