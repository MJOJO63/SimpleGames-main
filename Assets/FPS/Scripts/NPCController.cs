using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    //My components
    public Rigidbody RB;
    public Animator Anim;

    //My stats
    public float Speed = 5;
    public float Health = 100;
    
    //Who do I walk towards?
    public Vector3 Target;
    public float Timer = 0f;
    public float DirectionChange = 2f;

    void Start()
    {
        //At the start of the game I should play my walk animation
        Anim.Play("Walking");
        //I just walk forever, for now.
    }

    private void Update()
    {
       
        Timer =+ Time.deltaTime;

        if (Timer >= DirectionChange)
        {
            Target = new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f));
            Timer = 0f;
        }
        
        //Rotate to look at the player
        transform.LookAt(Target);
        //Make a temp velocity variable to calculate how I should move
        //By default, I keep my old momentum
        Vector3 vel = RB.linearVelocity;
        //Walk forwards, but don't do it perfectly. Lerp towards my desired speed
        //This makes it so that if I take a knockback it takes a second for me to recover
       
        
        vel = Vector3.Lerp(vel,transform.forward * Speed,10*Time.deltaTime);
        
        
        //Use my old Y velocity, though. I shouldn't be able to fly
        vel.y = RB.linearVelocity.y;
        //Plug it into my rigidbody
        RB.linearVelocity = vel;

        


    }

    private void OnMouseDown()
    {
        
        Health = Health - 50;
        //if()

        if (Health <= 0)
        Destroy(gameObject);
    }
}
