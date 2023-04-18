using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grpah : MonoBehaviour
{

    [SerializeField] LayerMask barrier;

    public Node[,] nodes;
    int gridSize = 31;

    

    void Awake()
    {   //inital setup
       
        nodes = new Node[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            { // checks each node and sees if there is a barrier there
                Vector3 position = new Vector3(x, 0, y);
                bool walkable = !Physics.CheckBox(position, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, barrier);
                nodes[x, y] = new Node(position, walkable);
            }
        }

        FindConnections();
    }

    void FixedUpdate()
    {

    }
    
    public void FindConnections()
    { //adds neighbouring nodes to each nodes' connections
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Node node = nodes[x, y];
                //Debug.Log(x + "," + y);

                

                if (x > 0)
                {
                    node.connections.Add(nodes[x - 1, y]);
                }
                if (x < gridSize - 1) 
                { 
                    node.connections.Add(nodes[x + 1, y]); 
                }
                if (y < gridSize - 1)
                {
                    node.connections.Add(nodes[x, y + 1]);
                }
                if (y > 0)
                {
                    node.connections.Add(nodes[x, y - 1]);
                }

            }
        }
    }

    //for drawing purposes, not needed
    //private void OnDrawGizmos()
    //{
    //    for(int i = 0; i < gridSize; i++)
    //    {
    //        for(int j = 0; j < gridSize; j++)
    //        {
    //            Gizmos.color = Color.grey;
    //            Gizmos.DrawCube(new Vector3(i, 0, j), new Vector3(0.6f, 0.6f, 0.6f));
    //        }
    //    }
    //}

}
