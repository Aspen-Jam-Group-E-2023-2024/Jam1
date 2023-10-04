using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")] 
    public float speed;

    [Header("Combat")] 
    public LayerMask enemyMask;
    public KeyCode dashKey = KeyCode.Space;
    public float IFrameTime = 0.05f;

    private Vector2 aimingDirection;
    private bool dashing;
    // private Vector2 mousePos;
    private Color originalColor;

    [Header("Stats")] 
    public int startingHealth = 20;
    public int damage = 20;
    public float dashForce = 5f; // how powerful (how far) the dash is
    public float dashCooldown = 0.5f; // how long it takes to dash again after dashing
    public float knockbackForce = 5f; // how much the enemy moves away after being hit

    private int health;


    #region Private references

    private CircleCollider2D coli;
    private Rigidbody2D rb;
    private Material mat;

    #endregion

    private void Start()
    {
        dashing = false;
        coli = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        mat = GetComponentInChildren<Renderer>().material;
        originalColor = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        
        // dash
        if (Input.GetKeyDown(dashKey) && dashing == false)
        {
            StartCoroutine(Dash(dashCooldown));
        }
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // rb.AddForce(Vector2.up * speed * Time.deltaTime, ForceMode2D.Impulse);
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            aimingDirection = Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            // rb.AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Impulse);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            aimingDirection = Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            // rb.AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Impulse);
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            aimingDirection = Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            // rb.AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Impulse);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            aimingDirection = Vector2.right;
        }

        // mouse calculations
        // mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // aimingDirection = (mousePos - (Vector2)transform.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // currently dashing - do damage
        if (dashing)
        {
            IEnemy ec = other.transform.GetComponent<IEnemy>();
            StartCoroutine(ec.Hurt(damage, knockbackForce));
            // Debug.Log("Hurt " + other.name + "!");
        }
    }

    private IEnumerator Dash(float cooldown)
    {
        dashing = true;

        // Physics2D.OverlapCircleAll()
        // float radius = (range - coli.radius) / 2;
        // this is the range to use for OverlapCircleAll
        // Vector2 sendRange = (Vector2)transform.position + (aimingDirection * range) - (aimingDirection * new Vector2(radius, radius));
        
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(sendRange, radius, enemyMask);
        //
        // foreach (var other in hitEnemies)
        // {
        //     if (other != null)
        //     {
        //         IEnemy ec = other.transform.GetComponent<IEnemy>();
        //         StartCoroutine(ec.Hurt(damage, knockbackForce));
        //         // Debug.Log("Hurt " + other.name + "!");
        //     }
        // }
        
        // add force in aiming direction, then check if anything ever hits the collider
        rb.AddForce(aimingDirection * dashForce, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(cooldown);
        
        dashing = false;
    }

    public IEnumerator TakeDamage(int hitDamage)
    {
        health -= hitDamage;

        if (health <= 0f)
        {
            Debug.Log("Died!");
            // start game over screen
        }
        
        mat.color = Color.red;
        
        yield return new WaitForSeconds(IFrameTime);

        mat.color = originalColor;
    }
}