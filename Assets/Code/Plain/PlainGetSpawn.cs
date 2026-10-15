using UnityEngine;

public class PlainGetSpawn : MonoBehaviour
{
    [SerializeField] private Transform _spawn;

    public void OnCollisionEnter(Collision collision)
    {
        var GameObject = collision.gameObject;
        
        if (GameObject.tag == "Player")
        {
            GameObject.GetComponent<PlayerRespawn>().SetRespawn(_spawn);
        }
        
        Destroy(this);
    }
}
