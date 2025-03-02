using System;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class Bullet : EnvironmentEntity
{
    private enum Direction{
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3
    }

    // private vars
    private Direction dir; // record the direction of the bullet

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        PrevStep = gameManager.GetComponent<GameController>().GameTicks;

        // assign direction
        switch(this.transform.rotation.eulerAngles.z){
            case 0:
                dir = Direction.UP;
                break;
            case 180:
                dir = Direction.DOWN;
                break;
            case 270:
                dir = Direction.RIGHT;
                break;
            case 90:
                dir = Direction.LEFT;
                break;
            }
    }

    // Update is called once per frame
    void Update()
    {
        // if the game controller has a tick update, move forwards
        if (checkGameTick()){
            stepForward();
        }
        if(checkToDestroy()){
            Destroy(this.gameObject);
        }
    }

    // move shot in direction it is facing
    void stepForward(){
        float x = this.transform.position.x;
        float y = this.transform.position.y;

        switch(dir){
            case Direction.UP:
                this.transform.position = new Vector2(x, y + 1);
                break;
            case Direction.DOWN:
                this.transform.position = new Vector2(x, y - 1);
                break;
            case Direction.LEFT:
                this.transform.position = new Vector2(x - 1, y);
                break;
            case Direction.RIGHT:
                this.transform.position = new Vector2(x + 1, y);
                break;
            
        }
    }

    bool checkToDestroy(){
        bool objFound = false;
        switch(dir){
            case Direction.UP:
                objFound = Physics.Raycast(this.transform.position, Vector2.up, 0.5f);
                break;
            case Direction.DOWN:
                objFound = Physics.Raycast(this.transform.position, -Vector2.up, 0.5f);
                break;
            case Direction.LEFT:
                objFound = Physics.Raycast(this.transform.position, -Vector2.right, 0.5f);
                break;
            case Direction.RIGHT:
                objFound = Physics.Raycast(this.transform.position, Vector2.right, 0.5f);
                break;
        }
    return objFound;
    }
}
