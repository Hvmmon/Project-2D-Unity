using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour
{
    private GameObject player;

    private Transform transform;
    private Animator animator;
    private Rigidbody2D body2D;
    private BoxCollider2D collider2D;
    [HideInInspector] [SerializeField] new SpriteRenderer renderer;

    [SerializeField]
    protected float runSpeed = 3f;

    private string currentMove;
    private int death;
    private int attack;

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

                move(go_up, go_down, go_left, go_right);
            }
            else
            {
                attack = attack - 1;
                switch (currentMove)
                {
                    case "go_left":
                        animator.Play("enemy_zombie_attack_right");
                        renderer.flipX = true;
                        break;
                    case "go_right":
                        animator.Play("enemy_zombie_attack_right");
                        renderer.flipX = false;
                        break;
                    case "go_up":
                        animator.Play("enemy_zombie_attack_up");
                        break;
                    default:
                        animator.Play("enemy_zombie_attack_down");
                        break;
                }
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
    void move(bool go_up, bool go_down, bool go_left, bool go_right)
    {
        if (go_left)
        {
            body2D.velocity = new Vector2(-runSpeed, 0);
            renderer.flipX = true;
            animator.Play("enemy_zombie_move_right");

            currentMove = "go_left";
        }
        else
        if (go_right)
        {
            body2D.velocity = new Vector2(runSpeed, 0);
            renderer.flipX = false;
            animator.Play("enemy_zombie_move_right");

            currentMove = "go_right";
        }
        else
        if (go_up)
        {
            body2D.velocity = new Vector2(0, runSpeed);
            animator.Play("enemy_zombie_move_up");

            currentMove = "go_up";
        }
        else
        if (go_down)
        {
            body2D.velocity = new Vector2(0, -runSpeed);
            animator.Play("enemy_zombie_move_down");

            currentMove = "go_down";
        }
    }
    // -------------------------------------------------------------------------
    public void isKilled()
    {
        Debug.Log("enemy zombie is killed");
        animator.Play("enemy_zombie_hurt");
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
