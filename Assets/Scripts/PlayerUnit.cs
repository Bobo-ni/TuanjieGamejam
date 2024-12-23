using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BasicUnit
{
    public Vector2 centerPoint;
    public Vector2 targetPoint;
    // Start is called before the first frame update
    public override void Start()
    {
        growCD = 1;
        base.Start();
    }
    public override void Grow()
    {
        //urHealth++;
        base.Grow();
    }
}
