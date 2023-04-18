using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 position;
    public List<Node> connections;
    public bool walkable;

    public bool visited = false;

    public Node(Vector3 _position, bool _walkable)
    {   //initiallizes the values and creates the array
        walkable = _walkable;
        position = _position;
        connections = new List<Node>();
    }
}
