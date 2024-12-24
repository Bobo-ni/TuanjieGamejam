using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnitManager : MonoBehaviour
{
    public List<Vector3> existedUnits = new List<Vector3>();
    public Stack<BasicUnit> unitPool = new Stack<BasicUnit>();
    public Mesh mesh;
    public List<Vector3> existVertices = new List<Vector3>();
    public List<int> existTriangles = new List<int>();
    MeshFilter filter;
    // Start is called before the first frame update
    public virtual void Start()
    {
        mesh = new Mesh();
        filter = gameObject.AddComponent<MeshFilter>();
        filter.mesh = mesh;
        existedUnits.Add(transform.GetChild(0).position);
        Vector3 firstMesh = transform.GetChild(0).position;
        Vector3 upleft = firstMesh + Vector3.up * 0.5f - Vector3.right * 0.5f;
        Vector3 upright = firstMesh + Vector3.up * 0.5f + Vector3.right * 0.5f;
        Vector3 downleft = firstMesh - Vector3.up * 0.5f - Vector3.right * 0.5f;
        Vector3 downright = firstMesh - Vector3.up * 0.5f + Vector3.right * 0.5f;
        mesh.vertices= new Vector3[] { upleft, upright, downleft, downright };
        mesh.triangles = new int[] { 0, 1, 2, 1, 2, 3 };
    }

    // Update is called once per frame
    void Update()
    {
        filter.mesh = mesh;
    }
}
