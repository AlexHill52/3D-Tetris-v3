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

        //Movement inputs
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetMoveInput(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetMoveInput(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetMoveInput(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetMoveInput(Vector3.back);
        }

        //Rotation inputs
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetRotationInput(new Vector3(0,0,90));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetRotationInput(new Vector3(0,0,-90));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetRotationInput(new Vector3(90,0,0));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetRotationInput(new Vector3(-90,0,0));
        }
    }

    public void SetMoveInput(Vector3 direction)
    {
        transform.position += direction;
        if (!CheckValidMove())
        {
            transform.position -= direction;
        }
        else
        {
            Playfield.instance.UpdatePlayfield(this);
        }
    }

    public void SetRotationInput(Vector3 rotation)
    {
        transform.Rotate(rotation, Space.World);
        if (!CheckValidMove())
        {
            transform.Rotate(-rotation, Space.World);
        }
        else
        {
            Playfield.instance.UpdatePlayfield(this);
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
