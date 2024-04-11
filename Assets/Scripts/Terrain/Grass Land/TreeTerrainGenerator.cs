using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTerrainGenerator : MonoBehaviour
{
    public GameObject tree;
    public List<GameObject> trees;
    public List<int> rotations = new List<int> { 0, 90, 180, 270 };
    public int treeNum = 3;
    GameObject treeClone;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < treeNum; i++)
        {
            tree = trees[Random.Range(0, trees.Count)]; //Choose random Tree
            int rotationValue = rotations[Random.Range(0, rotations.Count)]; //Choose random rotation
            Quaternion rotation = Quaternion.Euler(0, rotationValue, 0);
            treeClone = Instantiate(tree, new Vector3(transform.position.x + Random.Range(-7.51f, 7.51f), transform.position.y + Random.Range(0f,-0.5f), transform.position.z + Random.Range(-7.51f, 7.51f)), rotation);
            treeClone.transform.SetParent(transform, true); //Make platform created on its parent
        }
    }
}
