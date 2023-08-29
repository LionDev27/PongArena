using System;
using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private float _speed = 3f;

    private NetworkRigidbody2D _rb;

    private void Awake()
    {
        if (!_rb)
            _rb = GetComponent<NetworkRigidbody2D>();
        if (_rb) return;
        Debug.LogError("No hay rigidbody");
        enabled = false;
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out InputStructure inputData))
        {
            if (inputData.XDir == 0)
                _rb.Rigidbody.velocity = Vector2.zero;
            var velocity = _speed * Runner.DeltaTime * new Vector2(inputData.XDir, 0);
            _rb.Rigidbody.velocity = velocity;
        }
    }
}