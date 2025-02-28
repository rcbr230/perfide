using UnityEngine;

public class PCController : MonoBehaviour
{

    // private variables
    private Transform playerTransform;
    private Direction Facing;

    // public variables
    public GameObject shot;

    // timing delays private
    private float lastMoved;
    private float lastAttacked;

    // public timing delays
    public float attackDelay;
    public float movementDelay;

    



    // enum for directions
    enum Direction{
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = this.GetComponent<Transform>();

        lastMoved =     -movementDelay;
        lastAttacked =  -attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastMoved+movementDelay <= Time.time && CheckMovement()){
            lastMoved = Time.time;
        }
        if(lastAttacked+attackDelay <= Time.time && CheckAttack()){
            lastAttacked = Time.time;
        }
    }



    /*
    * Check for a user attacking, fire in the direction they are facing.
    */
    bool CheckAttack(){
        if(Input.GetKey(KeyCode.Space)){
            GameObject sentShot = Instantiate(shot);
            switch (Facing){
                case Direction.UP:
                    sentShot.transform.position = new Vector2(this.transform.position.x, this.transform.position.y+1);
                    break;
                case Direction.DOWN:
                    sentShot.transform.position = new Vector2(this.transform.position.x, this.transform.position.y-1);
                    sentShot.transform.Rotate(new Vector3(0.0f,0.0f,180.0f));
                    break;
                case Direction.LEFT:
                    sentShot.transform.position = new Vector2(this.transform.position.x-1, this.transform.position.y);
                    sentShot.transform.Rotate(new Vector3(0.0f,0.0f,90.0f));
                    break;
                case Direction.RIGHT:
                    sentShot.transform.position = new Vector2(this.transform.position.x+1, this.transform.position.y);
                    sentShot.transform.Rotate(new Vector3(0.0f,0.0f,270.0f));
                    break;
            }
            return true;
        }
        return false;
    }










    /*
    * Check user movement using wsad
    * Also check for an obstacle in the way and stop the player from moving
    */
    bool CheckMovement(){
        if(Input.GetKey(KeyCode.W)){
            if(isWall(Direction.UP)){
                return false;
            }
            Facing = Direction.UP;
            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 1);
            return true;
        }
        if(Input.GetKey(KeyCode.S)){
            if(isWall(Direction.DOWN)){
                return false;
            }
            Facing = Direction.DOWN;
            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 1);
            return true;
        }
        if(Input.GetKey(KeyCode.A)){
            if(isWall(Direction.LEFT)){
                return false;
            }
            Facing = Direction.LEFT;
            playerTransform.position = new Vector3(playerTransform.position.x - 1, playerTransform.position.y);
            return true;
        }
        if(Input.GetKey(KeyCode.D)){
            if(isWall(Direction.RIGHT)){
                return false;
            }
            Facing = Direction.RIGHT;
            playerTransform.position = new Vector3(playerTransform.position.x + 1, playerTransform.position.y);
            return true;
        }
        return false;
    }

    /*
    * Use raycast to check if there is something in the way of the user
    * dir is an enumerator: 0-up 1-down 2-left 3-right
    */
    bool isWall(Direction dir){
        switch (dir){
            // up
            case Direction.UP:
                if(Physics.Raycast(playerTransform.position, playerTransform.up, 1.0f)){
                    return true;
                }
                break;
            case Direction.DOWN:
                if(Physics.Raycast(playerTransform.position, -playerTransform.up, 1.0f)){
                    return true;
                }
                break;
            case Direction.LEFT:
                if(Physics.Raycast(playerTransform.position, -playerTransform.right, 1.0f)){
                    return true;
                }
                break;
            case Direction.RIGHT:
                if(Physics.Raycast(playerTransform.position, playerTransform.right, 1.0f)){
                    return true;
                }
                break;
        }
        return false;
    }
}
