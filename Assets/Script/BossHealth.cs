using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int boss_health = 500;

    [SerializeField] Vector2 boss_death_kick = new Vector2(4f, 4f);

    Animator animator;
    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void BossTakeDamage(int damage)
    {
        boss_health -= damage;
        if (boss_health <= 0)
        {
            BossDeath();
        }
    }
    void BossDeath()
    {
        animator.SetTrigger("Death");
        GetComponent<BossWeapon>().enabled = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(boss_death_kick, ForceMode2D.Impulse);

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false; // stops all physics interactions

        StartCoroutine(DisableColliderAfterDelay());
    }
    IEnumerator DisableColliderAfterDelay()
    {
        yield return new WaitForSeconds(1f); // wait for death animation
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = false;
        }
    }



}
