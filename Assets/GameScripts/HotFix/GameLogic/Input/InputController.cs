using GameBase;
using GameFramework.Runtime;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace GameLogic
{
    public class InputController : TSingleton<InputController>,IControllerBase,ILogicSys
    {
        float inputY;
        float inputX;
        private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction fireAction;

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
            playerInput = new PlayerInput();
            moveAction = playerInput.Player.Move;
            moveAction.Enable();

            moveAction.started += StartMove;
            moveAction.canceled += CancelMove;

            fireAction = playerInput.Player.Fire;
            fireAction.Enable();

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
            if (isInputMoving)
            {
                Vector2 moveDirection = Vector2.zero;
                moveDirection = moveAction.ReadValue<Vector2>();
                PlayerController.Instance.OnInputMove(moveDirection);
            }
        }

        bool isInputMoving = false;

        private void StartMove(InputAction.CallbackContext context) 
        {
            isInputMoving = true;
            PlayerController.Instance.PlayerStartMove();
        }

        private void CancelMove(InputAction.CallbackContext context)
        {
            isInputMoving = false;
            PlayerController.Instance.PlayerStopMove();
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