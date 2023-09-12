using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public float xDir;
    private Camera _playerCam;
    private bool _invertControls;

    private void Awake()
    {
        _playerCam = Camera.main;
    }

    public void InvertControls(bool value)
    {
        _invertControls = value;
    }

    private void Update()
    {
        Vector2 worldPos;
        if (Input.touchCount > 0)
            worldPos = _playerCam.ScreenToWorldPoint(Input.GetTouch(0).position);
        else if (Input.GetMouseButton(0))
            worldPos = _playerCam.ScreenToWorldPoint(Input.mousePosition);
        else
        {
            SetDir(0);
            return;
        }

        if (_invertControls)
        {
            if (worldPos.y > transform.position.y)
                SetDir(worldPos.x);
            return;
        }
        
        if (worldPos.y < transform.position.y)
            SetDir(worldPos.x);
    }

    private void SetDir(float xPos)
    {
        if (xPos == 0)
        {
            xDir = 0;
            return;
        }
        
        xDir = xPos > 0f ? 1 : -1;
    }

    public InputStructure GetInputs()
    {
        InputStructure inputs = new InputStructure();

        inputs.XDir = xDir;

        return inputs;
    }
}