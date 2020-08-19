using UnityEngine;

public class Enemy: MonoBehaviour
{
    protected GameObject player;

    private new Transform transform;
    protected Animator animator;
    protected Rigidbody2D body2D;
    protected new BoxCollider2D collider2D;
    protected float initial_x, initial_y;
    [HideInInspector] [SerializeField] new SpriteRenderer renderer;

    [SerializeField]
    protected float runSpeed = 3f;

    [SerializeField]
    protected GameObject _cloudParticlePrefab;

    [SerializeField]
    protected int lives = 1;                        // number of reincarnation

    [SerializeField]
    protected int blood = 1;                        // enemy run out of blood -> lost 1 lives
    protected int initial_blood;

    protected string currentMove;
    protected int hurt;                         // when collide by player who's attack
    protected int attack;

    // -------------------------------------------------------------------------
    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.FindWithTag("Player");

        renderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        initial_x = transform.position.x;
        initial_y = transform.position.y;

        hurt = 0;
        attack = 0;
        initial_blood = blood;
    }
    // -------------------------------------------------------------------------
    // update is called once per frame
    public void FixedUpdate()
    {
        if (!player)
        {
            // if player is already dead, donothing
            return;
        }

        // if player is not dead, update the position of enemy such that it approach the player's posistion
        if (hurt == 0)
        {
            // if enemy is alive
            if (attack == 0)
            {
                float player_x = player.transform.position.x;
                float player_y = player.transform.position.y;

                float current_x = transform.position.x;
                float current_y = transform.position.y;

                // to let zombie attack player
                bool go_up = false, go_down = false, go_left = false, go_right = false;

                if (player_x > current_x + 0.1)
                {
                    go_right = true;
                }

                if (player_x < current_x - 0.1)
                {
                    go_left = true;
                }

                if (player_y > current_y + 0.1)
                {
                    go_up = true;
                }
                if (player_y < current_y - 0.1)
                {
                    go_down = true;
                }

                Move(go_up, go_down, go_left, go_right);
            }
            else
            {
                attack = attack - 1;
            }
        }
        else
        {
            hurt = hurt + 1;
            if (blood > 0)
            {
                if (hurt > 20)
                {
                    // Destroy(gameObject);
                    hurt = 0;
                    attack = 0;
                    blood = blood - 1;
                    // transform.position = new Vector3(initial_x, initial_y, 0);
                }
            }
            else
            {
                if (hurt > 50)
                {
                    hurt = 0;
                    attack = 0;
                    blood = initial_blood;
                    lives = lives - 1;

                    GameObject explosion = Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
                    Destroy(explosion, 3);

                    if (lives < 0)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        transform.position = new Vector3(initial_x, initial_y, 0);
                    }
                }
            }
        }
    }
    // -------------------------------------------------------------------------
    public void Update()
    {
        if (hurt > 0 && blood > 0)
        {
            switch (currentMove)
            {
                case "go_left":
                    transform.position = new Vector3(transform.position.x + (float)0.05, 
                                                     transform.position.y, 
                                                     transform.position.z);
                    break;
                case "go_right":
                    transform.position = new Vector3(transform.position.x - (float)0.05, 
                                                     transform.position.y, 
                                                     transform.position.z);
                    break;
                case "go_up":
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y - (float)0.05,
                                                     transform.position.z);
                    break;
                case "go_down":
                    transform.position = new Vector3(transform.position.x,
                                                     transform.position.y + (float)0.05,
                                                     transform.position.z);
                    break;
                case "default":
                    break;
            }
        }
    }
    // -------------------------------------------------------------------------
    void Move(bool go_up, bool go_down, bool go_left, bool go_right)
    {
        if (go_left)
        {
            body2D.velocity = new Vector2(-runSpeed, 0);
            animator.Play("move_left");

            currentMove = "go_left";
        }
        else
        if (go_right)
        {
            body2D.velocity = new Vector2(runSpeed, 0);
            animator.Play("move_right");

            currentMove = "go_right";
        }
        else
        if (go_up)
        {
            body2D.velocity = new Vector2(0, runSpeed);
            animator.Play("move_up");

            currentMove = "go_up";
        }
        else
        if (go_down)
        {
            body2D.velocity = new Vector2(0, -runSpeed);
            animator.Play("move_down");

            currentMove = "go_down";
        }
    }
    // -------------------------------------------------------------------------
    public void IsHurt(string currentPlayerMove)
    {
        Debug.Log("enemy is hurt");
        Debug.Log("current player move: " + currentMove);
        animator.Play("hurt");
        hurt = 1;
    }
    // -------------------------------------------------------------------------
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player)
        {
            // if hit by player, let attack
            if (hurt == 0)
            {
                attack = 20;
            }
        }
    }
}