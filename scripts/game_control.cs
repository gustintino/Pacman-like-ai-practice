using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_control : MonoBehaviour
{
    public GameObject[] collectibles;

    public GameObject topLeft;
    public GameObject topRight;
    public GameObject bottomLeft;
    public GameObject bottomRight;

    public int current;
    int total;
    bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        total = collectibles.Length;
        current = total;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(once)
        {
            if (current < total * 0.34f)
            {   //sets all the ghosts to start chasing, except for one, which already is
                topLeft.GetComponent<topLeftAI>().currentState = topLeftAI.State.chasing;
                //topRight.GetComponent<topRightAI>().currentState = topRightAI.State.chasing;
                bottomLeft.GetComponent<bottomLeftAI>().currentState = bottomLeftAI.State.chasing;
                bottomRight.GetComponent<bottomRightAI>().currentState = bottomRightAI.State.chasing;
                once = false;
            }
        }
        //if all collectibles are gathered, loads other scene
        if(current == 0)
        {
            SceneManager.LoadScene("over");
        }
    }
}
