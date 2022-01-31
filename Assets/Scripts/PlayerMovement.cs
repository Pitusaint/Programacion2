using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    float horizontalMove;
    float verticalMove;
    private Vector3 playerInput;
    public CharacterController player;
    public float playerSpeed;
    public float gravity;
    public float fallVelocity;
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;
    public float jumpForce = 8f;
    public bool isJumping = false;
    // Use this for initialization
    void Start()
    {
        player = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        CamDirection();
        PlayerMovemente();
        SetGravity();
        PlayerSkills();
        player.Move(movePlayer * Time.deltaTime);
        Debug.Log(player.isGrounded);
    }
    //Funcion que obtiene el imnput de movimiento de nuestro jugador.
    public void PlayerInput()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
    }
    //Funcion para determinar la direccion a la que mira la camara. 
    public void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
    public void PlayerMovemente()
    {
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer = movePlayer * playerSpeed;
        player.transform.LookAt(player.transform.position + movePlayer);
    }
    public void PlayerSkills()
    {
        if (player.isGrounded)
        {
            isJumping = false;
        }
        //Jump                 
        if (player.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }
    //Funcion para la gravedad.
    public void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }
}