using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 4f;
    //[SerializeField] private float timer = 1f;
    //public float attackRange2 = 1.5f;

    Transform player;
    Rigidbody2D rigidbody;
    BossController boss;
    BossWeapon BossWeapon;

    PlayerMovement playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BossController>();
        playerController = player.GetComponent<PlayerMovement>();
        BossWeapon = boss.GetComponent<BossWeapon>();

    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rigidbody.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, speed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPos);


        if (!playerController.IsGrounded() && Vector2.Distance(player.position, rigidbody.position) <= attackRange && BossWeapon.attackCoolDown <= 0f)
        {
            //attack

            animator.SetTrigger("Attack");
            boss.canFlip = false;


        }
        else if (Vector2.Distance(player.position, rigidbody.position) <= attackRange && BossWeapon.attackCoolDown <= 0f)
        {

            animator.SetTrigger("Attack2");
            boss.canFlip = false;

        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Attack2");


    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
