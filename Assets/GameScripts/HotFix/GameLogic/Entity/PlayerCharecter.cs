using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// ��ҽ�ɫ
    /// </summary>
    public class PlayerCharecter : CharecterEnity
    {

        private Animator animator;
        private bool isMoving = false;//TODO ��ʱ������������������ƶ�,����ĳ�����״̬��
        private Vector2 targetGridPosition;

        public void Init() 
        {
            animator = GetComponent<Animator>();
        }

        public void Move(Vector2 moveV2) 
        {
            if (isMoving) return;
            //TODO ��������,��������ƶ�����ȵ�
            targetGridPosition = new Vector2(Mathf.Round(transform.position.x + moveV2.x), Mathf.Round(transform.position.y + moveV2.y));
            OnMove(moveV2);
        }

        public void OnMove(Vector2 moveV2) 
        {

            //�ƶ��ı����߼�
            isMoving = true;
            animator.SetBool("isWalk", true);
            animator.SetFloat("X", moveV2.x);
            animator.SetFloat("Y", moveV2.y);

            transform.DOMove(targetGridPosition, 0.5f).OnComplete(() =>
            {
                // �������������÷��򶯻��Ȳ���
                isMoving = false;
                animator.SetBool("isWalk", false);

                //animator.SetFloat("X", 0);
                //animator.SetFloat("Y", 0);
            });
        }
    }
}
