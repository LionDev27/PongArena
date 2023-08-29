using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local;
    public InputHandler Handler;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
            Local = this;
        if (HasStateAuthority && NetworkInitializer.Instance.HasPlayer())
        {
            Camera.main.transform.Rotate(0, 0, 180);
        }
    }
    
    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);
    }
}