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

        public Vector2 MoveDirection;//�ƶ�����

        Coroutine moveCoroutine;

        public float MoveSpeed { get; private set; }

        public void Init() 
        {
            animator = GetComponent<Animator>();
            TargetGridPosition = Vector2.zero;
            CurrentGridPosition = Vector2.zero;//TODO �ӵ�ǰ�浵��ȡ���λ��
            transform.position = CurrentGridPosition;//TODO ���ݴ浵���������ý�ɫģ��λ��
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
            //TODO �ж�Ŀ��λ���Ƿ�Ϸ����Ƿ�����ƶ�
            TargetGridPosition = targetPosition;//����Ҫ�ƶ���Ŀ��λ��
            MoveDirection = TargetGridPosition - CurrentGridPosition;
        }

        private void OnMoving() 
        {
            //�ƶ��Ķ������ű����߼�
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
