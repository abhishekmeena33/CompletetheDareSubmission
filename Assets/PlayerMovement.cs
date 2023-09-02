using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    private float Move;
    private bool isJumping;
    private bool isHoldingF = false;

    public bool isActiveForMovement = false;
    public bool hasSpirit = false;
    public GameManager gameManager;
    public SpriteRenderer spiritSprite;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        setSpirit(hasSpirit);
        if(isActiveForMovement || hasSpirit) {
            gameManager.currentActive.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        if (isActiveForMovement) {
            rb.velocity = new Vector2(Move*speed, rb.velocity.y);
        }

        if(Input.GetButtonDown("Jump") && isJumping == false && isActiveForMovement){
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }

        //checking if F is on hold or not
        if (Input.GetKey(KeyCode.F))
        {
            isHoldingF = true;
        }
        else
        {
            isHoldingF = false;
        }

        if(isHoldingF)
        {
            rb.velocity = new Vector2(2*Move*speed, rb.velocity.y);
        }

    }

    public void setSpirit(bool status) {
        hasSpirit = status;
        spiritSprite.enabled = status;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isJumping = false;
        }

        if(isActiveForMovement && other.gameObject.CompareTag("PlayerUnit")){
            PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
            Debug.Log("Attached");
            gameManager.inContact.Add(pm);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            isJumping = true;
        }

        if(isActiveForMovement && other.gameObject.CompareTag("PlayerUnit")){
            PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
            Debug.Log("Detached");
            gameManager.inContact.Remove(pm);
        }
    }
}
