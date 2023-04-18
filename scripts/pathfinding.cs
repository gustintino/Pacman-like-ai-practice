using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pathfinding : MonoBehaviour
{   //multiple layers to find the starter and goal nodes
    [SerializeField] LayerMask player;
    [SerializeField] LayerMask plyr; //the current ghost node, was called player in the beggining

    //queue and dictionary for the pathfinding
    Queue que = new Queue();
    Dictionary<Node, Node> dictionary = new Dictionary<Node, Node>();

    public Node startNode;
    public Node goalNode;

    public List<Node> path = new List<Node>(); //returns an actual path from pathfinding
    public Node[,] nodes;

    public enum Target
    {
        player,
        somethingElse
    }
    public Target target;

    // Start is called before the first frame update
    void Start()
    {
        //gets the node graph and sets the default path
        nodes = GameObject.Find("graph").GetComponent<grpah>().nodes;
        target = Target.player;
        goalNode = FindPlayerNode();
        startNode = FindStartNode();
        Breadth();
        Pathing();
        if(path.Count > 0)
        {
            moveTo = path[0];
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moves only if the node exists
        if(moveTo != null)
        {
            if (MoveTowardsNode(moveTo))
            {
                if (path.Count > 0)
                    moveTo = path[0];
            }
        }
        

        //checks the current state
        switch (target)
        {
            case Target.player:
                Node playerNode = FindPlayerNode();
                if (playerNode != goalNode)
                {
                    goalNode = playerNode;
                    goalNode = FindPlayerNode();
                    Breadth();
                    Pathing();
                }
                break;

            case Target.somethingElse:
                Breadth();
                Pathing();
                break;
        }

        


        //updates the start node (because it moves)
        Node newStart = FindStartNode();
        if (newStart != startNode)
        {
            startNode = newStart;
            dictionary.Clear();
            Breadth();
            Pathing();
        }


    }

    //moves the ghosts, is a bool because it moves from node to node and return true only when it reaches the next one
    Node moveTo;
    bool MoveTowardsNode(Node node)
    {
        if (transform.position != node.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, node.position, 0.2f);
            return false;
        }
        else
            return true;
    }

    //find the start node, aka the current node of the ghost
    Node FindStartNode()
    {
        foreach (Node node in nodes)
        {
            if (Physics.CheckBox(node.position, new Vector3(0.01f, 0.01f, 0.01f), Quaternion.identity, plyr))
            {
                return node;
            }
        }

        return null;
    }

    //find the player
    public Node FindPlayerNode()
    {
        foreach (Node node in nodes)
        {
            if (Physics.CheckBox(node.position, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, player))
            {
                return node;
            }
        }

        return null;
    }

    //goes backwards in the dictionary from the goal node and goes to the start, creates and actual useable path
    public void Pathing()
    {
        path.Clear();
        Node current = goalNode;
        while (current != startNode && current != null)
        {
            path.Add(current);
            current = dictionary[current];
        }
        path.Reverse();
        //asdf = path[0];
    }

    //pathfinding
    void Breadth()
    {
        foreach (Node node in nodes)
        {
            node.visited = false;
        }
        que.Clear();
        que.Enqueue(startNode);

        startNode.visited = true;

        while (que.Count != 0)
        {
            Node current = (Node)que.Peek();
            que.Dequeue();

            foreach (Node child in current.connections)
            {
                if (child.visited == false && child.walkable)
                {
                    child.visited = true;
                    dictionary[child] = current;

                    if (child == goalNode)
                    {
                        return;
                    }
                    else
                    {
                        que.Enqueue(child);
                    }
                }
            }
        }

        return;
    }

    //visualizations
    private void OnDrawGizmos()
    {
    
    
        foreach (Node n in nodes)
        {
            if (n == goalNode)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(n.position, new Vector3(0.6f, 0.6f, 0.6f));
            }
            else if (!n.walkable)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(n.position, new Vector3(0.4f, 0.4f, 0.4f));
            }
            else if (n == startNode)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(n.position, new Vector3(0.6f, 0.6f, 0.6f));
            }
            else
            {
                //Gizmos.color = Color.gray;
                //Gizmos.DrawCube(n.position, new Vector3(0.4f, 0.4f, 0.4f));
            }
    
        }
    
        foreach (Node node in path)
        {
            if (node != goalNode)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(node.position, new Vector3(0.5f, 0.5f, 0.5f));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if(collision.gameObject.name == "goal")
        {
            SceneManager.LoadScene("over");

        }
    }
}
