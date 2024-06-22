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

        public void Init() 
        {
            animator = GetComponent<Animator>();
            TargetGridPosition = Vector2.zero;
            CurrentGridPosition = Vector2.zero;//TODO 从当前存档读取玩家位置
            transform.position = CurrentGridPosition;//TODO 根据存档数据来设置角色模型位置
        }

        public void Move(Vector2 targetPosition) 
        {
            if (isMoving) return;
            //TODO 判断目标位置是否合法
            TargetGridPosition = targetPosition;//设置要移动的目标位置
            OnMove();
        }

        public void OnMove() 
        {

            //移动的表现逻辑
            isMoving = true;
            Vector2 moveV2 = TargetGridPosition - (Vector2)transform.position;
            animator.SetBool("isWalk", true);
            animator.SetFloat("X", moveV2.x);
            animator.SetFloat("Y", moveV2.y);
            Log.Debug(gameObject.name + moveV2 + " on move");
            transform.DOMove(TargetGridPosition, 0.2f).OnComplete(() =>
            {
                CurrentGridPosition = TargetGridPosition;
                isMoving = false;
                // 重置方向动画参数
                animator.SetFloat("X", moveV2.x);
                animator.SetFloat("Y", moveV2.y);
            });
        }

        private void Update()
        {
            animator.SetBool("isWalk", isMoving);
        }
    }
}
