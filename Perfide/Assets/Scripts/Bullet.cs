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
        createBaseStats();

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
        RaycastHit hit = checkToDestroy();
        if(hit.transform != null){
            DestroyOtherEntity(hit);
            Destroy(this.gameObject);
        }
    }

    // destroy other entity hit if it's a valid obj
    void DestroyOtherEntity(RaycastHit hit){
        switch(LayerMask.LayerToName(hit.transform.gameObject.layer)){
            case "Enemy":
                Destroy(hit.transform.gameObject);
                break;
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

    RaycastHit checkToDestroy(){
        RaycastHit hit = new RaycastHit();
        switch(dir){
            case Direction.UP:
                Physics.Raycast(this.transform.position, Vector2.up, out hit, 0.5f);
                break;
            case Direction.DOWN:
                Physics.Raycast(this.transform.position, -Vector2.up, out hit, 0.5f);
                break;
            case Direction.LEFT:
                Physics.Raycast(this.transform.position, -Vector2.right, out hit, 0.5f);
                break;
            case Direction.RIGHT:
                Physics.Raycast(this.transform.position, Vector2.right, out hit, 0.5f);
                break;
        }
        return hit;
    }
}
