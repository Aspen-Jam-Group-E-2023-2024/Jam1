using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] 
    public float speed = 1f;
    

    [Header("Combat")] 
    public int startingHealth = 20;
    private int health;
    
    // Start is called before the first frame update
    void Start()
    {
        // Anthony - do we want linear translate movement or rigidbody force movement?
        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    
    // Move Player function
    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.up * speed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.left * speed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.down * speed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.right * speed * Time.deltaTime); }
    }
}
