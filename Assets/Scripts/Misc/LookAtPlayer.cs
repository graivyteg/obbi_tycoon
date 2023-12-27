using UnityEngine;
using Zenject;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private bool _invert = false;
    [Inject] private Player _player;

    private void Update()
    {
        if (_player == null)
        {
            Debug.LogError("Player is not found!");
            return;
        }
        
        Vector3 directionToPlayer = _player.transform.position - transform.position;
        if (_invert) directionToPlayer *= -1;
        
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
    }
}
