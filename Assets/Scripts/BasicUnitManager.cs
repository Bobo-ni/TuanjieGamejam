using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnitManager : MonoBehaviour
{
    public List<Vector3> existedUnits = new List<Vector3>();
    public Stack<BasicUnit> unitPool = new Stack<BasicUnit>();
    // Start is called before the first frame update
    public virtual void Start()
    {
        existedUnits.Add(transform.GetChild(0).position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
