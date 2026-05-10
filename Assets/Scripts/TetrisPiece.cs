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

        //right joystick (?)
        Vector2 rightHandInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        //Movement inputs
        if (Input.GetKeyDown(KeyCode.LeftArrow) || rightHandInput.x < -0.8f)
        {
            SetMoveInput(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || rightHandInput.x > 0.8f)
        {
            SetMoveInput(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || rightHandInput.y > 0.8f)
        {
            SetMoveInput(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || rightHandInput.y < -0.8f)
        {
            SetMoveInput(Vector3.back);
        }

        //left joystick (?)
        Vector2 leftHandInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        //Rotation inputs
        if (Input.GetKeyDown(KeyCode.A) || leftHandInput.x < -0.8f)
        {
            SetRotationInput(new Vector3(0,0,90));
        }
        if (Input.GetKeyDown(KeyCode.D) || leftHandInput.x > 0.8f)
        {
            SetRotationInput(new Vector3(0,0,-90));
        }
        if (Input.GetKeyDown(KeyCode.W) || leftHandInput.y > 0.8f)
        {
            SetRotationInput(new Vector3(90,0,0));
        }
        if (Input.GetKeyDown(KeyCode.S) || leftHandInput.y < -0.8f)
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
