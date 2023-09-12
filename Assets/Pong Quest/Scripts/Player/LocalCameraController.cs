using UnityEngine;

public class LocalCameraController : MonoBehaviour
{
    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneWidth = 10;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        _camera.orthographicSize = desiredHalfHeight;
    }
}