using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImportantScripts.Core.Singleton; 
using Cloth;

public class Player : Singleton<Player>, IDamageable
{
    public Animator animator;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;


    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    public float vSpeed = 0f;
    [Header("Flash")]
    public List<FlashColor> flashColors;


    private bool _alive = true;

    [Header("Life UI")]
    public HealthBase healthBase;

    [Space]
    [SerializeField]private ClothChanger _clothChanger;

    private void Start() 
    {
        healthBase = GetComponent<HealthBase>();
        healthBase.OnKill += OnKill;
    }

    #region LIFE

    public void Damage(float damage)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        healthBase.Damage(damage);
    }

    public void Damage(float damage, Vector3 dir)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();

        if(healthBase != null)
        {
            healthBase.Damage(damage);
        }
    }

    private void OnKill(HealthBase h)
    {
        if(_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            characterController.enabled = false;
            Rigidbody rb = GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }

            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
    }

    private void TurnOnColliders()
    {
        characterController.enabled = true;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;


        if(characterController.isGrounded)
        {
            vSpeed = 0;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
            }
        }

        var isWalking = inputAxisVertical != 0;

        if(isWalking)
        {
            if(Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;
        
        characterController.Move(speedVector * Time.deltaTime);



        animator.SetBool("Run", isWalking);

    }


    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if(CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }

    public void ChangeJump(float jump, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(jump, duration));
    }

    IEnumerator ChangeJumpCoroutine(float jump, float duration)
    {
        var defaultJump = jumpSpeed;
        jumpSpeed = jump;
        yield return new WaitForSeconds(duration);
        jumpSpeed = defaultJump;
    }
}
