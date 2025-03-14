namespace Agent.Player
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //Unity
    using UnityEngine;

    public class PlayerController : Character
    {
        //���� ��ũ���ͺ� ������Ʈ�� ���� ���ɼ� ����
        private float currentHp = default;
        private float maxHp = default;

        private void Start()
        {
            currentHp = maxHp;
        }

        private void Update()
        {
            
        }




        protected override void TakeDamge(float damage)
        {
            Debug.Log($"{damage} ������ ����");

            if (currentHp <= 0)
            {
                Die();
            }
        }

        protected override void Die()
        {
            Debug.Log("���");
            Destroy(gameObject);
        }
    }
}


