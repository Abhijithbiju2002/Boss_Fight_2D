using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public bool canFlip;
    public bool isFlipped = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canFlip == true)
        {
            LookAtPlayer();
        }

    }
    public void LookAtPlayer()
    {
        Vector3 localScale = transform.localScale;


        if (transform.position.x > player.position.x && !isFlipped)
        {
            animator.SetTrigger("Idel");
            localScale.x *= -1f;

            transform.localScale = localScale;
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            animator.SetTrigger("Idel");
            localScale.x *= -1f;
            transform.localScale = localScale;
            isFlipped = false;
        }
    }



}
