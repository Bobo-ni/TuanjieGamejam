using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BasicUnit
{
    // Start is called before the first frame update
    public override void Start()
    {
        upWeight = 0.25f;
        downWeight = 0.25f;
        leftWeight = 0.25f;
        rightWeight = 0.25f;
        float totalWeight = upWeight + downWeight+leftWeight+rightWeight;
        upWeight /= totalWeight;
        downWeight /= totalWeight;
        leftWeight /= totalWeight;
        rightWeight /= totalWeight;
        growCD = 1;
        base.Start();
    }
    public override void Grow()
    {
        base.Grow();
    }
}
