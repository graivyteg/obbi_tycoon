using UnityEngine;
using Zenject;

public class LookAtCamera : MonoBehaviour
{
    [Inject] private Camera _camera;
    [SerializeField] private bool _invert = false;

    private void Update()
    {
        if (_camera == null)
        {
            Debug.LogError("Camera is not found!");
            return;
        }
        
        Vector3 directionToPlayer = _camera.transform.position - transform.position;
        if (_invert) directionToPlayer *= -1;
        
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
    }
}
