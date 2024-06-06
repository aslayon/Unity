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
    float approachDistance = 5f; // �÷��̾�� �����ϱ� ���� �Ÿ�
    
    Vector3 targetPosition; // ��ǥ ��ġ ���� �߰�
    float smoothTime = 0.5f; // �ε巯�� �̵��� ���� �ð�
    Vector3 velocity = Vector3.zero; // ���� �ӵ�

    // �߰��� ����
    float timeBetweenJumps = 2f; // ���� ���� ����
    float jumpTimer = 0f; // ������ ���� Ÿ�̸�

    void Start()
    {
        this.monster = GameObject.Find("monster");
        this.player = GameObject.Find("player");
        targetPosition = transform.position; // �ʱ� ��ǥ ��ġ ����
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < approachDistance)
        {
            // �÷��̾�� ����
            targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            // ���� �̵�
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // ��ǥ ��ġ�� �����ϸ� ���ο� ��ǥ ��ġ ����
                targetPosition = GetRandomPosition();
            }
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // ȭ�� ������ ������ �ʵ��� ����
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);

        // ���� Ÿ�̸� ������Ʈ
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= timeBetweenJumps)
        {
            // ���� Ÿ�̸Ӱ� ������ ������ ������ ���� ����
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTimer = 0f; // Ÿ�̸� �ʱ�ȭ
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

        float randomX = Random.Range(-screenBounds.x, screenBounds.x);// ȭ�� ���� ���� ��ġ ����.
        randomPosition.x = Mathf.Clamp(randomPosition.x + randomX, -screenBounds.x, screenBounds.x);

        return randomPosition;
    }
}
