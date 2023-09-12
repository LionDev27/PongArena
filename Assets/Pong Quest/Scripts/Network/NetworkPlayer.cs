using DG.Tweening;
using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local;
    public InputHandler Handler;

    [SerializeField] private PlayerScorer _scorer;

    private PlayerScore _playerScore;

    private void Awake()
    {
        _playerScore = GetComponent<PlayerScore>();
    }

    public void AddScore()
    {
        UIController.Instance.UpdateScores(_playerScore.GetID());
        if (Runner.IsClient)
            CounterAnimation.Instance.CounterAnim(true).Play();
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            _scorer.transform.SetParent(null);
            if (!Runner.IsClient)
            {
                _playerScore.SetID(0);
                return;
            }
            Camera.main.transform.Rotate(0, 0, 180);
            CounterAnimation.Instance.CounterAnim(true).Play();
            Handler.InvertControls(true);
            _playerScore.SetID(1);
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);
    }
}