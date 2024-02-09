using Cinemachine;
using UnityEngine;

public class CinematicsManager : MonoBehaviour
{
    private CinemachineVirtualCamera[] allCamerasScene;

    private void Start()
    {
        allCamerasScene = FindObjectsOfType<CinemachineVirtualCamera>();
    }

    private void DisableAll()
    {
        foreach (var camera in allCamerasScene)
            camera.Priority = 0;
    }

    public void ActiveVirtualCamera(CinemachineVirtualCamera cam)
    {
        DisableAll();
        cam.Priority = 1;
    }
}
