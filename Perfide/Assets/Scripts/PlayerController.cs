using UnityEngine;

public class PlayerController
{
    
    void Start()
    {
        Debug.Log("starting player controller");
    }
    void Update()
    {
        WASDMovement();
    }

    bool WASDMovement()
    {
        float LRMovement = Input.GetAxis("Horizontal");
        float UDMovement = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(LRMovement, UDMovement);

        this.
        return false;
    }
}
