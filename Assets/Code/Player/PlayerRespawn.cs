using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private CapsuleCollider _playerCollider;
    
    [SerializeField] private Transform _spawn;
    
    public void ReSpawn()
    {
        if (_spawn == null)
        {
            return;
        }
        
        _playerPosition.position =  _spawn.position + Vector3.up * _playerCollider.height / 2;
    }

    public void SetRespawn(Transform spawn)
    {
        _spawn = spawn;
    }
}
