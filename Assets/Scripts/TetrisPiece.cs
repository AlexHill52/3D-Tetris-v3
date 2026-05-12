using UnityEngine;

public class TetrisPiece : MonoBehaviour
{
    float prevTime;
    float fallTime = 1f;
     public float inputCooldown = 0.15f;
     private float lastInputTime;

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
                Playfield.instance.DeleteLayer();

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

        if (Time.time - lastInputTime < inputCooldown)
        {
            return;
        }

        //right joystick
        Vector2 rightHandInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        //Movement inputs
        if (Input.GetKey(KeyCode.LeftArrow) || rightHandInput.x < -0.8f)
        {
            SetMoveInput(Vector3.left);
        }
        if (Input.GetKey(KeyCode.RightArrow) || rightHandInput.x > 0.8f)
        {
            SetMoveInput(Vector3.right);
        }
        if (Input.GetKey(KeyCode.UpArrow) || rightHandInput.y > 0.8f)
        {
            SetMoveInput(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.DownArrow) || rightHandInput.y < -0.8f)
        {
            SetMoveInput(Vector3.back);
        }

        //left joystick
        Vector2 leftHandInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        //Rotation inputs
        if (Input.GetKey(KeyCode.A) || leftHandInput.x < -0.8f)
        {
            SetRotationInput(new Vector3(0,0,90));
        }
        if (Input.GetKey(KeyCode.D) || leftHandInput.x > 0.8f)
        {
            SetRotationInput(new Vector3(0,0,-90));
        }
        if (Input.GetKey(KeyCode.W) || leftHandInput.y > 0.8f)
        {
            SetRotationInput(new Vector3(90,0,0));
        }
        if (Input.GetKey(KeyCode.S) || leftHandInput.y < -0.8f)
        {
            SetRotationInput(new Vector3(-90,0,0));
        }

        //Hard-drop input
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.Three))
        {
            fallTime = 0.01f;
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
        lastInputTime = Time.time;
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
        lastInputTime = Time.time;
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
