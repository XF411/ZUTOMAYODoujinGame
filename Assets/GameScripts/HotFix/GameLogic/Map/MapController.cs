using UnityEditor;
using UnityEngine;
using GameBase;

namespace GameLogic
{
    public class MapController : Singleton<MapController>,IControllerBase
    {
        GameObject curMapObj;
        public void LoadMap()
        {
            curMapObj = GameModule.Resource.LoadGameObject("Map");
            curMapObj.transform.position = Vector3.zero;
        }

        void IControllerBase.OnEnter()
        {

        }

        void IControllerBase.OnExit()
        {

        }
    }
}