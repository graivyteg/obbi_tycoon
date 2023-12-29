using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public float updateInterval = 0.5f; // �������� ���������� FPS

    public TextMeshProUGUI textMeshPro;
    private float accum = 0f; // ��������� �����
    private int frames = 0; // ���������� ������
    private float timeleft; // ���������� ����� �� ���������� ����������

    private void Start()
    {
        timeleft = updateInterval;
    }

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // ��������� FPS ������ updateInterval ������
        if (timeleft <= 0.0)
        {
            float fps = accum / frames;
            int roundedFPS = Mathf.RoundToInt(fps); // ��������� FPS �� ���������� ������ �����
            string fpsText = string.Format("{0} ФПС", roundedFPS);
            textMeshPro.text = fpsText;

            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
