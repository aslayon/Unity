using UnityEngine;

public class playerbehavior : MonoBehaviour
{
    float moveSpeed = 5f; // �̵� �ӵ�
    float jumpForce = 10f; // ���� ��
    private Vector3 attackPoint; // ���� ����
    public GameObject attackPrefab; // ���� ������
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
        // �÷��̾� �̵�
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // ����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // ����
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� Ŭ������ ��
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
            if (collision.gameObject.CompareTag("attack"))//�÷��̾���� �浹�� ����
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            }
            isGrounded = false;
        }
    }

    void Attack(Vector3 targetPosition)
    {
        // �÷��̾��� ��ġ���� ���콺 Ŭ�� ���������� ���� ����
        Vector3 tmp = transform.position;
        
        Vector2 direction = (targetPosition - transform.position).normalized;
        GameObject attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<Rigidbody2D>().velocity = direction * 50f; // ���� �̵� �ӵ�
        Destroy(attack, 2f); // ������ ���� �ð� �Ŀ� ��������� ����
    }

    

}