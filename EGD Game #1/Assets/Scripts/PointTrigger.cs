using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    [Tooltip("How much damage this point does to the player")] public int damage = 10;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            StartCoroutine(pc.TakeDamage(damage));
        }
    }
}
