using System.Collections;
using UnityEngine;
using Cinemachine;
using GameLogic.Common;

namespace GameLogic
{
    public class CameraControl : GameBase.Singleton<CameraControl>,IControllerBase
    {
        GameObject MainCameraObj;
        Camera MainCamera;
        CinemachineBrain MainCinemachineBrain;
        VitrualCameraList vitrualCameraList;
        CinemachineVirtualCamera curCamera;
        void IControllerBase.OnEnter()
        {
            if (MainCameraObj == null)
            {
                MainCameraObj = GameModule.Resource.LoadGameObject("MainCameraPrefab");
                MainCamera = MainCameraObj.GetComponent<Camera>();
                MainCinemachineBrain = MainCameraObj.GetComponent<CinemachineBrain>();
                vitrualCameraList = GameObjectCommon.FindComponentWithName<VitrualCameraList>("TransVirtualCamera", MainCameraObj);
                curCamera = vitrualCameraList.cinemachineVirtualCameras[0];
                //GameObjectCommon.FindComponentWithName<CinemachineBrain>("MainCamera", MainCameraObj);
            }
        }

        void IControllerBase.OnExit()
        {
            MainCamera = null;
            MainCinemachineBrain = null;
            GameObject.DestroyImmediate(MainCameraObj);
        }

        public void LookAt(Transform target) 
        {
            if(curCamera != null) 
            {
                curCamera.LookAt = target;
            }
        }

        public void Follow(Transform target)
        {
            if (curCamera != null)
            {
                curCamera.Follow = target;
            }
        }
    }

    
}