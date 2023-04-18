using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomRightAI : MonoBehaviour
{
    //possibles states
    public enum State
    {
        patrollingUp,
        patrollingDown,
        chasing,
        escaping
    }
    public State currentState;

    pathfinding pathfinding;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.patrollingUp;
        pathfinding = GetComponent<pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {   //patrolling up and down makes the ghosts circle the bottom right portion
            case State.patrollingUp:
                pathfinding.target = pathfinding.Target.somethingElse;
                pathfinding.goalNode = pathfinding.nodes[21, 13];
                if (pathfinding.startNode == pathfinding.goalNode)
                {
                    currentState = State.patrollingDown;
                }
                break;
            case State.patrollingDown:
                pathfinding.target = pathfinding.Target.somethingElse;
                pathfinding.goalNode = pathfinding.nodes[29,1];
                if (pathfinding.startNode == pathfinding.goalNode)
                {
                    currentState = State.patrollingUp;
                }
                break;
            case State.escaping: // runs to his corner
                pathfinding.target = pathfinding.Target.somethingElse;
                pathfinding.goalNode = pathfinding.nodes[29,1];
                break;
            case State.chasing: // chases the player
                pathfinding.target = pathfinding.Target.player;
                break;
        }
    }

}
