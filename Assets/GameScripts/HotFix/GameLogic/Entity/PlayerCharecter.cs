using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// 玩家角色
    /// </summary>
    public class PlayerCharecter : CharecterEnity
    {

        private Animator animator;
        private bool isMoving = false;//TODO 暂时先用这个变量来控制移动,后面改成有限状态机
        private Vector2 targetGridPosition;

        public void Init() 
        {
            animator = GetComponent<Animator>();
        }

        public void Move(Vector2 moveV2) 
        {
            if (isMoving) return;
            //TODO 其他处理,计算具体移动距离等等
            targetGridPosition = new Vector2(Mathf.Round(transform.position.x + moveV2.x), Mathf.Round(transform.position.y + moveV2.y));
            OnMove(moveV2);
        }

        public void OnMove(Vector2 moveV2) 
        {

            //移动的表现逻辑
            isMoving = true;
            animator.SetBool("isWalk", true);
            animator.SetFloat("X", moveV2.x);
            animator.SetFloat("Y", moveV2.y);

            transform.DOMove(targetGridPosition, 0.5f).OnComplete(() =>
            {
                // 可以在这里重置方向动画等操作
                isMoving = false;
                animator.SetBool("isWalk", false);

                //animator.SetFloat("X", 0);
                //animator.SetFloat("Y", 0);
            });
        }
    }
}
