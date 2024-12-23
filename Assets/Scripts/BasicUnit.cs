using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnit : MonoBehaviour
{
    //Attribute
    public float damage;
    public float attackRange;
    public float defense;
    public float maxHealth;
    public float curHealth;
    public float resistance;

    //Grow attributes
    [SerializeField]
    internal float upWeight, downWeight, leftWeight, rightWeight;
    internal float growCD;
    internal List<Vector3> existedUnits;
    internal BasicUnitManager manager;
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

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    public virtual void Grow()
    {
        float growDirection = 0;
        while (growDirection ==0 || growDirection ==1)
        {
            growDirection = Random.value;
        }
        Vector3 newPosition = Vector3.zero;
        if (growDirection >= 0 && growDirection < upWeight)
            newPosition = this.transform.position+Vector3.up;
        else if (growDirection >= upWeight && growDirection < downWeight + upWeight)
            newPosition = this.transform.position+Vector3.down;
        else if (growDirection >= downWeight + upWeight && growDirection < leftWeight + downWeight + upWeight)
            newPosition = this.transform.position+Vector3.left;
        else if (growDirection >= leftWeight + downWeight + upWeight && growDirection <= 1)
            newPosition = this.transform.position +Vector3.right;
        transform.localScale /= 0.9f;
        if(!manager.existedUnits.Exists(pos => pos == newPosition))
        {
            GameObject newUnit = Instantiate(this.gameObject, this.transform.parent);
            newUnit.transform.localScale = transform.localScale * 0.9f;
            newUnit.transform.position = newPosition;
            manager.existedUnits.Add(newPosition);
        }
        if(transform.localScale.x >1)
        {
            manager.existedUnits.Remove(transform.position);
            Destroy(gameObject);
        }
        Debug.Log(transform.parent.name + " Spawn once ");
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
