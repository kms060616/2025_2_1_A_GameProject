using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlle : MonoBehaviour
{


    [Header("이동 설정")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10;

    [Header("점프 설정")]
    public float jumpHeight = 2f;  //
    public float gravity = -9.81f;
    public float landingDuration = 0.3f;   //착지 후 착지 지속 시간
    

    [Header("공격설정")]
    public float attackDruation = 0.8f;   //공격 지속 시간
    public bool canMoveWhileAttacking = false;  //공격중 이동 가능 여부

    [Header("컴포넌트")]
    public Animator animator;

    private CharacterController controller;
    private Camera playerCamera;

    //현재상태
    private float currentSpeed;
    private bool isAttacking = false;

    private bool isLanding = false;
    private float landingTimer;

    private Vector3 velocity;
    private bool isGrounded;
    private bool wasGrounded;    //이전 프레임에 땅이였는지
    private float attackTimer;

    private bool isUIMode = false;  //UI모드 설정


   
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleCursorLock();
        }

        if (!isUIMode)        //UI모드가 아닐때만 플레이어 조작 가능
        {
            CheckGrounded();
            HandleLanding();
            HadleMovement();
            HandleJump();
            HandleAttack();
            UpdateAnimator();
        }

    }

    void CheckGrounded()
    {
        wasGrounded = isGrounded;
        isGrounded = controller.isGrounded;

        if (!isGrounded && wasGrounded)    //땅에서 떨어 졌을때 지금 프레임은 땅이 아니고, 이전프레임은 땅
        {
            Debug.Log("떨어지기 시작");
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            
            if (!wasGrounded && animator != null)
            {
                
                //animator.SetTrigger("landTrigger");
                isLanding = true;
                landingTimer = landingDuration;
                Debug.Log("착지");
            }
        }

    }

    void HandleJump()
    {
        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            if (animator != null)
            {
                animator.SetTrigger("jumpTrigger");
            }
        }
        

        if(!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleLanding()
    {
        if(isLanding)
        {
            landingTimer -= Time.deltaTime;
            if(landingTimer <= 0)
            {
                isLanding = false;
            }
        }
    }

    void HadleMovement()
    {
        if((isAttacking && !canMoveWhileAttacking) || isLanding)
        {
            currentSpeed = 0;
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float verical = Input.GetAxis("Vertical");

        if (horizontal != 0 || verical != 0)
        {
            Vector3 cameraFoward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            cameraFoward.y = 0;
            cameraRight.y = 0;
            cameraFoward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraFoward * verical + cameraRight * horizontal;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
        else
        {
            currentSpeed = 0;
        }

    }
    
    void HandleAttack()
    {
        if(isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                isAttacking = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) && !isAttacking)
        {
            isAttacking = true;
            attackTimer = attackDruation;

            if(animator != null)
            {
                animator.SetTrigger("attackTrigger");
            }
        }
    }
    void UpdateAnimator()
    {
        float animatorSpeed = Mathf.Clamp01(currentSpeed / runSpeed);
        animator.SetFloat("speed", animatorSpeed);
        animator.SetBool("isGrounded", isGrounded);

        bool isFalling = !isGrounded && velocity.y < -0.1f;
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isLanding", isLanding);


    }

    public void SetCursorLock(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isUIMode = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
            isUIMode = true;
        }
    }

    public void ToggleCursorLock()
    {
        bool shouldLock = Cursor.lockState != CursorLockMode.Locked;
        SetCursorLock(shouldLock);
    }
    public void SetUIMode(bool isUIMode)
    {
        SetCursorLock(!isUIMode);
    }
}
