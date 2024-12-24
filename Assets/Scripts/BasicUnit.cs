using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BasicUnit : MonoBehaviour
{
    public enum Direction
    {
        Up, Down, Left, Right
    }
    //Attribute
    public float damage;
    public float attackRange;
    public float defense;
    public float maxHealth;
    public float curHealth;
    public float resistance;

    //Grow attributes
    public float upWeight, downWeight, leftWeight, rightWeight;
    internal Direction dir;
    internal float growCD;
    internal List<Vector3> existedUnits;
    internal BasicUnitManager manager;
    internal Vector3 newPosition = Vector3.zero;
    // Start is called before the first frame update
    public virtual void Start()
    {
        float totalWeight = upWeight + downWeight + leftWeight + rightWeight;
        upWeight /= totalWeight;
        downWeight /= totalWeight;
        leftWeight /= totalWeight;
        rightWeight /= totalWeight;
        StartCoroutine(GrowTimeCounter());
        manager = transform.parent.GetComponent<BasicUnitManager>();
        transform.localScale = Vector3.one * Mathf.Pow(0.9f, 10);
    }
    public virtual void Init()
    {
        StartCoroutine(GrowTimeCounter());
        transform.localScale = Vector3.one * Mathf.Pow(0.9f, 10);
    }
    // Update is called once per frame
    public virtual void Update()
    {

    }
    public virtual void Grow()
    {
        float growDirection = 0;
        bool canGrow=false;
        while (!canGrow)
        {
            while (growDirection == 0 || growDirection == 1)
            {
                growDirection = Random.value;
            }
            newPosition = Vector3.zero;
            if (growDirection >= 0 && growDirection < upWeight)
            {
                newPosition = this.transform.position + Vector3.up;
                dir=Direction.Up;
            }
            else if (growDirection >= upWeight && growDirection < downWeight + upWeight)
            {
                newPosition = this.transform.position + Vector3.down;
                dir=Direction.Down;
            }
            else if (growDirection >= downWeight + upWeight && growDirection < leftWeight + downWeight + upWeight)
            {
                newPosition = this.transform.position + Vector3.left;
                dir=Direction.Left;
            }
            else if (growDirection >= leftWeight + downWeight + upWeight && growDirection <= 1)
            {
                newPosition = this.transform.position + Vector3.right;
                dir=Direction.Right;
            }
            canGrow = manager.existedUnits.Exists(pos => pos == transform.position);
        }
        transform.localScale /= 0.9f;
        if (!manager.existedUnits.Exists(pos => pos == newPosition))
        {
            GameObject newUnit;
            if (manager.unitPool.Count != 0)
            {
                manager.unitPool.Peek().transform.localScale = transform.localScale * 0.9f;
                manager.unitPool.Peek().transform.position = newPosition;
                manager.unitPool.Peek().gameObject.SetActive(true);
                manager.unitPool.Peek().Init();
                manager.unitPool.Pop();
            }
            else
            {
                newUnit = Instantiate(this.gameObject, this.transform.parent);
                newUnit.name = gameObject.name;
                newUnit.transform.localScale = transform.localScale * 0.9f;
                newUnit.transform.position = newPosition;
            }
            AddMesh();
            //manager.existedUnits.Add(newPosition);
        }
        if (transform.localScale.x > 1)
        {
            manager.existedUnits.Remove(transform.position);
            manager.unitPool.Push(this);
            gameObject.SetActive(false);
        }
        Debug.Log(transform.parent.name + " Spawn once ");
    }
    void AddMesh()
    {
        Debug.Log("wabiwabi");
        manager.existedUnits.Add(newPosition);
        int positionIndex = manager.existedUnits.FindIndex(pos => pos == transform.position);
        int vertexIndex = positionIndex * 4;
        int triangleIndex = positionIndex * 4-2;
        Vector3 upleft = newPosition + Vector3.up * 0.5f - Vector3.right * 0.5f;
        Vector3 upright = newPosition + Vector3.up * 0.5f + Vector3.right * 0.5f;
        Vector3 downleft = newPosition - Vector3.up * 0.5f - Vector3.right * 0.5f;
        Vector3 downright = newPosition - Vector3.up * 0.5f + Vector3.right * 0.5f;
        Vector3[] newVertexs = new Vector3[] { upleft, upright, downleft, downright };
        
        switch (dir)
        {
            case Direction.Up:
                manager.existTriangles.Concat(new int[3] { vertexIndex, vertexIndex + 1, (manager.existedUnits.Count - 1) * 4 + 3 });
                manager.existTriangles.Concat(new int[3] { vertexIndex, (manager.existedUnits.Count - 1) * 4 + 2, (manager.existedUnits.Count - 1) * 4 + 3 });
                manager.existTriangles.Concat(new int[3] { (manager.existedUnits.Count - 1) * 4, (manager.existedUnits.Count - 1) * 4 + 1, (manager.existedUnits.Count - 1) * 4 + 2 });
                manager.existTriangles.Concat(new int[3] { (manager.existedUnits.Count - 1) * 4+1, (manager.existedUnits.Count - 1) * 4 + 2, (manager.existedUnits.Count - 1) * 4 + 3 });
                break;
            case Direction.Down:
                manager.existTriangles.Concat(new int[3] { vertexIndex+2, vertexIndex + 3, (manager.existedUnits.Count - 1) * 4});
                manager.existTriangles.Concat(new int[3] { vertexIndex+3, (manager.existedUnits.Count - 1) * 4, (manager.existedUnits.Count - 1) * 4 + 1 });
                manager.existTriangles.Concat(new int[3] { (manager.existedUnits.Count - 1) * 4, (manager.existedUnits.Count - 1) * 4 + 1, (manager.existedUnits.Count - 1) * 4 + 2 });
                manager.existTriangles.Concat(new int[3] { (manager.existedUnits.Count - 1) * 4 + 1, (manager.existedUnits.Count - 1) * 4 + 2, (manager.existedUnits.Count - 1) * 4 + 3 });
                break;
            case Direction.Left:
                manager.existTriangles.Concat(new int[3] { vertexIndex, vertexIndex + 2, (manager.existedUnits.Count - 1) * 4 +3});
                manager.existTriangles.Concat(new int[3] { vertexIndex, (manager.existedUnits.Count - 1) * 4 + 1, (manager.existedUnits.Count - 1) * 4 + 3});
                manager.existTriangles.Concat(new int[3] { (manager.existedUnits.Count - 1) * 4, (manager.existedUnits.Count - 1) * 4 + 1, (manager.existedUnits.Count - 1) * 4 + 2 });
                manager.existTriangles.Concat(new int[3] { (manager.existedUnits.Count - 1) * 4 + 1, (manager.existedUnits.Count - 1) * 4 + 2, (manager.existedUnits.Count - 1) * 4 + 3 });
                break;
            case Direction.Right:
                manager.existTriangles.Concat(new int[3] { vertexIndex+1, vertexIndex + 3, (manager.existedUnits.Count - 1) * 4 });
                manager.existTriangles.Concat(new int[3] { vertexIndex+3, (manager.existedUnits.Count - 1) * 4, (manager.existedUnits.Count - 1) * 4 + 1 });
                manager.existTriangles.Concat(new int[3] { (manager.existedUnits.Count - 1) * 4, (manager.existedUnits.Count - 1) * 4 + 1, (manager.existedUnits.Count - 1) * 4 + 2 });
                manager.existTriangles.Concat(new int[3] { (manager.existedUnits.Count - 1) * 4 + 1, (manager.existedUnits.Count - 1) * 4 + 2, (manager.existedUnits.Count - 1) * 4 + 3 });
                break;
            default:
                break;
        }
        
        manager.existVertices.AddRange(newVertexs);
        manager.mesh.vertices = manager.existVertices.ToArray();
        manager.mesh.triangles=manager.existTriangles.ToArray();
    }
    public virtual void Attack()
    {

    }
    public virtual IEnumerator GrowTimeCounter()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(growCD);
            Grow();
        }
    }
}
