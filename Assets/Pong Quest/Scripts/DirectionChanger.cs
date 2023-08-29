using Fusion;
using UnityEngine;

public class DirectionChanger : NetworkBehaviour
{
    [SerializeField] private bool _changeX;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out NetworkRigidbody2D rb) && col.collider.CompareTag("Ball"))
        {
            Debug.Log("Colliding");
            var velocity = rb.Rigidbody.velocity;
            Debug.Log(velocity);
            if (_changeX)
                velocity.x = -velocity.x;
            else
                velocity.y = -velocity.y;
            rb.Rigidbody.velocity = velocity;
            Debug.Log(velocity);
        }
    }
}