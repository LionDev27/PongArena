using Fusion;
using UnityEngine;

public class PlayerScorer : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnPlayerScore))]
    public NetworkBool scored { get; set; }
    
    [SerializeField] private PlayerScore _playerScore;

    public void OnPlayerScore()
    {
        _playerScore.AddScore();
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && _playerScore != null)
        {
            Destroy(other.gameObject);
            
        }
    }
}