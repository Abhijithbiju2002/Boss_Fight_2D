using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    Animator animator;
    Rigidbody2D Rigidbody;
    [SerializeField] Vector2 deathKick = new Vector2(2f, 2f);



    private void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(20);
        }
    }
    void Die()
    {
        animator.SetTrigger("Death");
        GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine(DeathKickWithDelay());
        StartCoroutine(WaitAndReload());
    }

    IEnumerator DeathKickWithDelay()
    {
        yield return new WaitForSeconds(0.01f);
        Rigidbody.velocity = Vector2.zero;
        Rigidbody.AddForce(deathKick, ForceMode2D.Impulse);
    }
    IEnumerator WaitAndReload()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);



    }
}