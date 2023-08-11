using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_life : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource DeathSoundEffect;
    [SerializeField] private int maxHealth = 3;


    [SerializeField] private int currentHealth;
    
    private Vector2 startingPoint;
    private HealthController healthController;

    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        startingPoint = transform.position;

        healthController = FindObjectOfType<HealthController>();
        healthController.playerHealth = currentHealth;
        healthController.UpdateHealth();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {   
           
            DeathSoundEffect.Play();
            Die();
            healthController.playerHealth = currentHealth;
        }
    }
    private void Die()
    {
        currentHealth--;
        healthController.playerHealth = currentHealth;
        healthController.UpdateHealth();
        if (currentHealth <= 0)
        {            
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
            isGameOver = true;
            Invoke("GameOver", 2f);           
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
            Invoke("respawnToTheStartPoint", 2f);
        }
        
    }
    private void Respawn()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
    private void respawnToTheStartPoint()
    {
        transform.position = startingPoint;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
   
}
