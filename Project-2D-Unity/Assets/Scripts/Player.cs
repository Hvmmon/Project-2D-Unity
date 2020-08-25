using UnityEngine;

public class Player : MonoBehaviour {
    public int blood;
    public GameObject OverMenu;
    protected Animator animator;
    protected Rigidbody2D body2D;
    [HideInInspector] [SerializeField] new SpriteRenderer renderer;

    [SerializeField]
    protected GameObject _cloudParticlePrefab;

    [SerializeField]
    protected float runSpeed = 10f;


    [SerializeField]
    protected HealthBar healthBar;

    [SerializeField]
    protected ScoreScript scoreScript;


    protected string currentMove;
    protected Vector2 isHurt;
    protected bool isAttack;
    // -------------------------------------------------------------------------
    // Use this for initialization
    public void Start () {
        animator = GetComponent<Animator>();
        body2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

        isHurt = new Vector2(0, 0);
        switch (DifficultyValues.Difficulty)
        {
            case DifficultyValues.Difficulties.Normal:
                blood = 4;
                break;
            case DifficultyValues.Difficulties.Hard:
                blood = 2;
                break;
        }

        healthBar.SetMaxHealth(blood + 1);
}
    // -------------------------------------------------------------------------
    // Update is called once per frame
    public void FixedUpdate () {
        if (isHurt.magnitude < 0.1)
        {
            bool go_left = Input.GetKey(KeyCode.LeftArrow);
            bool go_right = Input.GetKey(KeyCode.RightArrow);
            bool go_up = Input.GetKey(KeyCode.UpArrow);
            bool go_down = Input.GetKey(KeyCode.DownArrow);
            bool attack = Input.GetKey(KeyCode.A);

            if (go_left)
            {
                body2D.velocity = new Vector2(-runSpeed, 0);
                currentMove = "go_left";
                animator.Play("go_left");

                isAttack = false;
            }
            else
            if (go_right)
            {
                body2D.velocity = new Vector2(runSpeed, 0);
                currentMove = "go_right";
                animator.Play("go_right");

                isAttack = false;
            }
            else
            if (go_up)
            {
                body2D.velocity = new Vector2(0, runSpeed);
                currentMove = "go_up";
                animator.Play("go_up");

                isAttack = false;
            }
            else
            if (go_down)
            {
                body2D.velocity = new Vector2(0, -runSpeed);
                currentMove = "go_down";
                animator.Play("go_down");

                isAttack = false;
            }
            else
            if (attack)
            {
                isAttack = true;

                switch (currentMove)
                {
                    case "go_left":
                        animator.Play("attack_left");
                        break;
                    case "go_right":
                        animator.Play("attack_right");
                        break;
                    case "go_up":
                        animator.Play("attack_up");
                        break;

                    default:
                        animator.Play("attack_down");
                        break;
                }
            }
            else
            {
                isAttack = false;

                switch (currentMove)
                {
                    case "go_left":
                        animator.Play("left_standing");
                        break;
                    case "go_right":
                        animator.Play("right_standing");
                        break;
                    case "go_up":
                        animator.Play("up_standing");
                        break;

                    default:
                        animator.Play("down_standing");
                        break;
                }
            }
        }
    }
    // -------------------------------------------------------------------------
    public void Update()
    {

        if (blood < 0)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            OverMenu.SetActive(true);


        }

        if (isHurt.x > 0)
        {
            transform.position = new Vector3(transform.position.x + (float) 0.04, transform.position.y, transform.position.z);
            isHurt.x = isHurt.x - 1;
        }
        else
        if (isHurt.x < 0)
        {
            transform.position = new Vector3(transform.position.x - (float) 0.04, transform.position.y, transform.position.z);
            isHurt.x = isHurt.x + 1;
        }
        else
        if (isHurt.y > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (float)0.04, transform.position.z);
            isHurt.y = isHurt.y - 1;
        }
        else
        if (isHurt.y < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (float)0.04, transform.position.z);
            isHurt.y = isHurt.y + 1;
        }

    }
    // -------------------------------------------------------------------------
    public bool AttackWrongDirection(Collision2D collision)
    {
        if (collision.contacts[0].normal.y < 0)
        {
            // the contact force is downward
            // that's mean, enemy hit the player on top

            if (currentMove != "go_up")
            {
                return true;
            }
        }
        else
        if (collision.contacts[0].normal.y > 0)
        {
            // the contact force is upward
            // that's mean, enemy hit the player on the bottom

            if (currentMove != "go_down")
            {
                return true;
            }

        }
        else
        if (collision.contacts[0].normal.x > 0)
        {
            // the contact force is to the right
            // that's mean, enemy hit this player from the left
            if (currentMove != "go_left")
            {
                return true;
            }

        }
        else
        if (collision.contacts[0].normal.x < 0)
        {
            // the contact force is to the left
            // that's mean, enemy hit this player from the right
            if (currentMove != "go_right")
            {
                return true;
            }
        }

        return false;
    }
    // -------------------------------------------------------------------------
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // get objec that hit this enemy
        Enemy hitByEnemy = collision.collider.GetComponent<Enemy>();

        if (hitByEnemy)
        {
            if (isAttack && !AttackWrongDirection(collision))
            {
                hitByEnemy.IsHurt(currentMove);
            }
            else
            {
                if (collision.contacts[0].normal.y < 0)
                {
                    // the contact force is downward
                    // that's mean, enemy is on top of the player

                    isHurt = new Vector2(0, -30);
                    animator.Play("hurt");
                    blood = blood - 1;
                }
                else
                if (collision.contacts[0].normal.y > 0)
                {
                    // the contact force is upward
                    // that's mean, enemy is in the bottom of the player

                    isHurt = new Vector2(0, 30);
                    animator.Play("hurt");
                    blood = blood - 1;

                }
                else
                if (collision.contacts[0].normal.x > 0)
                {
                    // the contact force is to the right
                    // that's mean, enemy hit this player from the left

                    isHurt = new Vector2(30, 0);
                    animator.Play("hurt");
                    blood = blood - 1;

                }
                else
                if (collision.contacts[0].normal.x < 0)
                {
                    // the contact force is to the left
                    // that's mean, enemy hit this player from the right

                    isHurt = new Vector2(-30, 0);
                    animator.Play("hurt");
                    blood = blood - 1;
                }

                healthBar.SetHealth(blood + 1);
            }
        }
    }
}