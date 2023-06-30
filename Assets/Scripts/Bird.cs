using UnityEngine;

public class Bird : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;
    public float maxHealth = 3f;
    public float health;
    public Animator animator;
    public HealthBarColorChange healthBar;

    private Rigidbody rb;
    private float timeToChangeDirection = 1f;
    private float changeDirectionTimer = 0f;
    private Vector3 randomDirection;
    private bool isDead = false;
    private float nextJump = 0;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        randomDirection = Random.insideUnitSphere;

        health = maxHealth;
        healthBar.SetHealth(maxHealth);

        nextJump = Time.time + Random.Range(1, 5);
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        RandomJump();
        HandleMovement();
        HandleChangeDirection();
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void RandomJump()
    {
        if (Time.time >= nextJump)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            nextJump = Time.time + Random.Range(1, 5);
        }

    }

    private void HandleMovement()
    {
        transform.position += randomDirection * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(randomDirection), rotationSpeed * Time.deltaTime);
    }

    private void HandleChangeDirection()
    {
        changeDirectionTimer += Time.deltaTime;

        if (changeDirectionTimer >= timeToChangeDirection)
        {
            randomDirection = Random.insideUnitSphere;
            changeDirectionTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            TakeDamage(1f);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health / maxHealth);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        isDead = true;
        // Ölüm animasyonu bittikten sonra kuşu yok etmek için gerekli kodu ekleyin.
        // Destroy(gameObject, 2f); // Örneğin, 2 saniye sonra kuş nesnesini yok eder.
    }
}
