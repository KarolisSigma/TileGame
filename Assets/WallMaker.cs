using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaker : MonoBehaviour
{
    public GameObject wallPrefab;
    public Transform parent;
    public Vector2 size;

    void Start(){
        MakeWalls();
    }

    public void MakeWalls(){
        Vector2 planesize=size/10f;
        Transform tf = wallPrefab.GetComponent<Transform>().GetChild(0);
        tf.localScale = new Vector3(planesize.x, 1, planesize.y);
        tf.position = new Vector3(0,-size.x/2f,0);

        
        Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(0,0,180), parent);
        Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(90,0,180), parent);
        Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(90,0,0), parent);
        Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(90,90,0), parent);
        Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(90,270,0), parent);

        parent.transform.position += new Vector3(0, size.x/2f-0.5f ,0);
    }
}
