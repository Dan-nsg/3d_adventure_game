using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Player : MonoBehaviour, IDamageable
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
}
