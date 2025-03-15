namespace Agent.Player
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;

    //Unity
    using UnityEngine;

    public class PlayerController : Character
    {
        //추후 스크립터블 오브젝트로 변경 가능성 있음
        private float currentHp = default;
        private float maxHp = default;
        private float moveSpeed = default;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            moveSpeed = 5;
            currentHp = maxHp;
        }

        private void Update()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputZ = Input.GetAxisRaw("Vertical");

            Vector3 moveVec = new Vector3(inputX, 0, inputZ).normalized;

            rb.velocity = moveVec * moveSpeed;
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


