using UnityEngine;

public class ZoneRespawn : MonoBehaviour
{
    [SerializeField] private Collider _zoneCollider;

    public void OnTriggerEnter(Collider other)
    {
        var gameobject = other.gameObject;
        
        if (gameobject.tag == "Player")
        {
            gameobject.GetComponent<PlayerRespawn>().ReSpawn();
        }
    }
}