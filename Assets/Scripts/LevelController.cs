using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LevelController : MonoBehaviour
{
    private int _level => SceneManager.GetActiveScene().buildIndex;
    private int _levelCount => SceneManager.sceneCountInBuildSettings;

    [SerializeField] private GameObject _nextLevelUI;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Building _lastBuilding;

    private void Start()
    {
        _nextLevelUI.gameObject.SetActive(false);
        _nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void OnEnable()
    {
        _lastBuilding.OnBuild += OnLastBuilding;
    }

    private void OnDisable()
    {
        _lastBuilding.OnBuild -= OnLastBuilding;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _level < YandexGame.savesData.level && _level < _levelCount - 1 )
        {
            NextLevel();
        }
        
        if (_level < _levelCount - 1 && !_nextLevelUI.activeSelf && _level < YandexGame.savesData.level)
        {
            _nextLevelUI.gameObject.SetActive(true);
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(_level + 1);
    }

    private void OnLastBuilding()
    {
        YandexGame.savesData.level = _level + 1;
        YandexGame.SaveProgress();
    }
}