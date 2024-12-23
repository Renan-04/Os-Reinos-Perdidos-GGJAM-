using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    private Vector2 targetPosition;
    private bool isMoving = false;
    private List<Vector2> path = new List<Vector2>();

    void Update()
    {
        if (!isMoving)
        {
            HandleMouseInput();
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = mouseWorldPosition;
            path = FindPath(transform.position, targetPosition);
            if (path != null)
            {
                StartCoroutine(MoveToTarget());
            }
        }
    }

    IEnumerator MoveToTarget()
    {
        isMoving = true;
        foreach (Vector2 node in path)
        {
            while (Vector2.Distance(transform.position, node) > 0.05f)
            {
                transform.position = Vector2.MoveTowards(transform.position, node, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        isMoving = false;
    }

    List<Vector2> FindPath(Vector2 start, Vector2 end)
    {
        List<Vector2> openList = new List<Vector2>();
        List<Vector2> closedList = new List<Vector2>();
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
        Dictionary<Vector2, float> gScore = new Dictionary<Vector2, float>();
        Dictionary<Vector2, float> fScore = new Dictionary<Vector2, float>();

        openList.Add(start);
        gScore[start] = 0;
        fScore[start] = Heuristic(start, end);

        while (openList.Count > 0)
        {
            Vector2 current = GetNodeWithLowestFScore(openList, fScore);
            if (current == end)
            {
                return ReconstructPath(cameFrom, current);
            }
            openList.Remove(current);
            closedList.Add(current);

            foreach (Vector2 neighbor in GetNeighbors(current))
            {
                if (closedList.Contains(neighbor))
                {
                    continue;
                }
                float tentativeGScore = gScore[current] + 1;
                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
                else if (tentativeGScore >= gScore[neighbor])
                {
                    continue;
                }
                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, end);
            }
        }
        return null;
    }

    float Heuristic(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b);
    }

    List<Vector2> GetNeighbors(Vector2 node)
    {
        List<Vector2> neighbors = new List<Vector2>();
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (Vector2 dir in directions)
        {
            Vector2 neighbor = node + dir;
            neighbors.Add(neighbor);
        }
        return neighbors;
    }

    Vector2 GetNodeWithLowestFScore(List<Vector2> openList, Dictionary<Vector2, float> fScore)
    {
        Vector2 lowest = openList[0];
        foreach (Vector2 node in openList)
        {
            if (fScore[node] < fScore[lowest])
            {
                lowest = node;
            }
        }
        return lowest;
    }

    List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
    {
        List<Vector2> path = new List<Vector2>();
        while (cameFrom.ContainsKey(current))
        {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Reverse();
        return path;
    }
}