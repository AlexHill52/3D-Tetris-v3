using UnityEngine;

public class TetrisPiece : MonoBehaviour
{
    float prevTime;
    float fallTime = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - prevTime > fallTime)
        {
            transform.position += Vector3.down;
            prevTime = Time.time;
        }
    }

    bool checkValidMove()
    {
        

        return true;
    }
}
