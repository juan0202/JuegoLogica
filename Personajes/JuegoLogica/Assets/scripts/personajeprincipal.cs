using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personajeprincipal : MonoBehaviour {
    public float speed = 4f;
    Animator anim;
    Rigidbody2D rigidbody;
    Vector2 mov;

   


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
         mov = new Vector2(
        
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );

       /* transform.position = Vector2.MoveTowards(
            transform.position,
            transform.position + mov,
            speed * Time.deltaTime

            ); */
        anim.SetFloat("motx", mov.x);
        anim.SetFloat("moty", mov.y);


    }
    //Colisiona con objetos

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + mov * speed * Time.deltaTime);
    }

}
