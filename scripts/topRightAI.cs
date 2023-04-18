using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topRightAI : MonoBehaviour
{
    //this script is not actually used, only the basic pathfinding script is, however it does exist

    //states
    public enum State
    {
        chasing,
        escaping
    }
    public State currentState;

    pathfinding pathfinding;
    // Start is called before the first frame update
    void Start()
    {
        currentState = State.chasing;
        pathfinding = GetComponent<pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.chasing: //chases the player
                pathfinding.target = pathfinding.Target.player;
                break;
            case State.escaping: //runs to his corner
                pathfinding.target = pathfinding.Target.somethingElse;
                pathfinding.goalNode = pathfinding.nodes[29, 29];
                break;
        }
    }
}
