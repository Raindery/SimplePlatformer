using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static UnityEvent playerIsDeath;
    public static UnityEvent OnPlayerWin;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpScale = 2;
    [SerializeField] private LayerMask playerRayNotIgnored;
    [SerializeField] private Sprite[] spritesJump;

    private Rigidbody2D playerRb;
    private SpriteRenderer playerRenderer;
    private Animator playerAnimator;

    private float directionHorizontal;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();

        if (playerIsDeath == null)
            playerIsDeath = new UnityEvent();
        playerIsDeath.AddListener(Death);

        if (OnPlayerWin == null)
            OnPlayerWin = new UnityEvent();
    }

    void Update()
    {
        if (playerRb.velocity.x != 0)
            playerAnimator.Play("PlayerRun");
        else if (playerRb.velocity.x == 0)
            playerAnimator.Play("PlayerIdle");
        

            


        //directionHorizontal = Input.GetAxis("Horizontal") * speed;
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

    }

    private void FixedUpdate()
    {
        Movement();
   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
            playerRb.AddForce(transform.up * 15, ForceMode2D.Impulse);
        if (collision.gameObject.layer == 13)
        {
            OnPlayerWin.Invoke();
        }
            
    }


    private void Movement()
    {
        playerRb.velocity = new Vector2(directionHorizontal * speed, playerRb.velocity.y);

        if (playerRb.velocity.x < 0)
            playerRenderer.flipX = true;
        else if (playerRb.velocity.x > 0)
            playerRenderer.flipX = false;
    }

    public void SetAxis(float directionInput)
    {
        directionHorizontal = directionInput;
    }

    public void Jump()
    {
        if(OnGround())
            playerRb.AddForce(transform.up * jumpScale, ForceMode2D.Impulse);
    }

    private bool OnGround()
    {
        
        return Physics2D.Raycast(transform.position, -transform.up, 0.7f, playerRayNotIgnored).collider != null;
    }

    private void Death()
    {
        Destroy(gameObject);
        
    }


}
