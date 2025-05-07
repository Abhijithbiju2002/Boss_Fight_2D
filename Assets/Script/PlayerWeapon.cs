using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int attack_dmg = 25;
    private float timeBetweenAttack;
    [SerializeField] private float startbetweenAttack = 0.05f;
    [SerializeField] Transform attakPos;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask EnemyLayer;


    public BossHealth boss_health = null;
    public Collider2D[] enemiesToDamage;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        PlayerAttack();
    }
    void PlayerAttack()
    {
        if (timeBetweenAttack <= 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                bool isRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
                animator.ResetTrigger("Attacker");
                animator.ResetTrigger("IdleAttack");

                if (isRunning)
                {
                    animator.SetTrigger("Attacker");
                    //attackRange = 0.5f;
                }
                else
                {
                    animator.SetTrigger("IdleAttack");
                    // attackRange = 0.5f;

                }
                enemiesToDamage = Physics2D.OverlapCircleAll(attakPos.position, attackRange, EnemyLayer);

                foreach (Collider2D enemy in enemiesToDamage)
                {
                    boss_health = enemy.GetComponent<BossHealth>();
                    if (boss_health != null)
                    {
                        boss_health.BossTakeDamage(attack_dmg);
                    }
                }
                timeBetweenAttack = startbetweenAttack;

            }
            //else if (Input.GetMouseButtonDown(1))
            //{
            //    bool isRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
            //    animator.ResetTrigger("Attacker");

            //    if (isRunning)
            //    {
            //        animator.SetTrigger("Attacker");
            //        attackRange = 1f;
            //    }
            //    else
            //    {

            //        attackRange = 0.5f;
            //    }
            //    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attakPos.position, attackRange, EnemyLayer);
            //    foreach (Collider2D enemy in enemiesToDamage)
            //    {
            //        BossHealth boss_health = enemy.GetComponent<BossHealth>();
            //        if (boss_health != null)
            //        {
            //            boss_health.BossTakeDamage(attack_dmg);
            //        }
            //    }


            //timeBetweenAttack = startbetweenAttack;

            //}
            //then u can attack

        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;

            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            {
                animator.ResetTrigger("RunAttack");
                // animator.ResetTrigger("IdleAttack");
                animator.ResetTrigger("Attacker");
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attakPos != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attakPos.position, attackRange);
        }
    }
}
