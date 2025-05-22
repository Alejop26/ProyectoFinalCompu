using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public UsePU usepu;
    public Gun gun;
    public Rigidbody2D PlayerBody;
    public float MoveSpeed;
    public float MoveSpeed2;
    public Animator animator;
    public Animator Leganimator;
    public bool isMoving;
    private string currentstate;
    //public UnityEvent StopSpawning;
    private  KeyCode UpKey = KeyCode.W, DownKey = KeyCode.S, LeftKey = KeyCode.A, RightKey = KeyCode.D;
    private Vector2 movement;
    
    const string DefaultState = "Default State";
    const string CBAimUp = "CBAimUp";
    const string CBAimLeft = "CBAimLeft";
    const string CBAimDown = "CBAimDown";
    const string CBAimRight = "CBAimRight";

    private void Start()
    {
        usepu = GameObject.Find("Player").GetComponent<UsePU>();    
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        Leganimator = GameObject.FindGameObjectWithTag("Leg").GetComponent<Animator>();
        MoveSpeed = 4.25f;
        MoveSpeed2 = 3.5f;
    }
    void Update()
    {
        if ((Input.GetKey(UpKey) || Input.GetKey(LeftKey) || Input.GetKey(DownKey) || Input.GetKey(RightKey)) && usepu.IsUsingTombstone == false)
        {
            Leganimator.SetTrigger("IsWalking");
            isMoving = true;

            if (Input.GetKey(UpKey)) ChangeAnim(CBAimUp);
            if (Input.GetKey(LeftKey)) ChangeAnim(CBAimLeft);
            if (Input.GetKey(DownKey)) ChangeAnim(CBAimDown);
            if (Input.GetKey(RightKey)) ChangeAnim(CBAimRight);
            if (Input.GetKey(LeftKey) && Input.GetKey(DownKey) || Input.GetKey(LeftKey) && Input.GetKey(UpKey)) ChangeAnim(CBAimLeft);
            if (Input.GetKey(RightKey) && Input.GetKey(DownKey) || Input.GetKey(RightKey) && Input.GetKey(UpKey)) ChangeAnim(CBAimRight);

            if (gun.isShootingUp == true) ChangeAnim(CBAimUp);
            if (gun.isShootingLeft == true) ChangeAnim(CBAimLeft);
            if (gun.isShootingDown == true) ChangeAnim(CBAimDown);
            if (gun.isShootingRight == true) ChangeAnim(CBAimRight);
            if (gun.isShootingUp == true || gun.isShootingDown == true)
            {
                if (gun.isShootingLeft == true) ChangeAnim(CBAimLeft);
                if (gun.isShootingRight == true) ChangeAnim(CBAimRight);
            }
        }
        else isMoving = false;

        if (isMoving == false && gun.isShooting == false && usepu.IsUsingTombstone == false)
        {
            ChangeAnim(DefaultState);
        }

        //====================Disable player collider & stop spawning====================//
        if (Input.GetKey(KeyCode.O)) GetComponent<Collider2D>().enabled = false;
        else if (Input.GetKey(KeyCode.P)) GetComponent<Collider2D>().enabled = true;
        /*if (Input.GetKey(KeyCode.R)) StopSpawning.Invoke();*/
        //====================Disable player collider & stop spawning====================//
    }
void FixedUpdate()
{
    Vector2 movement = Vector2.zero;

    if (Input.GetKey(UpKey)) movement += Vector2.up;
    if (Input.GetKey(DownKey)) movement += Vector2.down;
    if (Input.GetKey(LeftKey)) movement += Vector2.left;
    if (Input.GetKey(RightKey)) movement += Vector2.right;

    SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
    if (sr != null)
    {
        if (movement == Vector2.zero)
        {
            // Si está quieto, asegurar que el sprite esté activo
            if (!sr.enabled)
                sr.enabled = true;
        }
        else
        {
            // Si se mueve, también asegurar que el sprite esté activo
            if (!sr.enabled)
                sr.enabled = true;
        }
    }

    if (movement != Vector2.zero)
    {
        movement = movement.normalized;
        PlayerBody.MovePosition(PlayerBody.position + movement * MoveSpeed * Time.fixedDeltaTime);
    }
}


    public void ChangeAnim(string newstate)
    {
        if (currentstate == newstate) return;
        animator.Play(newstate, -1, 0f);    
        currentstate = newstate;    
    }
}
