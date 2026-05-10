using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static Playfield instance;
    public int gridSizeX, gridSizeY, gridSizeZ;

    [Header("Pieces")]
    public GameObject[] pieceList;

    [Header("Playfield Visuals")]
    public GameObject bottomPlane;
    public GameObject N, S, E, W;
    public Transform[,,] theGrid;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theGrid = new Transform[gridSizeX,gridSizeY,gridSizeZ];
    }

    public Vector3 Round(Vector3 myVector)
    {
        return new Vector3(Mathf.RoundToInt(myVector.x),
                            Mathf.RoundToInt(myVector.y),
                            Mathf.RoundToInt(myVector.z));
    }

    public bool CheckInsidePlayfield(Vector3 position)
    {
        return ((int)position.x >= 0 && (int) position.x < gridSizeX &&
                (int)position.y >= 0 &&
                (int)position.z >= 0 && (int) position.z < gridSizeZ);
    }

    public void UpdatePlayfield(TetrisPiece myPiece)
    {
        //delete possible parent objects
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if(theGrid[x,y,z] != null)
                    {
                        if(theGrid[x,y,z].parent == myPiece.transform)
                        {
                            theGrid[x,y,z] = null;
                        }
                    }                    
                }
            }
        }
        //fill in child objects
        foreach(Transform child in myPiece.transform)
        {
            Vector3 pos = Round(child.position);
            if(pos.y < gridSizeY)
            {
                theGrid[(int)pos.x, (int)pos.y, (int)pos.z] = child;
            }
        }
    }

    public Transform GetTransformOnGridPos(Vector3 pos)
    {
        if(pos.y > gridSizeY - 1)
        {
            return null;
        }
        else
        {
            return theGrid[(int)pos.x, (int)pos.y, (int)pos.z];
        }
    }
    
    public void SpawnNewBlock()
    {
        Vector3 spawnPoint = new Vector3((int)(transform.position.x + (float)gridSizeX / 2),
                                            (int)(transform.position.y + gridSizeY),
                                            (int)(transform.position.z + (float)gridSizeZ / 2));
        int randomIndex = Random.Range(0, pieceList.Length);
        
        GameObject newPiece = Instantiate(pieceList[randomIndex], spawnPoint, Quaternion.identity) as GameObject;
    }
    
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
