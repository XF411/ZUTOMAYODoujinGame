using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VitrualCameraList : MonoBehaviour
{
    public List<CinemachineVirtualCamera> cinemachineVirtualCameras = new List<CinemachineVirtualCamera>();

    private void Awake()
    {
        cinemachineVirtualCameras.Clear();
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++) 
        {
            var child = transform.GetChild(i);
            if (child != null) 
            {
                var camera = child.GetComponent<CinemachineVirtualCamera>();
                if (camera != null) 
                {
                    cinemachineVirtualCameras.Add(camera);
                }
            } 
        }
    }
}
