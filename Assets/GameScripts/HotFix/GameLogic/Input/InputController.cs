using GameBase;
using GameFramework.Runtime;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    public class InputController : TSingleton<InputController>,IControllerBase,ILogicSys
    {
        float inputY;
        float inputX;

        public void OnApplicationPause(bool pause)
        {

        }

        public void OnDestroy()
        {

        }

        public void OnDrawGizmos()
        {

        }

        public void OnFixedUpdate()
        {

        }

        public bool OnInit()
        {
            return true;
        }

        public void OnLateUpdate()
        {

        }

        public void OnRoleLogout()
        {
            
        }

        public void OnStart()
        {

        }

        public void OnUpdate()
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                inputY = Input.GetAxis("Vertical");
                Log.Debug("x:{0}", inputY);
                RPGSytem.Instance.OnInputY(inputY);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                inputX = Input.GetAxis("Horizontal");
                Log.Debug("y:{0}", inputX);
                RPGSytem.Instance.OnInputX(inputX);
            }
        }

        void IControllerBase.OnEnter()
        {

            GameApp.Instance.AddLogicSys(Instance);
        }

        void IControllerBase.OnExit()
        {

        }
    }
}