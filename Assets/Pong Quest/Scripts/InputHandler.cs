using System;
using Fusion;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject cameraPrefab;
    private float _xDir;
    private Camera _playerCam;

    private void Awake()
    {
        _playerCam = Camera.main;
    }

    // private void Start()
    // {
    //     InstantiateCamera();
    // }
    //
    // private void InstantiateCamera()
    // {
    //     _playerCam = Instantiate(cameraPrefab, Camera.main.transform.position, Quaternion.identity).GetComponent<Camera>();
    //     if (NetworkInitializer.Instance.LobbyCompleted())
    //     {
    //         _playerCam.transform.Rotate(0, 0, 180);
    //         Camera.main.gameObject.SetActive(false);
    //     }
    // }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var worldPos = _playerCam.ScreenToViewportPoint(Input.GetTouch(0).position);
            _xDir = worldPos.x > 0.5f ? 1 : -1;
        }
        else if (Input.GetMouseButton(0))
        {
            var worldPos = _playerCam.ScreenToViewportPoint(Input.mousePosition);
            _xDir = worldPos.x > 0.5f ? 1 : -1;
        }
        else
            _xDir = 0;
    }

    public InputStructure GetInputs()
    {
        InputStructure inputs = new InputStructure();

        inputs.XDir = _xDir;

        return inputs;
    }
}