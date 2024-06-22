using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameLogic
{
    /// <summary>
    /// ��ҽ�ɫ
    /// </summary>
    public class PlayerCharecter : CharecterEnity
    {

        private Animator animator;
        public bool isMoving { get; private set; }//TODO ��ʱ������������������ƶ�,����ĳ�����״̬��
        public Vector2 TargetGridPosition { get; private set; }//Ҫ�ƶ���Ŀ��λ��
        public Vector2 CurrentGridPosition { get; private set; }//��ɫ��ǰ����λ��

        public void Init() 
        {
            animator = GetComponent<Animator>();
            TargetGridPosition = Vector2.zero;
            CurrentGridPosition = Vector2.zero;//TODO �ӵ�ǰ�浵��ȡ���λ��
            transform.position = CurrentGridPosition;//TODO ���ݴ浵���������ý�ɫģ��λ��
        }

        public void Move(Vector2 targetPosition) 
        {
            if (isMoving) return;
            //TODO �ж�Ŀ��λ���Ƿ�Ϸ�
            TargetGridPosition = targetPosition;//����Ҫ�ƶ���Ŀ��λ��
            OnMove();
        }

        public void OnMove() 
        {

            //�ƶ��ı����߼�
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
                // ���÷��򶯻�����
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
