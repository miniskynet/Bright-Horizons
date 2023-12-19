using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

       if(Input.GetButtonDown("Jump"))
       {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
       }
       UpdateAnimation();

    }

    private void UpdateAnimation()
    {
     if(dirX > 0f)
       {
            anim.SetBool("Running", true);
            sprite.flipX = false;
       }
       else if (dirX < 0f)
       {
            anim.SetBool("Running", true);
            sprite.flipX = true;
       }
       else
       {
            anim.SetBool("Running", false);
       }
    }

}
