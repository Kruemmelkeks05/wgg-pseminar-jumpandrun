using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;
    public float jumph = 5;
    private bool isGrounded = false;

    private Animator anim;
    private Vector3 rotation;
    private  Coins coinmanager;
    public GameObject panel;
    public GameObject panel2;
    private int mc;
    public GameObject player;
    
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotation = transform.eulerAngles;
        coinmanager = GameObject.FindGameObjectWithTag("Text").GetComponent<Coins>();
        mc = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float richtung = Input.GetAxis("Horizontal");
        if (richtung != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (richtung < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
            transform.Translate(Vector2.right * speed * -richtung * Time.deltaTime);
        }
        if (richtung > 0)
        {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * speed * richtung * Time.deltaTime);
        }
        if (isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }
        else { anim.SetBool("isJumping", false); }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumph, ForceMode2D.Impulse);
            isGrounded = false;
        }
        if (mc == 9)
        {
            panel2.SetActive(true);
        }

        {
            if (player.transform.position.y < -5.3)
            {
                Destroy(GameObject.FindWithTag("player"));
                panel.SetActive(true);
            }
        }

    }



    private void OnCollisionEnter2D(Collision2D collsion)
    {
        if (collsion.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
       

    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            coinmanager.AddMoney();
            mc++;

        }
        if (other.gameObject.tag == "Spike")
        {
            panel.SetActive(true);
            Destroy(gameObject);


        }
        if (other.gameObject.tag == "finish")
        {
            panel2.SetActive(true);
        }
        
    }
}
