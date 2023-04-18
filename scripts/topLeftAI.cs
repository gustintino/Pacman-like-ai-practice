using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topLeftAI : MonoBehaviour
{
    //states
    public enum State
    {
        patrollingRight,
        patrollingLeft,
        chasing,
        escaping
    }
    public State currentState;

    pathfinding pathfinding;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.patrollingRight;
        pathfinding = GetComponent<pathfinding>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       switch(currentState)
        {
            case State.patrollingRight: //makes the ghost circle the top left portion
                pathfinding.target = pathfinding.Target.somethingElse;
                pathfinding.goalNode = pathfinding.nodes[24, 24];
                if(pathfinding.startNode == pathfinding.goalNode)
                {
                    currentState = State.patrollingLeft;
                }
                break;
            case State.patrollingLeft:
                pathfinding.target = pathfinding.Target.somethingElse;
                pathfinding.goalNode = pathfinding.nodes[1, 29];
                if (pathfinding.startNode == pathfinding.goalNode)
                {
                    currentState = State.patrollingRight;
                }
                break;
            case State.escaping: //runs to his corner
                pathfinding.target = pathfinding.Target.somethingElse;
                pathfinding.goalNode = pathfinding.nodes[1, 29];
                break;
            case State.chasing: //chases the player
                pathfinding.target = pathfinding.Target.player;
                break;
        }
    }
}
