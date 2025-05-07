using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int boss_health = 500;

    [SerializeField] Vector2 boss_death_kick = new Vector2(4f, 4f);
    [SerializeField] Material hitFlash;
    [SerializeField] float duration;
    [SerializeField] Vector2 attackPush = new Vector2(1.5f, 0.5f);

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Material originalMaterial;
    private Coroutine flashRoutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }
    public void BossTakeDamage(int damage)
    {

        Flash();
        rb.AddForce(attackPush, ForceMode2D.Impulse);
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
        StartCoroutine(DeathKickWithDelay());
        gameObject.layer = LayerMask.NameToLayer("DeadBoss");

    }

    void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }
    IEnumerator DeathKickWithDelay()
    {
        yield return new WaitForSeconds(0.01f);
        rb.velocity = Vector2.zero;
        rb.AddForce(boss_death_kick, ForceMode2D.Impulse);
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = hitFlash;
        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;
        flashRoutine = null;
    }


}
