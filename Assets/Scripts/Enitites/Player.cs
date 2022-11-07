using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(EntityMovement))]
public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 1.5f;
    public float speedDodge = 5f;
    public float immuneTime = 1f;
    public float turnSpeed;
    public float cooldownDodge;

    float privateCooldownDodge = 0;
    Vector2 insert;
    InputAction _Look;
    InputAction _Dodge;
    Rigidbody rb;
    EntityMovement entMovement;
    Animator animator;

    void Awake()
    {
        if (!mainCamera) mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        entMovement = GetComponent<EntityMovement>();
        animator = GetComponent<Animator>();
        _Look = GetComponent<PlayerInput>().actions.FindAction("Look");
        _Dodge = GetComponent<PlayerInput>().actions.FindAction("Dodge");
    }

    // Update is called once per frame
    void Update()
    {

        Rotation();
        Dodge();
    }

    private void FixedUpdate()
    {
        if(insert.magnitude > 0f)
            entMovement.Movements(insert, speed);
    }

    #region Movements

    public void Move(InputAction.CallbackContext context)
    {
        insert = context.performed ? context.ReadValue<Vector2>() : Vector2.zero;

    }
    #endregion


    #region Rotation
    void Rotation()
    {
        if (_Look.activeControl == null) return;

        Vector2 dir = _Look.ReadValue<Vector2>();
        if(_Look.activeControl.device is Mouse || _Look.activeControl.device is Touchscreen)
        {
            dir = (dir - (Vector2)mainCamera.WorldToScreenPoint(transform.position)).normalized;
            transform.forward = new Vector3(dir.x, 0, dir.y);

        }else if(dir.magnitude >= 0.5)
        {
            transform.forward = new Vector3(dir.x, 0, dir.y);
        }
    }


    #endregion

    #region Dodge
    void Dodge()
    {
        privateCooldownDodge -= privateCooldownDodge > 0 ? Time.deltaTime : 0;

        if (_Dodge.activeControl == null || privateCooldownDodge > 0) return;

        if(_Dodge.activeControl.device is Keyboard || _Dodge.activeControl.device is Touchscreen)
        {
            privateCooldownDodge = cooldownDodge;
            GetComponent<HealthSystem>().ImmuneActivate(true, immuneTime);
            
            if (insert.magnitude > 0) {
                //rb.velocity += new Vector3(insert.x, 0, insert.y) * speedDodge;
                rb.AddForce(new Vector3(insert.x, 0, insert.y) * speedDodge);
            }
            else
            {
                rb.AddForce(transform.forward * speedDodge);
            }
            //animator.SetTrigger("Dodge");
        }

    }
    #endregion
}
