using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BasicUnitManager
{
    public Vector2 centerPoint;
    public Vector2 targetPoint;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCenter();
    }

    public void CalculateCenter()
    {
        Vector3 temp = Vector3.zero;
        for (int i = 0; i < existedUnits.Count; i++)
        {
            temp += existedUnits[i];
        }
        centerPoint = temp / existedUnits.Count;
    }
}
