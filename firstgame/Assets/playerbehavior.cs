using UnityEngine;

public class playerbehavior : MonoBehaviour
{
    float moveSpeed = 5f; // 이동 속도
    float jumpForce = 10f; // 점프 힘
    private Vector3 attackPoint; // 공격 지점
    public GameObject attackPrefab; // 공격 프리팹
    private Camera mainCamera;
    Rigidbody2D rb;
    bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 플레이어 이동
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // 공격
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 클릭했을 때
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Attack(mousePos);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.CompareTag("attack"))//플레이어와의 충돌은 무시
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            }
            isGrounded = false;
        }
    }

    void Attack(Vector3 targetPosition)
    {
        // 플레이어의 위치에서 마우스 클릭 지점까지의 방향 설정
        Vector3 tmp = transform.position;
        
        Vector2 direction = (targetPosition - transform.position).normalized;
        GameObject attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<Rigidbody2D>().velocity = direction * 50f; // 공격 이동 속도
        Destroy(attack, 2f); // 공격이 일정 시간 후에 사라지도록 설정
    }

    

}