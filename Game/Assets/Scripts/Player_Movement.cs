using UnityEngine;

public class Player_Movement : MonoBehaviour
{
   private Rigidbody2D rb;
   private BoxCollider2D coll;
   private SpriteRenderer sprite;
   private Animator anim;

   [SerializeField] private LayerMask jumpableGround;

   private float dirX = 0f;
   [SerializeField] private float moveSpeed = 7f;
   [SerializeField] private float jumpForce = 14f;

   private enum MovementState { Idle, Running, Jumping, Falling };

   [SerializeField] private AudioSource jumpSoundEffect;

   private void Start()
   {
      rb = GetComponent<Rigidbody2D>();
      coll = GetComponent<BoxCollider2D>();
      sprite = GetComponent<SpriteRenderer>();
      anim = GetComponent<Animator>();
   }

   private void Update()
   {
      //move the player left or right based on key press
      dirX = Input.GetAxisRaw("Horizontal");
      rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

      if (Input.GetButtonDown("Jump") && IsGrounded())
      {
         //make the player character jump
         //if player is in jumpable ground and the jump button is pressed
         jumpSoundEffect.Play();
         rb.velocity = new Vector2(rb.velocity.x, jumpForce);
      }
      UpdateAnimation();

   }

   private void UpdateAnimation()
   {

      MovementState state;

      //change sprite and animation based on movement
      //i.e. running or idle
      if (dirX > 0f)
      {
         state = MovementState.Running;
         sprite.flipX = false;
      }
      else if (dirX < 0f)
      {
         state = MovementState.Running;
         sprite.flipX = true;
      }
      else
      {
         state = MovementState.Idle;
      }

      //if the player character is moving horizontally
      //i.e. moving up or down, change the state to either jumping or falling
      if (rb.velocity.y > .1f)
      {
         state = MovementState.Jumping;
      }
      else if (rb.velocity.y < -.1f)
      {
         state = MovementState.Falling;
      }

      anim.SetInteger("State", (int)state);
   }

   private bool IsGrounded()
   {
      //check whether the player character is in contact with jumpable ground
      return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
   }

}
