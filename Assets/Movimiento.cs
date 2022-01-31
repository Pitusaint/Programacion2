using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Movimiento : MonoBehaviour
{
    public Rigidbody Player;
    public float HorizontalInput;
    public float Speed;
    public float jumpForce = 8f;
    public bool isJumping = false;
    public float gravity;
    public float fallVelocity;
    public CharacterController player;
    private Vector3 movePlayer;
    public Text puntoss;
    public int puntuacion = 5;
    public GameObject msg1;


    void Update()
    {

        HorizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(HorizontalInput, 0) * Speed * Time.deltaTime;
        Player.MovePosition(Player.position + movement);
        puntoss.text = "Plataformas que faltan: " + puntuacion;

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

    void OnCollisionEnter(Collision collision)
    {

        if ( collision.collider.tag == "Plataforma"  && puntuacion > 0)
        {
            puntuacion = puntuacion - 1;
            
        }
        if (collision.collider.tag == "Plataforma2" && puntuacion < 5)
        {
            puntuacion++;

        }

        if (collision.collider.tag == "Meta" && puntuacion == 0)
        {
            
            msg1.SetActive(true);

        }
    }

 
}
