using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heroScript : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rig;

    float posX, posY;

    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    bool facingRight = true;

    public int score=0;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        posX = rig.position.x;
        posY = rig.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);


        float move;
        move = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(move * speed, rig.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) {
            rig.AddForce(new Vector2(0, 700f));
        }


        if (move<0 && facingRight)        
            Flip();        
        else if (move>0 && !facingRight)        
            Flip();        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Collider2D>().tag == "star")
            Destroy(coll.gameObject);
            score++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            rig.position = new Vector2(posX, posY);
        }

        if (collision.gameObject.CompareTag("endLevel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 80, 30), "Score: " + score);
    }
}
