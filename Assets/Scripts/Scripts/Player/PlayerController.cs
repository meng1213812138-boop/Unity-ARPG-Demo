using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private CharacterController characterController;
    [SerializeField] private float speed = 2;
    private bool canMove = true;
    private int atk = 1;
    public int maxHealth = 20;
    public string playerName = "勇者";
    public int currentHealth;
    private Vector2 currentInput;
    private Vector2 inputVelocity;

    [SerializeField]
    private float inputSmoothTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        moveAction = playerInput.actions.FindAction("Role/Move");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// 控制角色跳跃的方法
    /// </summary>
    /// <param name="callbackContext"></param>
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed)
            animator.SetTrigger("Jump");
    }

    /// <summary>
    /// 控制角色移动的方法
    /// </summary>
    public void Move()
    {
        if (canMove)
        {
            Vector2 targetInput = moveAction.ReadValue<Vector2>();
            currentInput = Vector2.SmoothDamp(
                currentInput,
                targetInput,
                ref inputVelocity,
                inputSmoothTime
            );
            animator.SetFloat("HSpeed", currentInput.x);
            animator.SetFloat("VSpeed", currentInput.y);
            //Vector3 move = new Vector3(currentInput.x, 0, currentInput.y);

            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();
            Vector3 move = cameraForward * currentInput.y + cameraRight * currentInput.x;
            if (move.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,10*Time.deltaTime);
            }
            characterController.Move(move * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 控制角色攻击的方法
    /// </summary>
    /// <param name="callbackContext"></param>
    public void Attack(InputAction.CallbackContext callbackContext)
    {

        if (callbackContext.performed)
        {
            canMove = false;
            animator.SetFloat("HSpeed", 0);
            animator.SetFloat("VSpeed", 0);
            currentInput = Vector2.zero;
            inputVelocity = Vector2.zero;
            animator.SetTrigger("Attack");
        }
    }

    public void AttackEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position + transform.forward + transform.up,
            1,
            1 << LayerMask.NameToLayer("EnemyLayer"));
        //Debug.Log(colliders.Length);
        for(int i = 0; i < colliders.Length; i++)
        {
            EnemyObject enemy = colliders[i].gameObject.GetComponent<EnemyObject>();
            if (enemy != null)
            {
                enemy.TakeDamage(atk);
            }
            else
                return;
        }
    }

    public void CanMove()
    {
        canMove = true;
    }

    public void TakeDamage(int atk)
    {
        currentHealth -= atk;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("GetHit");
        }
    }

    public void Die()
    {
        canMove = false;
        
        animator.SetFloat("HSpeed", 0);
        animator.SetFloat("VSpeed", 0);
        currentInput = Vector2.zero;
        inputVelocity = Vector2.zero;
        animator.SetBool("IsDead",true);
    }

    public void DeadEvent()
    {
        Destroy(gameObject);
    }
}
