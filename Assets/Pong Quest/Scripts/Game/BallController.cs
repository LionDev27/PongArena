using Fusion;
using UnityEngine;

public class BallController : NetworkBehaviour
{
    public void Init(Vector2 dir)
    {
        GetComponent<Rigidbody2D>().velocity = dir;
    }
}