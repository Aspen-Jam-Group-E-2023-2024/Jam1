using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    [Header("Stats")] 
    public int startingHealth = 20;
    public int attackDamage = 20;
    public float IFrameTime = 0.1f;

    [Tooltip("This is how fast (distance) the enemy moves per tick")] 
    public float maxDistDelta = 0.01f;
    
    public Color damagedFlashColor = Color.red;
    private Color originalColor;

    private float health;

    #region Private References

    private GameManager gm;
    private Transform player;
    private Rigidbody2D rb;
    private PlayerController pc;

    #endregion

    // private float hitTimer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        originalColor = gameObject.GetComponent<Renderer>().material.color;
        pc = player.GetComponent<PlayerController>();

        // hitTimer = 0f;

        health = startingHealth;
    }

    private void FixedUpdate()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, maxDistDelta);
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         // IFrameTime wasn't properly working, so we do this instead
    //         hitTimer -= Time.deltaTime;
    //         
    //         if (hitTimer <= -0.005f)
    //         {
    //             StartCoroutine(other.gameObject.GetComponent<PlayerController>().TakeDamage(attackDamage));
    //
    //             hitTimer = pc.IFrameTime;
    //         }
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     hitTimer = 0f;
    // }

    public IEnumerator Hurt(int damage, float knockbackForce)
    {
        // taking damage
        health -= damage;
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        
        // knockback
        Vector2 knockbackDir = (transform.position - player.position).normalized;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        
        // flash color
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = damagedFlashColor;

        yield return new WaitForSeconds(IFrameTime);

        mat.color = originalColor;
    }
}
