using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attack_damage = 20;
    public float attackCoolDown = 1.75f;
    BossController boss;
    // public int engrandAttack = 40;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    private void Start()
    {
        boss = GetComponent<BossController>();
    }
    private void Update()
    {
        if (attackCoolDown > 0f)
        {
            attackCoolDown -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (attackCoolDown <= 0f)
        {

            Vector3 pos = transform.position;
            if (boss.isFlipped)
            {
                pos += transform.right * -attackOffset.x;

            }
            else
            {
                pos += transform.right * attackOffset.x;

            }
            pos += transform.up * attackOffset.y;
            Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);

            if (colInfo != null)
            {
                colInfo.GetComponent<PlayerHealth>().TakeDamage(attack_damage);
            }

        }
        attackCoolDown = 1.75f;


    }
    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Vector3 pos = transform.position + (transform.right * attackOffset.x) + (transform.up * attackOffset.y);
        // Gizmos.DrawWireSphere(pos, attackRange);

        Gizmos.color = Color.red;
        // Try to get BossController in Editor for gizmo preview
        BossController boss = GetComponent<BossController>();
        Vector3 pos = transform.position;

        if (boss != null && boss.isFlipped)
        {
            pos += transform.right * -attackOffset.x;
        }
        else
        {
            pos += transform.right * attackOffset.x;
        }

        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);

    }

}
