using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public GameObject bloodEffectPrefab;    // �� ����Ʈ ������


    private void Awake()
    {
       
    }

    private void Update()
    {
    }
    

 

    /// <summary>
    /// ���� ���ظ� ���� �� ȣ��
    /// </summary>
    /// <param name="hitPoint">�浹 ����</param>
    /// <param name="hitNormal">�浹 ǥ���� ����</param>
    public void TakeDamage(Vector3 hitPoint, Vector3 hitNormal)
    {
        // ���� ��ġ�� �� ����Ʈ�� ����
        SpawnBloodEffect(hitPoint, hitNormal);
    }

    /// <summary>
    /// �� ����Ʈ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="position">��ƼŬ ���� ��ġ</param>
    /// <param name="normal">��ƼŬ ����</param>
    private void SpawnBloodEffect(Vector3 position, Vector3 normal)
    {
        // �����տ��� ��ƼŬ�� ����
        GameObject bloodEffect = Instantiate(bloodEffectPrefab, position, Quaternion.LookRotation(normal));

        // ��ƼŬ�� ������ �ڵ����� ����
        ParticleSystem particle = bloodEffect.GetComponent<ParticleSystem>();
        if (particle != null)
        {
            // ��ƼŬ ���� �ð� �� ����
            Destroy(bloodEffect, particle.main.duration + particle.main.startLifetime.constantMax);
        }
        else
        {
            // ��ƼŬ�� �ƴ� ��� ���� �ð� �� ����
            Destroy(bloodEffect, 2f);
        }
    }

}

