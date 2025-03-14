using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    [Range(0, 200)] public int startHealth = 100, currentHealth;
    public static bool isDead;
    public float attackRange = 50f;        // ���� �Ÿ�
    public LayerMask Zombie;              // ���� ���Ե� ���̾�
    public Color rayColor = Color.red;    // Ray ����� ����
    public float rayStartOffset = 1f;     // Ray ������ �÷��̾� ���� ������
    public float moveSpeed = 5f;          // �̵� �ӵ�
    public float lineDuration = 1f;       // LineRenderer ���� �ð�

    public LineRenderer lineRenderer;     // Ray�� �ð������� ǥ���ϱ� ���� LineRenderer

    void Start()
    {
        // LineRenderer ������Ʈ Ȯ�� �� �ʱ�ȭ
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = rayColor;
        lineRenderer.endColor = rayColor;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false; // �ʱ⿡�� ��Ȱ��ȭ
        currentHealth = startHealth;
    }

    void Update()
    {
        PlayerMovement();

        if (Input.GetMouseButtonDown(0)) // ���� ���콺 Ŭ��
        {
            Shoot();
        }

        if(currentHealth <= 00 && !isDead)
        {
            isDead = true;
            Debug.Log("The player has died!");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
    }

    private void PlayerMovement()
    {
        // WASD Ű�� �÷��̾� �̵� ó��
        float moveX = Input.GetAxis("Horizontal"); // A, D Ű �Է� (-1 ~ 1)
        float moveZ = Input.GetAxis("Vertical");   // W, S Ű �Է� (-1 ~ 1)

        // �̵� ���� ���
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ);

        // �̵� ����
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void Shoot()
    {
        RaycastHit hit;

        // Ray ������: �÷��̾� ��ġ���� �ణ ������ �̵�
        Vector3 rayOrigin = transform.position + transform.forward * rayStartOffset;

        // Ray ����: �÷��̾��� ���� ����
        Vector3 rayDirection = transform.forward;

        // ���� Raycast ó��
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, attackRange, Zombie))
        {
            // �浹�� ��ü���� ZombieDamage ��ũ��Ʈ ��������
            Zombie zombie = hit.collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                // ���񿡰� ���� ����
                zombie.TakeDamage(hit.point, hit.normal);
                Debug.Log($"Ray�� {hit.collider.gameObject.name}�� ����!");
            }

            // LineRenderer�� Ray�� ǥ��
            lineRenderer.SetPosition(0, rayOrigin);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            Debug.Log("Ray�� �ƹ��͵� ������ ���߽��ϴ�.");

            // Ray�� ���� ���� ��� LineRenderer�� �ִ� �Ÿ����� ǥ��
            lineRenderer.SetPosition(0, rayOrigin);
            lineRenderer.SetPosition(1, rayOrigin + rayDirection * attackRange);
        }

        // LineRenderer Ȱ��ȭ �� ��Ȱ��ȭ �ڷ�ƾ ȣ��
        StartCoroutine(DisableLineAfterDelay());
    }

    private IEnumerator DisableLineAfterDelay()
    {
        // LineRenderer Ȱ��ȭ
        lineRenderer.enabled = true;

        // ������ ���� �ð� ���� ���
        yield return new WaitForSeconds(lineDuration);

        // LineRenderer ��Ȱ��ȭ
        lineRenderer.enabled = false;
    }
}
