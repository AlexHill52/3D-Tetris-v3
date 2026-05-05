using UnityEngine;

public class Playfield : MonoBehaviour
{
    public GameObject bottomPlane;
    public GameObject N, S, E, W;

    public int gridSizeX, gridSizeY, gridSizeZ;
    public Transform[,,] theGrid;

    void OnDrawGizmos()
    {
        if(bottomPlane != null)
        {
            //Resize bottom plane
            Vector3 scaler = new Vector3((float)gridSizeX/10, 1, (float)gridSizeZ/10);
            bottomPlane.transform.localScale = scaler;

            //Reposition bottom plane
            bottomPlane.transform.position = new Vector3(transform.position.x + (float)gridSizeX/2,
                                                            transform.position.y,
                                                            transform.position.z + (float)gridSizeZ/2);
        }
    }
}
