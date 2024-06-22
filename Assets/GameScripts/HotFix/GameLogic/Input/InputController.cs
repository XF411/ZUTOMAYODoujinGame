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
            Vector2 moveDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.UpArrow)) 
            {
                moveDirection = Vector2.up;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            { 
                moveDirection = Vector2.down; 
            }
                
            if (Input.GetKey(KeyCode.LeftArrow))
            { 
                moveDirection = Vector2.left; 
            }
                
            if (Input.GetKey(KeyCode.RightArrow)) 
            { 
                moveDirection = Vector2.right; 
            }
                

            if (moveDirection != Vector2.zero)
            {
                //Log.Debug(moveDirection);
                RPGSytem.Instance.OnInputDirection(moveDirection);
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