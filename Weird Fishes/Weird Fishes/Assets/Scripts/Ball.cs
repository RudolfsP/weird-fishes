﻿using UnityEngine;

public class Ball : MonoBehaviour {

    //config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    //Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start() {
        //the compiler already knows we are talking about the ball game object, so transform.position is sufficient
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if(!hasStarted) {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick() {
        //if mouse is clicked then the ball flies up and to the right
        if(Input.GetMouseButtonDown(0)) {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);  
        }
    }

    private void LockBallToPaddle() {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddleToBallVector + paddlePos;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor), 
            Random.Range(0f, randomFactor));

        if (hasStarted) {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }

    }
}
