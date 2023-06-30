using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class Fox : MonoBehaviour
{
    [SerializeField] private float moveForce = 100f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private InputActionAsset inputActionsAsset;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] private float cameraRotationSpeed = 5.0f;
    [SerializeField] private float maxNormalSpeed = 5.0f;
    [SerializeField] private float maxRunSpeed = 8.0f;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private float runCooldown = 3.0f;
    [SerializeField] private Image attackCooldownImage;
    [SerializeField] private Image runCooldownImage;
    [SerializeField] private GameObject attackHitbox;
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private TextMeshProUGUI pickBirdText;
    [SerializeField] private TextMeshProUGUI speedLevelText;
    [SerializeField] private TextMeshProUGUI jumpLevelText;
    [SerializeField] private TextMeshProUGUI attackLevelText;
    [SerializeField] private TextMeshProUGUI runLevelText;

    private InputAction movementAction;
    private InputAction lookAction;

    public int DeadBirds { get; set; } = 0;
    public int [] levels = new int[4];

    private Rigidbody rb;
    private Animator animator;

    private bool isGrounded;
    private int jumpCount;
    private const int maxJumps = 2; // Double jump için

    private float runSpeed = 1.0f;
    private bool isRunning = false;
    private float runAcceleration = 1.0f;
    private float lastAttackTime;
    private float lastRunTime;
    private float nextAttackTime;
    private float nextRunTime;
    private bool canAttack = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        movementAction = inputActionsAsset.FindActionMap("gameplay").FindAction("Move");
        lookAction = inputActionsAsset.FindActionMap("gameplay").FindAction("Look");
        inputActionsAsset.FindActionMap("gameplay").FindAction("Jump").performed += OnJump;
        inputActionsAsset.FindActionMap("gameplay").FindAction("Attack").performed += OnAttack;
        inputActionsAsset.FindActionMap("gameplay").FindAction("Pickup").performed += OnPickup;
        var runAction = inputActionsAsset.FindActionMap("gameplay").FindAction("Run");
        runAction.started += OnRunStarted;
        runAction.canceled += OnRunCanceled;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleRotation();
        HandleRunning();
        UpdateCooldownText();
        CheckIsThereAnyBird();
        HandleCameraRotation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    // Handle Functions

    private void HandleMovement()
    {
        if (!isRunning && rb.velocity.magnitude > maxNormalSpeed)
            return;
        if (isRunning && rb.velocity.magnitude > maxRunSpeed)
            return;

        Vector2 movementInput = movementAction.ReadValue<Vector2>();
        Vector3 force = transform.forward * movementInput.y * moveForce * runSpeed
                        + transform.right * movementInput.x * moveForce * runSpeed;

        rb.AddForce(force);

        animator.SetFloat("Z", movementInput.x * runSpeed);
        animator.SetFloat("X", movementInput.y * runSpeed);
    }

    private void HandleRotation()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        transform.Rotate(0f, lookInput.x * rotationSpeed * Time.deltaTime, 0f);
    }

    private void HandleRunning()
    {
        if (isRunning && Time.time - lastRunTime > runCooldown)
        {
            runSpeed = Mathf.MoveTowards(runSpeed, maxRunSpeed, runAcceleration * Time.deltaTime);
        }
        else
        {
            runSpeed = Mathf.MoveTowards(runSpeed, 1.0f, runAcceleration * Time.deltaTime);
        }
    }

    private void HandleCameraRotation()
    {
    }

    private void UpdateCooldownText()
    {
        float attackCooldownRemaining = nextAttackTime - Time.time;
        float runCooldownRemaining = nextRunTime - Time.time;

        attackCooldownImage.fillAmount = Mathf.Clamp01(attackCooldownRemaining / attackCooldown);
        runCooldownImage.fillAmount = Mathf.Clamp01(runCooldownRemaining / runCooldown);
    }

    // Callbacks

    private void OnJump(InputAction.CallbackContext context)
    {
        if ((isGrounded || jumpCount < maxJumps) && Time.time - lastRunTime > runCooldown)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpCount++;
            animator.SetTrigger("Jump");
        }
    }

    private void OnPickup(InputAction.CallbackContext context)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.0f); 
        foreach (var hitCollider in hitColliders)
        {
            Bird bird = hitCollider.GetComponent<Bird>();
            if (bird != null && bird.IsDead())
            {
                hitCollider.gameObject.SetActive(false);
                DeadBirds++;
                inventoryText.text = ": " + DeadBirds;
                break;
            }
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnRunStarted(InputAction.CallbackContext context)
    {
        if (Time.time < nextRunTime)
            return;
            
        isRunning = true;
        nextRunTime = Time.time + runCooldown;
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        isRunning = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnEnable()
    {
        inputActionsAsset.FindActionMap("gameplay").Enable();
    }

    private void OnDisable()
    {
        inputActionsAsset.FindActionMap("gameplay").Disable();
    }    

    // Helper Functions

    private void CheckIsThereAnyBird()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.0f); 
        foreach (var hitCollider in hitColliders)
        {
            Bird bird = hitCollider.GetComponent<Bird>();
            if (bird != null && bird.IsDead())
            {
                pickBirdText.text = "Press E to collect dead bird";
                break;
            }
            else
            {
                pickBirdText.text = "";
            }
        }
    }

    // IEnumerator

    private IEnumerator Attack()
    {
        canAttack = false;
        attackHitbox.SetActive(true);
        nextAttackTime = Time.time + attackCooldown;
        animator.SetTrigger("Attack");

        // Saldırı hitbox'unu kısa bir süre aktif tut
        yield return new WaitForSeconds(0.3f);
        attackHitbox.SetActive(false);

        // Cooldown süresi boyunca beklet
        yield return new WaitForSeconds(attackCooldown - 0.3f);
        canAttack = true;
    }


    // Upgrade System

    public void UpgradeSpeed()
    {
        moveForce += 10f;
        levels[0]++;
        speedLevelText.text = "" + levels[0];
    }

    public void UpgradeJump()
    {
        jumpForce += 1f;
        levels[1]++;
        jumpLevelText.text = "" + levels[1];
    }

    public void UpgradeAttack()
    {
        attackCooldown -= 0.1f;
        levels[2]++;
        attackLevelText.text = "" + levels[2];
    }

    public void UpgradeRun()
    {
        runCooldown -= 0.1f;
        levels[3]++;
        runLevelText.text = "" + levels[3];
    }

    public int [] GetLevels()
    {
        return levels;
    }

    public int NextUpgradeCost()
    {
        return  (levels[0] + levels[1] + levels[2] + levels[3] + 2) / 2;
    }


}
