using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    //configuration parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    //cached references
    GameSession gameSession;
    Ball ball;

    // Start is called before the first frame update
    void Start() {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update() {
        //vector 2 is a compact way of storing x and y coordinates
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        //the clamp function makes the paddle not go over the specified min and max values on the screen
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos() {
        if(gameSession.IsAutoPlayEnabled()) {
            return ball.transform.position.x;
        }

        else {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
