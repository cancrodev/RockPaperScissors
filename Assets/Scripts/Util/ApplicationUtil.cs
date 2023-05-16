using UnityEngine;

public class ApplicationUtil : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}