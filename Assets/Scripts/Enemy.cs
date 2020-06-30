using UnityEngine;


public class Enemy : MonoBehaviour
{
    

    [SerializeField] private float speed;
    [SerializeField] private float distanceToMovement;

    private Rigidbody2D enemyRb;
    private SpriteRenderer enemyRenderer;
    private BoxCollider2D enemyCollider;

    private Vector3 startPoint;
    private Vector3 endPoint;
    

    private bool onEndPoint = false;
    

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<BoxCollider2D>();

        startPoint = transform.position;
        endPoint = new Vector2(transform.position.x + distanceToMovement, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyRb.position.x < endPoint.x && enemyRb.position.x <= startPoint.x)
            onEndPoint = false;
        else if (enemyRb.position.x > startPoint.x && enemyRb.position.x >= endPoint.x)
            onEndPoint = true;
        

    }

    private void FixedUpdate()
    { 

        if (!onEndPoint) 
        {
            enemyRb.position = new Vector2(enemyRb.position.x + speed * Time.fixedDeltaTime, enemyRb.position.y);
            enemyRenderer.flipX = false;
        }
        else
        {
            enemyRb.position = new Vector2(enemyRb.position.x - speed * Time.fixedDeltaTime, enemyRb.position.y);
            enemyRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
            AttackPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            EnemyDeath();
        }
            
    }


    public void EnemyDeath()
    {
        enemyRb.AddForce(transform.up * 20, ForceMode2D.Impulse);
        
        enemyCollider.isTrigger = true;
        speed = 0;
        enemyRb.gravityScale = 20f;
        Destroy(gameObject, 4f);
    }

    private void AttackPlayer()
    {
        Player.playerIsDeath.Invoke();
    }

}
