using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool isSliding = false;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra xem đang trượt không
        if (isSliding)
        {
            // Ví dụ: Di chuyển xuống theo trục y
            rb.velocity = new Vector2(0, -5f);
        }
    }


    // Xử lý va chạm
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Bắt đầu trạng thái trượt
            isSliding = true;

            // Disable Collider để không tương tác được
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
