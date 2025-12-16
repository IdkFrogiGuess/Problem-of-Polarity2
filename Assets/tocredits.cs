using UnityEngine;

public class tocredits : StateMachineBehaviour
{
    public int NextLevel;


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextLevel);
    }

    
}
