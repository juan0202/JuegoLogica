using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float visionRadius;
    public float attackRadius;
    public float speed;



    ///----- Variables relacionadas con el ataque
    [Tooltip("Prefab de la roca que se disparará")]
    public GameObject rockPrefab;
    [Tooltip("Velocidad de ataque (segundos entre ataques)")]
    public float attackSpeed = 2f;
    bool attacking;
    ///----- Fin de Variables relacionadas con el ataque


    // Variable para guardar al jugador
    GameObject player;

    // Variable para guardar la posición inicial
    Vector3 initialPosition, target;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    void Start () {

        // Recuperamos al jugador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Guardamos nuestra posición inicial
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        target = initialPosition;
    }

    void Update () {

        // Por defecto nuestro target siempre será nuestra posición inicial
        

        // Comprobamos un Raycast del enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, 
            player.transform.position - transform.position, 
            visionRadius, 
            1 << LayerMask.NameToLayer("Default") 
                // Poner el propio Enemy en una layer distinta a Default para evitar el raycast
                // También poner al objeto Attack y al Prefab Slash una Layer Attack 
                // Sino los detectará como entorno y se mueve atrás al hacer ataques
        );

        // Aquí podemos debugear el Raycast
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Vector3 spawn = transform.TransformDirection(initialPosition - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);
        

        // Si el Raycast encuentra al jugador lo ponemos de target
        //Error aqui movimiento
        if (hit.collider != null) {            if (hit.collider.tag == "Player"){
                target = player.transform.position;
                Debug.Log("Si esta aqui puta madreee");
            }
        }
        target = player.transform.position;

        // Calculamos la distancia y dirección actual hasta el target
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;
        Vector3 spawnDir = (spawn - transform.position).normalized;

        // Si es el enemigo y está en rango de ataque nos paramos y le atacamos
        if (target != initialPosition && distance < attackRadius){
            // Aquí le atacaríamos, pero por ahora simplemente cambiamos la animación
            anim.SetFloat("MovX", dir.x);
            anim.SetFloat("MovY", dir.y);
            anim.Play("walking", -1, 0);  // Congela la animación de andar
            Debug.DrawRay(transform.position, forward, Color.yellow);
            ///-- Empezamos a atacar (importante una Layer en ataque para evitar Raycast)
            if (!attacking)
            {
                StartCoroutine(Attack(attackSpeed));
            }
        }
        // En caso contrario nos movemos hacia él
        else if (!(distance < visionRadius))
        {
            Debug.DrawRay(transform.position, spawn, Color.blue);
                rb2d.MovePosition(transform.position + spawn * Time.deltaTime);
            // Al movernos establecemos la animación de movimiento
            anim.speed = 1;
            anim.SetFloat("MovX", spawnDir.x);
            anim.SetFloat("MovY", spawnDir.y);
            anim.SetBool("walking", true);

        }
        else {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
            Debug.DrawRay(transform.position, forward, Color.green);
            // Al movernos establecemos la animación de movimiento
            anim.speed = 1;
            anim.SetFloat("MovX", dir.x);
            anim.SetFloat("MovY", dir.y);
            anim.SetBool("walking", true);
        }

        // Una última comprobación para evitar bugs forzando la posición inicial
        if (target == initialPosition && distance < 0.05f){
            transform.position = initialPosition;
            // Y cambiamos la animación de nuevo a Idle
            anim.SetBool("walking", false);
        }

        // Y un debug optativo con una línea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);
    }

    // Podemos dibujar el radio de visión y ataque sobre la escena dibujando una esfera
    void OnDrawGizmosSelected() {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }

    IEnumerator Attack(float seconds){
        attacking = true;  // Activamos la bandera
        // Si tenemos objetivo y el prefab es correcto creamos la roca
        if (target != initialPosition && rockPrefab != null) {
            Instantiate(rockPrefab, transform.position, transform.rotation);
            // Esperamos los segundos de turno antes de hacer otro ataque
            yield return new WaitForSeconds(seconds);
        }
        attacking = false; // Desactivamos la bandera
    }

}