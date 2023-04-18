using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomLeftAI : MonoBehaviour
{
    //states
    public enum State
    {
        chasing,
        escaping,
        middle
    }
    public State currentState;
    [SerializeField] GameObject topright;
    pathfinding toprightpath;


    pathfinding pathfinding;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.middle;
        pathfinding = GetComponent<pathfinding>();
        toprightpath = topright.GetComponent<pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.middle: // averages the positions and moves towards that
                pathfinding.target = pathfinding.Target.somethingElse;
                Node middle = pathfinding.nodes[(int)(toprightpath.startNode.position.x + pathfinding.FindPlayerNode().position.x) / 2,
                                                (int)(toprightpath.startNode.position.z + pathfinding.FindPlayerNode().position.z) / 2];
                while(middle.walkable != true)
                {
                    middle = pathfinding.nodes[(int)middle.position.x - 1, (int)middle.position.y + 1];
                }

                pathfinding.goalNode = middle;
                //Debug.Log((int)(toprightpath.startNode.position.x + pathfinding.FindPlayerNode().position.x) / 2 + ", " + (int)(toprightpath.startNode.position.y + pathfinding.FindPlayerNode().position.y) / 2);
                break;

            case State.escaping: //runs to his corner
                pathfinding.target = pathfinding.Target.somethingElse;
                pathfinding.goalNode = pathfinding.nodes[1,1];
                break;

            case State.chasing: //chases the player
                pathfinding.target = pathfinding.Target.player;
                break;
        }
    }
}
