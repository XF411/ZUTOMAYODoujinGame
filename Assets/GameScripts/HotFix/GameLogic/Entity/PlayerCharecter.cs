using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// 玩家角色
    /// </summary>
    public class PlayerCharecter : CharecterEnity
    {

        private Animator animator;
        public bool isMoving { get; private set; }//TODO 暂时先用这个变量来控制移动,后面改成有限状态机
        public Vector2 TargetGridPosition { get; private set; }//要移动的目标位置
        public Vector2 CurrentGridPosition { get; private set; }//角色当前所在位置

        public Vector2 MoveDirection;//移动方向

        Coroutine moveCoroutine;

        public float MoveSpeed { get; private set; }

        public void Init() 
        {
            animator = GetComponent<Animator>();
            TargetGridPosition = Vector2.zero;
            CurrentGridPosition = Vector2.zero;//TODO 从当前存档读取玩家位置
            transform.position = CurrentGridPosition;//TODO 根据存档数据来设置角色模型位置
            MoveSpeed = 4f;
        }

        public void StartMove() 
        {
            isMoving = true;
            if (moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(PlayerMove());
            }
        }

        public void StopMove() 
        {
            isMoving = false;
            if (moveCoroutine != null) 
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = null;
            OnStopMoving();
        }

        IEnumerator PlayerMove() 
        {
            while (isMoving) 
            {
                OnMoving();
                Log.Debug(gameObject.name + TargetGridPosition.ToVector3() + " on move");
                transform.position = Vector2.MoveTowards(transform.position, TargetGridPosition, MoveSpeed * Time.deltaTime);
                CurrentGridPosition = transform.position;
                yield return null;
            }
        }


        public void Move2TargetPosition(Vector2 targetPosition) 
        {
            //TODO 判断目标位置是否合法，是否可以移动
            TargetGridPosition = targetPosition;//设置要移动的目标位置
            MoveDirection = TargetGridPosition - CurrentGridPosition;
        }

        private void OnMoving() 
        {
            //移动的动画播放表现逻辑
            animator.SetBool("isWalk", true);
            animator.SetFloat("X", MoveDirection.x);
            animator.SetFloat("Y", MoveDirection.y);
        }

        private void OnStopMoving() 
        {
            animator.SetBool("isWalk", false);
            animator.SetFloat("X", MoveDirection.x);
            animator.SetFloat("Y", MoveDirection.y);
        }
    }
}
