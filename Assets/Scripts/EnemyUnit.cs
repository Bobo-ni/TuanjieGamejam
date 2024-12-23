using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BasicUnit
{
    // Start is called before the first frame update
    public override void Start()
    {
        growCD = 1;
        base.Start();
    }
    public override void Grow()
    {
        base.Grow();
    }
}
