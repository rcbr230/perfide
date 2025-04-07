using UnityEditor.SearchService;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private GameObject Player; 
    private GameObject exit;
    private bool playerMoved;
    private Vector2 prevPosition;
    public int GameTicks = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.Find("Player");
        exit = GameObject.Find("Exit");
        prevPosition = Player.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        // on player move, update game ticker, and perform environment actions
        Vector2 currentPlayerPos = Player.GetComponent<Transform>().position;
        if(currentPlayerPos != prevPosition){
            prevPosition = currentPlayerPos;
            GameTicks += 1;
            // run steps for environment
        }

        if(CheckPlayerExit()){
            // end game/round
            Debug.Log("END GAME");
        }
    }

    bool CheckPlayerExit(){
        int xOffset = (int)Player.transform.position.x - (int)exit.transform.position.x;
        int yOffset = (int)Player.transform.position.y - (int)exit.transform.position.y;
        int checkVal = xOffset + yOffset;
        if(checkVal == 0){
            return true;
        }
        return false;
    }
}
