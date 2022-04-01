using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    public List<DotController> dots;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;

        dots = new List<DotController>();
    }

    public void PrintAllDots()
    {
        if (dots != null)
        {
            for (int i = 0; i < dots.Count; i++)
            {
                Debug.Log(dots[i].name + " " + dots[i].index + " position: " + dots[i].transform.position);
            }
        }
    }

    public void AddPoint(DotController dot)
    {
        dot.index = dots.Count;
        dot.SetLine(this);
        lr.positionCount++;
        dots.Add(dot);
    }

    public void SplitPointsAtIndex(int index, out List<DotController> beforeDots, out List<DotController> afterDots)
    {
        List<DotController> before = new List<DotController>();
        List<DotController> after = new List<DotController>();

        int i = 0;
        for (; i < index; i++)
        {
            before.Add(dots[i]);
        }
        i++;
        for (; i < dots.Count; i++)
        {
            after.Add(dots[i]);
        }

        beforeDots = before;
        afterDots = after;
    }

    private void LateUpdate()
    {
        if (dots.Count >= 2) // Need two points to make a line
        {
            for (int i = 0; i < dots.Count; i++)
            {
                lr.SetPosition(i, dots[i].transform.position);
            }
        }
    }
}
