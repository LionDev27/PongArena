using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform _leftLimit, _rightLimit;
    [SerializeField] private GameObject _limitPrefab;

    private BallManager _ballManager;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
        _ballManager = GetComponentInChildren<BallManager>();
    }

    [ContextMenu("Start Game")]
    private void StartGame()
    {
        _ballManager.SpawnBall();
    }

    private void PrepareGame()
    {
        Runner.Spawn(_limitPrefab, _leftLimit.position, Quaternion.identity, Object.InputAuthority);
        Runner.Spawn(_limitPrefab, _rightLimit.position, Quaternion.identity, Object.InputAuthority);
    }

    private void OnEnable()
    {
        NetworkInitializer.OnLobbyCompleted += PrepareGame;
        CounterAnimation.OnAnimationEnd += StartGame;
    }

    private void OnDisable()
    {
        NetworkInitializer.OnLobbyCompleted -= PrepareGame;
        CounterAnimation.OnAnimationEnd -= StartGame;
    }
}