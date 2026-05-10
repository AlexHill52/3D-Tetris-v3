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
            if (!CheckValidMove())
            {
                transform.position += Vector3.up;
                //check to delete layer

                enabled = false;
                //make a new piece
                Playfield.instance.SpawnNewBlock();
            }
            else
            {
                //update the grid
                Playfield.instance.UpdatePlayfield(this);
            }

            prevTime = Time.time;
        }
    }

    bool CheckValidMove()
    {
        foreach (Transform child in transform)
        {
            Vector3 pos = Playfield.instance.Round(child.position);
            if (!Playfield.instance.CheckInsidePlayfield(pos))
            {
                return false;
            }
        }

        foreach (Transform child in transform)
        {
            Vector3 pos = Playfield.instance.Round(child.position);
            Transform t = Playfield.instance.GetTransformOnGridPos(pos);
            if (t != null && t.parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
