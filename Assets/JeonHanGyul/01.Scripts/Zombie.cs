using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public GameObject bloodEffectPrefab;    // 피 이펙트 프리팹


    private void Awake()
    {
       
    }

    private void Update()
    {
    }
    

 

    /// <summary>
    /// 좀비가 피해를 입을 때 호출
    /// </summary>
    /// <param name="hitPoint">충돌 지점</param>
    /// <param name="hitNormal">충돌 표면의 방향</param>
    public void TakeDamage(Vector3 hitPoint, Vector3 hitNormal)
    {
        // 맞은 위치에 피 이펙트를 생성
        SpawnBloodEffect(hitPoint, hitNormal);
    }

    /// <summary>
    /// 피 이펙트를 생성하는 함수
    /// </summary>
    /// <param name="position">파티클 생성 위치</param>
    /// <param name="normal">파티클 방향</param>
    private void SpawnBloodEffect(Vector3 position, Vector3 normal)
    {
        // 프리팹에서 파티클을 생성
        GameObject bloodEffect = Instantiate(bloodEffectPrefab, position, Quaternion.LookRotation(normal));

        // 파티클이 끝나면 자동으로 삭제
        ParticleSystem particle = bloodEffect.GetComponent<ParticleSystem>();
        if (particle != null)
        {
            // 파티클 지속 시간 후 삭제
            Destroy(bloodEffect, particle.main.duration + particle.main.startLifetime.constantMax);
        }
        else
        {
            // 파티클이 아닌 경우 일정 시간 후 삭제
            Destroy(bloodEffect, 2f);
        }
    }

}

