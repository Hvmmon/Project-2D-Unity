using UnityEngine;

public class Enemy: MonoBehaviour
{
    protected GameObject player;

    protected Transform transform;
    protected Animator animator;
    protected Rigidbody2D body2D;
    protected BoxCollider2D collider2D;
    [HideInInspector] [SerializeField] new SpriteRenderer renderer;

    [SerializeField]
    protected float runSpeed = 3f;

    protected string currentMove;
    protected int death;
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

        death = 0;
        attack = 0;
    }
    // -------------------------------------------------------------------------
    // update is called once per frame
    public void FixedUpdate()
    {
        if (death == 0)
        {
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
            death = death + 1;
            if (death > 50)
                Destroy(gameObject);
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
    public void IsKilled()
    {
        Debug.Log("enemy is killed");
        animator.Play("hurt");
        death = 1;
    }
    // -------------------------------------------------------------------------
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player)
        {
            // if hit by player, let attack
            if (death == 0)
            {
                attack = 20;
            }
        }
    }
}