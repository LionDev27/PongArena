using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private TextMeshProUGUI _myScore, _otherScore;

    public static UIController Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void HideMainMenu()
    {
        _mainMenu.SetActive(false);
    }

    public void ShowGameMenu()
    {
        _gameUI.SetActive(true);
    }

    public void UpdateScores(int id)
    {
        if (id == 0)
        {
            _myScore.text = ScoreController.Instance.HostScore.ToString();
            _otherScore.text = ScoreController.Instance.ClientScore.ToString();
        }
        else
        {
            _myScore.text = ScoreController.Instance.ClientScore.ToString();
            _otherScore.text = ScoreController.Instance.HostScore.ToString();
        }
    }
}