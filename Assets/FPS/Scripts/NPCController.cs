using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class NPCController : MonoBehaviour
{
    //My components
    public Rigidbody RB;
    public Animator Anim;

    //My stats
    public float Speed = 50;
    public float Health = 100;
    public float Score = 0;
    
    //Who do I walk towards?
    public Vector3 Target;
    public float Timer = 0f;
    public float DirectionChange = 3f;
    public GameObject objectToSpawn;
    public TextMeshPro playerScoreText;

    void Start()
    {
        Target = new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f));
        //At the start of the game I should play my walk animation
        Anim.Play("Walking");
        //I just walk forever, for now.
    }

    public void UpdateScore()
   {
       playerScoreText.text = "Score: " + Score;

   }

    private void Update()
    {
       //playerScoreText.text = "Score: " + Score;
        Timer += Time.deltaTime;

        if (Timer >= DirectionChange)
        {
            Target = new Vector3(Random.Range(-35.0f, 35.0f), Random.Range(2f, 35.0f), Random.Range(-35.0f, 35.0f));
            Timer = 0f;
            DirectionChange = Random.Range(1f, 5f);
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
        //vel.y = RB.linearVelocity.y;
        //Plug it into my rigidbody
        RB.linearVelocity = vel;

        


    }

    private void OnMouseDown()
    {
        
        Health = Health - 100;
        //if()

        if (Health <= 0)
        {
            Score += 100;
            Health = 100;
            transform.position = new Vector3(Random.Range(-35.0f, 35.0f),Random.Range(2f, 35.0f),Random.Range(-35.0f, 35.0f));
            UpdateScore();
        }
        
    }
}
