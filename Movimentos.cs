using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movimentos : MonoBehaviour
{
    public float speed = 1.0f;
    public float speedSide = 10.00f;
    //public float rotateSpeed = 2.0f;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundcheck; //game object em baixo do player
    public float groundDistance = 0.4f;//radio da circunferencia gerada
    public LayerMask groundMask;//conferir se é um terreno
    bool isGrounded;//Confere se o objeto está no chão


    CharacterController controller;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
         controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);//cria uma esfera invisivel abaixo do player, de forma a avisar se o mesmo está no chão


        // Rotate around y - axis
        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime * 10, 0) ;



        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.back);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.Move(forward * curSpeed * Time.deltaTime); //Enviando Movimento base Forward Backward



        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //velocity.y = 17f;
            float x = 1 / 2;
            velocity.y = Mathf.Pow(jumpHeight * -2f * gravity,x) ; //Movimento base de Gravidade

        }

        if(Input.GetKeyDown(KeyCode.A)){
            transform.Rotate(0,-5,0);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            transform.Rotate(0,5,0);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity *10*Time.deltaTime);// Enviando gravidade para o Player pelo metodo MOVE()

    }
}
