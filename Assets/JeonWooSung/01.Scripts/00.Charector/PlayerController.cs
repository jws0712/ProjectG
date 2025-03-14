namespace Agent.Player
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //Unity
    using UnityEngine;

    public class PlayerController : Character
    {
        //추후 스크립터블 오브젝트로 변경 가능성 있음
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
            Debug.Log($"{damage} 데미지 받음");

            if (currentHp <= 0)
            {
                Die();
            }
        }

        protected override void Die()
        {
            Debug.Log("사망");
            Destroy(gameObject);
        }
    }
}


