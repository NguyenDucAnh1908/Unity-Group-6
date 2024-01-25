using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die() 
    {
        _animator.SetTrigger("death");
    }

    private void RestarLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu vị trí Y của nhân vật là -10
        if (transform.position.y <= -10f)
        {
            // Gọi hàm RestartLevelS
            RestarLevel();
        }
    }
}
