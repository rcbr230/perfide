using UnityEngine;

public class EnvironmentEntity : MonoBehaviour
{
    protected GameObject gameManager;
    protected int PrevStep;

    protected bool checkGameTick(){
        int currentStep = gameManager.GetComponent<GameController>().GameTicks;

        if(PrevStep < currentStep){
            PrevStep = currentStep;
            return true;
        }
        return false;
    }

    protected void createBaseStats(){
        gameManager = GameObject.Find("GameManager");
        PrevStep = gameManager.GetComponent<GameController>().GameTicks;
    }
}
