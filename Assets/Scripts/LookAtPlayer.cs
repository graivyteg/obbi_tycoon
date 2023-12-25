using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        // ���������, ��� � ��� ���� ������ �� ������
        if (player != null)
        {
            // �������� ����������� � ������
            Vector3 directionToPlayer = player.position - transform.position;

            // ���������� LookRotation ������ ��� ��� �������� Y (�������������� ���������)
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

            // ��������� �������� � �������, �������� ������������ ��� ��� ���������
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        }
        else
        {
            Debug.LogWarning("Player reference is not set in LookAtPlayerRotationOnly script!");
        }
    }
}
