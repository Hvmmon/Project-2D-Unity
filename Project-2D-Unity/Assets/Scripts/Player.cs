using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D body2D;
    [HideInInspector] [SerializeField] new SpriteRenderer renderer;

    [SerializeField]
    protected float runSpeed = 10f;

    string currentMove;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        bool go_left = Input.GetKey(KeyCode.LeftArrow);
        bool go_right = Input.GetKey(KeyCode.RightArrow);
        bool go_up = Input.GetKey(KeyCode.UpArrow);
        bool go_down = Input.GetKey(KeyCode.DownArrow);

        if (go_left)
        {
            body2D.velocity = new Vector2(-runSpeed, 0);
            currentMove = "go_left";
            animator.Play("player_go_left");
        }
        else
        if (go_right)
        {
            body2D.velocity = new Vector2(runSpeed, 0);
            currentMove = "go_right";
            animator.Play("player_go_right");
        }
        else
        if (go_up) 
        {
            body2D.velocity = new Vector2(0, runSpeed);
            currentMove = "go_up";
            animator.Play("player_go_up");
        }
        else
        if (go_down)
        {
            body2D.velocity = new Vector2(0, -runSpeed);
            currentMove = "go_down";
            animator.Play("player_go_down");
        }
        else
        {
            switch (currentMove)
            {
                case "go_left":
                    animator.Play("player_left_standing");
                    break;
                case "go_right":
                    animator.Play("player_right_standing");
                    break;
                case "go_up":
                    animator.Play("player_up_standing");
                    break;

                default:
                    animator.Play("player_down_standing");
                    break;
            }
        }

        /*
        if (go_left || go_right || go_up || go_down)
        {
            if (go_left)
            {
                body2D.velocity = new Vector2(-runSpeed, body2D.velocity.y);
                if (!go_up)
                    animator.Play("player_run");
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (go_right)
            {
                body2D.velocity = new Vector2(runSpeed, body2D.velocity.y);
                if (!go_up)
                    animator.Play("player_run");
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            animator.Play("player_down_standing");
            body2D.velocity = new Vector2(0, body2D.velocity.y);
        }
        */
    }
}
