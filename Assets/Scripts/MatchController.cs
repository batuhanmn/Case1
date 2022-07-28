using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : Singleton<MatchController>
{
    public Dictionary<int, GridElement> Grids = new Dictionary<int, GridElement>();
    int columnSize=0;
    public int[,] gridMatris = new int[3,3];

    public void Mark2Matris(int id, int fill=1, bool isControl = true) // mark the selected area into the matrix
    {
        int tmp_queue = 0;
        bool isFilled = false;
        for (int i = 0; i < columnSize; i++)
        {
            for (int j = 0; j < columnSize; j++)
            {
                if (tmp_queue == id)
                {
                    gridMatris[i, j] = fill;
                    if (isControl)
                    {
                        Check(i, j);
                    }
                    isFilled = true;
                    break;
                }
                tmp_queue+=1;
            }
            if (isFilled) break;
        }
    }


    private bool CheckValidity(Point p) // is it filled
    {
        if (p.x < 0 || p.x >= columnSize)
        {
            return false;
        }
        if (p.y < 0 || p.y >= columnSize)
        {
            return false;
        }

        var isFill = gridMatris[p.x, p.y];
        if (isFill == 1) return true;
        else return false;
    }

    private void Check(int i, int j)
    {
        Dictionary<Vector2, bool> registry = new Dictionary<Vector2, bool>();
        var q = new Queue<Point>(columnSize * columnSize);
        q.Enqueue(new Point(i, j));
        int iterations = 0;
        while (q.Count > 0)
        {
            var point = q.Dequeue();
            var x1 = point.x;
            var y1 = point.y;

            if (q.Count > columnSize * columnSize)
            {
                throw new System.Exception("Queue size: " + q.Count);
            }

            var newPoint = new Point(x1 + 1, y1); //checking a battom row
            if (CheckValidity(newPoint))
            {
                if (!registry.ContainsKey(newPoint.GetVector2()))
                {
                    registry.Add(newPoint.GetVector2(), true);
                    q.Enqueue(newPoint);
                }
            }

            newPoint = new Point(x1 - 1, y1); //checking a top row
            if (CheckValidity(newPoint))
            {
                if (!registry.ContainsKey(newPoint.GetVector2())) {
                    registry.Add(newPoint.GetVector2(), true);
                    q.Enqueue(newPoint);
                }
            }

            newPoint = new Point(x1, y1 + 1); //checking a right column
            if (CheckValidity(newPoint))
            {
                if (!registry.ContainsKey(newPoint.GetVector2()))
                {
                    registry.Add(newPoint.GetVector2(), true);
                    q.Enqueue(newPoint);
                }
            }

            newPoint = new Point(x1, y1 - 1); //checking a left column
            if (CheckValidity(newPoint))
            {
                if (!registry.ContainsKey(newPoint.GetVector2()))
                {
                    registry.Add(newPoint.GetVector2(), true);
                    q.Enqueue(newPoint);
                }
            }

            iterations += 1;

        }
        if (registry.Keys.Count >= 3) //merge three and more than three matches
        {
            MatchCounter.Instance.UpdateCount(1);
            foreach (var item in registry)
            {
                int tmp_gridId = (int)item.Key.x*columnSize + (int)item.Key.y;
                Grids[tmp_gridId].MatchedGrid();
            }
        }
    }

    
    public void CreateMatris(int b) // create a given size matrix
    {
        Debug.Log("Created " + b + "x" + b + " matris");
        columnSize = b;
        gridMatris = new int[columnSize, columnSize];
    }

    public struct Point
    {

        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2 GetVector2()
        {
            return new Vector2(x, y);
        }
    }
}
