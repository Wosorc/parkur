using UnityEngine;

public class PlainGetSpawn : MonoBehaviour
{
    [SerializeField] private Transform _spawn;

    public void OnTriggerEnter(Collider other)
    {
        var GameObject = other.gameObject;
        
        if (GameObject.tag == "Player")
        {
            GameObject.GetComponent<PlayerRespawn>().SetRespawn(_spawn);
        }
        
        Destroy(this);
    }
}
