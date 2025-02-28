using UnityEngine;

public class GameController : MonoBehaviour
{

    // public variables
    public GameObject Player; 

    private bool playerMoved;
    private Vector2 prevPosition;
    private int GameTicks = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prevPosition = Player.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        // on player move, update game ticker, and perform environment actions
        Vector2 currentPlayerPos = Player.GetComponent<Transform>().position;
        if(currentPlayerPos != prevPosition){
            GameTicks += 1;
            // run steps for environment
        }
    }
}
