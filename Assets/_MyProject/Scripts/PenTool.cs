using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenTool : MonoBehaviour
{
    [Header("Pen Canvas")]
    [SerializeField] private PenCanvas penCanvas;

    [Header("Dots")]
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] Transform dotParent;

    [Header("Lines")]
    [SerializeField] private GameObject linePrefab;
    [SerializeField] Transform lineParent;

    private LineController currentLine;

    private void Start()
    {
        penCanvas.onPenCanvasLeftClick += AddDot;
        penCanvas.onPenCanvasRightClick += EndCurrentLine;
    }

    private void Update()
    {
        if (currentLine != null)
        {
            currentLine.PrintAllDots();
        }
    }

    private void EndCurrentLine()
    {
        if (currentLine != null)
        {
            currentLine = null;
        }
    }

    private void AddDot()
    {
        if (currentLine == null)
        {
            currentLine = Instantiate(linePrefab, Vector2.zero, Quaternion.identity, lineParent).GetComponent<LineController>();
        }

        DotController dot = Instantiate(dotPrefab, GetMousePosition(), Quaternion.identity, dotParent).GetComponent<DotController>();
        dot.onDragEvent += MoveDot;
        dot.OnRightClickEvent += RemoveDot;
        dot.OnLeftClickEvent += SetCurrentLine;
        currentLine.AddPoint(dot);
    }

    private void SetCurrentLine(DotController dot)
    {
        EndCurrentLine();
        currentLine = dot.line;
    }

    private void RemoveDot(DotController dot)
    {
        LineController line = dot.line;
        line.SplitPointsAtIndex(dot.index, out List<DotController> before, out List<DotController> after);

        Destroy(line.gameObject);
        Destroy(dot.gameObject);

        LineController beforeLine = Instantiate(linePrefab, Vector2.zero, Quaternion.identity, lineParent).GetComponent<LineController>();
        for (int i = 0; i < before.Count; i++)
        {
            beforeLine.AddPoint(before[i]);
        }

        LineController afterLine = Instantiate(linePrefab, Vector2.zero, Quaternion.identity, lineParent).GetComponent<LineController>();
        for (int i = 0; i < after.Count; i++)
        {
            afterLine.AddPoint(after[i]);
        }
    }

    private void MoveDot(DotController dot)
    {
        dot.transform.position = GetMousePosition();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition.z = 0;

        return worldMousePosition;
    }

    public void ClearDots()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.transform.name == "Dot Prefab(Clone)")
            {
                Destroy(obj);
            }

            if (obj.transform.name == "Line Prefab(Clone)")
            {
                Destroy(obj);
            }
        }
    }
}
