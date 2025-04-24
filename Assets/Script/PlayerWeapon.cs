using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private float timeBetweenAttack;
    [SerializeField] private float startbetweenAttack = 0.05f;
    [SerializeField] Transform attakPos;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask EnemyLayer;


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
                animator.ResetTrigger("RunAttack");
                animator.ResetTrigger("IdleAttack");

                if (isRunning)
                {
                    animator.SetTrigger("RunAttack");
                    attackRange = 0.35f;
                }
                else
                {
                    animator.SetTrigger("IdleAttack");
                    attackRange = 0.35f;

                }
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attakPos.position, attackRange, EnemyLayer);
                timeBetweenAttack = startbetweenAttack;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                bool isRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
                animator.ResetTrigger("Attacker");

                if (isRunning)
                {
                    animator.SetTrigger("Attacker");
                    attackRange = 0.5f;
                }
                else
                {

                    attackRange = 0.35f;
                }
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attakPos.position, attackRange, EnemyLayer);
                timeBetweenAttack = startbetweenAttack;

            }
            //then u can attack

        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;

            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            {
                animator.ResetTrigger("RunAttack");
                animator.ResetTrigger("IdleAttack");
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
