using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int _id;
    private NetworkPlayer _networkPlayer;

    private void Awake()
    {
        _networkPlayer = GetComponent<NetworkPlayer>();
    }

    public void SetID(int id)
    {
        _id = id;
    }
    
    public void AddScore()
    {
        _networkPlayer.AddScore();
        ScoreController.Instance.UpdateScore(_id);
    }

    public int GetID()
    {
        return _id;
    }
}