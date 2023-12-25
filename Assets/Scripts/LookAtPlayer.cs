using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        // Проверяем, что у нас есть ссылка на игрока
        if (player != null)
        {
            // Получаем направление к игроку
            Vector3 directionToPlayer = player.position - transform.position;

            // Используем LookRotation только для оси вращения Y (горизонтальная плоскость)
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

            // Применяем вращение к объекту, оставляя вертикальную ось без изменений
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        }
        else
        {
            Debug.LogWarning("Player reference is not set in LookAtPlayerRotationOnly script!");
        }
    }
}
