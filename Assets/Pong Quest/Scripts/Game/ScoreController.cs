using System;
using Fusion;

public class ScoreController : NetworkBehaviour
{
    public static ScoreController Instance;
    public static Action OnPlayerScored;
    public int HostScore => _hostScore;
    public int ClientScore => _clientScore;
    
    private int _hostScore, _clientScore;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void UpdateScore(int id)
    {
        if (id == 0)
            _hostScore++;
        else
            _clientScore++;
        OnPlayerScored?.Invoke();
    }
}