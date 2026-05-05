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

            //Retile
            bottomPlane.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeZ);
        }

        if(N != null)
        {
            //Resize plane
            Vector3 scaler = new Vector3((float)gridSizeX/10, 1, (float)gridSizeY/10);
            N.transform.localScale = scaler;

            //Reposition bottom plane
            N.transform.position = new Vector3(transform.position.x + (float)gridSizeX/2,
                                                transform.position.y + (float)gridSizeY/2,
                                                transform.position.z + (float)gridSizeZ);

            //Retile
            N.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }

        if(S != null)
        {
            //Resize plane
            Vector3 scaler = new Vector3((float)gridSizeX/10, 1, (float)gridSizeY/10);
            S.transform.localScale = scaler;

            //Reposition bottom plane
            S.transform.position = new Vector3(transform.position.x + (float)gridSizeX/2,
                                                transform.position.y + (float)gridSizeY/2,
                                                transform.position.z);

            //Retile
            //S.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);
        }

        if(E != null)
        {
            //Resize plane
            Vector3 scaler = new Vector3((float)gridSizeZ/10, 1, (float)gridSizeY/10);
            E.transform.localScale = scaler;

            //Reposition bottom plane
            E.transform.position = new Vector3(transform.position.x + (float)gridSizeX,
                                                transform.position.y + (float)gridSizeY/2,
                                                transform.position.z + (float)gridSizeZ/2);

            //Retile
            E.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeZ, gridSizeY);
        }

        if(W != null)
        {
            //Resize plane
            Vector3 scaler = new Vector3((float)gridSizeZ/10, 1, (float)gridSizeY/10);
            W.transform.localScale = scaler;

            //Reposition bottom plane
            W.transform.position = new Vector3(transform.position.x,
                                                transform.position.y + (float)gridSizeY/2,
                                                transform.position.z + (float)gridSizeZ/2);

            //Retile
            //W.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeZ, gridSizeY);
        }
    }
}
