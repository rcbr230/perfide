using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Unity.Collections;
using UnityEngine;

public class BasicEnemyBehavior : EnvironmentEntity
{
 
 
 // enum for directions
    enum Direction{
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3
    }


    private bool foundPlayer = false;
    private GameObject player;
    private Vector2 lastSeenLocation = new Vector2(10000,10000);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createBaseStats();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        tryToKill();
        foundPlayer = searchForPlayer();
        // only do actions when the player moves
        if (!checkGameTick()){
            return;
        }

        if(foundPlayer){
            moveTowardsTransform(player.transform.position);
        } else {
            defaultMovement();
        }
    }

    // do raycasts in 360 around entity, if hits player return true
    bool searchForPlayer(){
        
        int RaysToShoot = 40;

        float angle = 0;
        for(int i=0; i<RaysToShoot; i++) {
            // unit circle stuff for angle:
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);
            // 2pi = full circle
            angle += 2.0f * Mathf.PI / RaysToShoot;
            Vector3 dir = new Vector3 (x, y, 0);
            RaycastHit hit; 

            // perform raycast and see if the player layer is hit
            if (Physics.Raycast(this.transform.position, dir, out hit)) {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")){
                    lastSeenLocation = hit.transform.position;
                    return true;
                }
            }
        }
        return false;
    }

    // movement when player is not in sight
    void defaultMovement(){
        // go to last seen location
        if(lastSeenLocation.x < 100){
            // reached last location
            int xOverlap = (int)lastSeenLocation.x - (int)this.transform.position.x;
            int yOverlap = (int)lastSeenLocation.y - (int)this.transform.position.y;
            if(xOverlap == 0 && yOverlap == 0){
                lastSeenLocation = new Vector2(10000, 10000);
            }
            moveTowardsTransform(lastSeenLocation);

        }
    }

    // movement when player is in sight
    void moveTowardsTransform(Vector3 playerPos){
        float xOffset = playerPos.x - this.transform.position.x;
        float yOffset = playerPos.y - this.transform.position.y;

        // random change to move the opposite to make it look more human. Ignored when on a straight line
        System.Random rand = new System.Random();
        float randChance = rand.Next(100);

        if(Math.Abs(xOffset) > Math.Abs(yOffset)){
            Direction dir = xOffset > 0 ? Direction.RIGHT : Direction.LEFT;
            Direction Rdir = yOffset > 0 ? Direction.UP : Direction.DOWN;
            if(yOffset != 0 && randChance <= 10){
                moveEnemy(Rdir);
            } else {
                moveEnemy(dir);
            }
        } else{
            Direction dir = yOffset > 0 ? Direction.UP : Direction.DOWN;
            Direction Rdir = xOffset > 0 ? Direction.RIGHT : Direction.LEFT;
            if(xOffset != 0 && randChance <= 10){
                moveEnemy(Rdir);
            } else {
                moveEnemy(dir);
            }
        }
    }

    // move entity a certain direction 
    void moveEnemy(Direction dir){
        switch(dir){
            case Direction.UP:
                if(isWall(Direction.UP)){
                    return;
                }
                this.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
                break;
            case Direction.DOWN:
                if(isWall(Direction.DOWN)){
                    return;
                }
                this.transform.position += new Vector3(0.0f, -1.0f, 0.0f);
                break;
            case Direction.LEFT:
                if(isWall(Direction.LEFT)){
                    return;
                }
                this.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
                break;
            case Direction.RIGHT:
                if(isWall(Direction.RIGHT)){
                    return;
                }
                this.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
                break;
        }
    }

    bool tryToKill(){
        int xOverlap = (int)player.transform.position.x - (int)this.transform.position.x;
        int yOverlap = (int)player.transform.position.y - (int)this.transform.position.y;
        if(xOverlap == 0 && yOverlap == 0){
            Destroy(player);
            return true;
        }
        return false;
    }

    bool isWall(Direction dir){
        RaycastHit hit;

        switch (dir){
            // up
            case Direction.UP:
                if(Physics.Raycast(this.transform.position, Vector2.up, out hit, 1.0f)){
                    if(LayerMask.NameToLayer("Wall") == hit.transform.gameObject.layer){
                        return true;
                    }
                }
                break;
            case Direction.DOWN:
                if(Physics.Raycast(this.transform.position, -Vector2.up, out hit, 1.0f)){
                    if(LayerMask.NameToLayer("Wall") == hit.transform.gameObject.layer){
                        return true;
                    }
                }
                break;
            case Direction.LEFT:
                if(Physics.Raycast(this.transform.position, -Vector2.right, out hit, 1.0f)){
                    if(LayerMask.NameToLayer("Wall") == hit.transform.gameObject.layer){
                        return true;
                    }
                }
                break;
            case Direction.RIGHT:
                if(Physics.Raycast(this.transform.position, Vector2.right, out hit, 1.0f)){
                    if(LayerMask.NameToLayer("Wall") == hit.transform.gameObject.layer){
                        return true;
                    }
                }
                break;
        }
        return false;
    }
}
