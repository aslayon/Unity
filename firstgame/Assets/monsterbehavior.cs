using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monsterbehavior : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce = 5f;
    GameObject monster;
    GameObject player;
    float approachDistance = 5f; // 플레이어에게 접근하기 위한 거리
    
    Vector3 targetPosition; // 목표 위치 변수 추가
    float smoothTime = 0.5f; // 부드러운 이동을 위한 시간
    Vector3 velocity = Vector3.zero; // 현재 속도

    // 추가된 변수
    float timeBetweenJumps = 2f; // 점프 간격 설정
    float jumpTimer = 0f; // 점프를 위한 타이머

    void Start()
    {
        this.monster = GameObject.Find("monster");
        this.player = GameObject.Find("player");
        targetPosition = transform.position; // 초기 목표 위치 설정
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < approachDistance)
        {
            // 플레이어에게 접근
            targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            // 랜덤 이동
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // 목표 위치에 도달하면 새로운 목표 위치 설정
                targetPosition = GetRandomPosition();
            }
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // 화면 밖으로 나가지 않도록 제한
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);

        // 점프 타이머 업데이트
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= timeBetweenJumps)
        {
            // 점프 타이머가 설정된 간격을 넘으면 점프 실행
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTimer = 0f; // 타이머 초기화
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<gamedirector>().DecreaseHp();
        }
    }
    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = transform.position;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, 0));

        float randomX = Random.Range(-screenBounds.x, screenBounds.x);// 화면 내부 랜덤 위치 설정.
        randomPosition.x = Mathf.Clamp(randomPosition.x + randomX, -screenBounds.x, screenBounds.x);

        return randomPosition;
    }
}
