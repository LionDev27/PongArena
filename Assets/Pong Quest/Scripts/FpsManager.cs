using UnityEngine;

public class FpsManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}