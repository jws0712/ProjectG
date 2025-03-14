using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    [Range(0, 200)] public int startHealth = 100, currentHealth;
    public static bool isDead;
    public float attackRange = 50f;        // 공격 거리
    public LayerMask Zombie;              // 좀비가 포함된 레이어
    public Color rayColor = Color.red;    // Ray 디버깅 색상
    public float rayStartOffset = 1f;     // Ray 시작점 플레이어 앞쪽 오프셋
    public float moveSpeed = 5f;          // 이동 속도
    public float lineDuration = 1f;       // LineRenderer 지속 시간

    public LineRenderer lineRenderer;     // Ray를 시각적으로 표시하기 위한 LineRenderer

    void Start()
    {
        // LineRenderer 컴포넌트 확인 및 초기화
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
        lineRenderer.enabled = false; // 초기에는 비활성화
        currentHealth = startHealth;
    }

    void Update()
    {
        PlayerMovement();

        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 클릭
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
        // WASD 키로 플레이어 이동 처리
        float moveX = Input.GetAxis("Horizontal"); // A, D 키 입력 (-1 ~ 1)
        float moveZ = Input.GetAxis("Vertical");   // W, S 키 입력 (-1 ~ 1)

        // 이동 방향 계산
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ);

        // 이동 실행
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void Shoot()
    {
        RaycastHit hit;

        // Ray 시작점: 플레이어 위치에서 약간 앞으로 이동
        Vector3 rayOrigin = transform.position + transform.forward * rayStartOffset;

        // Ray 방향: 플레이어의 전방 방향
        Vector3 rayDirection = transform.forward;

        // 실제 Raycast 처리
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, attackRange, Zombie))
        {
            // 충돌한 객체에서 ZombieDamage 스크립트 가져오기
            Zombie zombie = hit.collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                // 좀비에게 피해 전달
                zombie.TakeDamage(hit.point, hit.normal);
                Debug.Log($"Ray가 {hit.collider.gameObject.name}에 명중!");
            }

            // LineRenderer로 Ray를 표시
            lineRenderer.SetPosition(0, rayOrigin);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            Debug.Log("Ray가 아무것도 맞추지 못했습니다.");

            // Ray가 맞지 않을 경우 LineRenderer를 최대 거리까지 표시
            lineRenderer.SetPosition(0, rayOrigin);
            lineRenderer.SetPosition(1, rayOrigin + rayDirection * attackRange);
        }

        // LineRenderer 활성화 및 비활성화 코루틴 호출
        StartCoroutine(DisableLineAfterDelay());
    }

    private IEnumerator DisableLineAfterDelay()
    {
        // LineRenderer 활성화
        lineRenderer.enabled = true;

        // 설정한 지속 시간 동안 대기
        yield return new WaitForSeconds(lineDuration);

        // LineRenderer 비활성화
        lineRenderer.enabled = false;
    }
}
