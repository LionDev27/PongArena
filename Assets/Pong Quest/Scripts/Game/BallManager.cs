using Fusion;
using UnityEngine;

public class BallManager : NetworkBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _startingBallSpeed = 10f;
    [SerializeField] private PlayerDirections[] _startingDirections;

    private void Awake()
    {
        foreach (var playerDir in _startingDirections)
        {
            foreach (var dir in playerDir.directions)
            {
                dir.Normalize();
            }
        }
    }

    public void SpawnBall()
    {
        int randomPlayer = Random.Range(0, _startingDirections.Length);
        int randomIndex = Random.Range(0, _startingDirections[randomPlayer].directions.Length);
        Vector2 startingDirection = _startingDirections[randomPlayer].directions[randomIndex];

        Runner.Spawn(_ballPrefab, Vector3.zero, Quaternion.identity, Object.InputAuthority, (runner, o) =>
        {
            //Initialize before syncronizing
            o.GetComponent<BallController>().Init(startingDirection * _startingBallSpeed);
        });
    }
}

[System.Serializable]
public struct PlayerDirections
{
    public Vector2[] directions;
}