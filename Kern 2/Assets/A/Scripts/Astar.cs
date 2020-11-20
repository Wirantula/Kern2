using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Astar
{

    public List<Node> openList = new List<Node>();
    public List<Node> closedList = new List<Node>();
    public List<Node> adjacentSquares = new List<Node>();
    public List<Vector2Int> path = new List<Vector2Int>();
    public Vector2Int endGoal;
    public Node start = new Node();
    public int gscore = 0;
    public Node currentNode = new Node();
    /// <summary>
    /// TODO: Implement this function so that it returns a list of Vector2Int positions which describes a path
    /// Note that you will probably need to add some helper functions
    /// from the startPos to the endPos
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        start.position = startPos;
        endGoal = endPos;
        openList.Clear();
        closedList.Clear();
        path.Clear();
        openList.Add(start);
        while (openList.Count > 0)
        {
            var lowest = openList.Min(l => l.FScore);
            currentNode = openList.First(l => l.FScore == lowest);
            closedList.Add(currentNode);
            openList.Remove(currentNode);
            if (closedList.FirstOrDefault(l => l.position.x == endPos.x && l.position.y == endPos.y) != null) break;
            adjacentSquares.Clear();
            adjacentSquares = GetWalkableAdjecent(currentNode.position, grid);
            gscore++;
            
            foreach (var adjacentSquare in adjacentSquares)
            {
                if (closedList.FirstOrDefault(l => l.position.x == adjacentSquare.position.x && l.position.y == adjacentSquare.position.y) != null) continue;

                if(openList.FirstOrDefault(l => l.position.x == adjacentSquare.position.x && l.position.y == adjacentSquare.position.y) == null)
                {
                    adjacentSquare.GScore = gscore;
                    adjacentSquare.parent = currentNode;

                    openList.Insert(0, adjacentSquare);
                }
                else if(gscore + adjacentSquare.HScore < adjacentSquare.FScore)
                {
                    adjacentSquare.GScore = gscore;
                    adjacentSquare.parent = currentNode;
                }
            }

        }
        if (currentNode.position == endPos)
        {
            while (currentNode.parent != null)
            {
                path.Add(currentNode.position);
                currentNode = currentNode.parent;
            }
        }
        path.Reverse();
        return path;
    }

    public int CalcHScore(Vector2Int startPos, Vector2Int endPos)
    {
        return (Mathf.Abs(endPos.x - startPos.x) + Mathf.Abs(endPos.y - startPos.y));
    }

    public List<Node> GetWalkableAdjecent(Vector2Int pos, Cell[,] grid)
    {
        List<Node> possibleLocations = new List<Node>();
        possibleLocations.Clear();
        if (!grid[pos.x, pos.y].HasWall(Wall.DOWN))
        {
            Vector2Int nextPos = new Vector2Int(pos.x, pos.y - 1);
            possibleLocations.Add( new Node(nextPos, null, 0, CalcHScore(nextPos, endGoal)));
        }
        if (!grid[pos.x, pos.y].HasWall(Wall.UP))
        {
            Vector2Int nextPos = new Vector2Int(pos.x, pos.y + 1);
            possibleLocations.Add( new Node(nextPos, null, 0, CalcHScore(nextPos, endGoal)));
        }
        if (!grid[pos.x, pos.y].HasWall(Wall.LEFT))
        {
            Vector2Int nextPos = new Vector2Int(pos.x - 1, pos.y);
            possibleLocations.Add( new Node(nextPos, null, 0, CalcHScore(nextPos, endGoal)));
        }
        if (!grid[pos.x, pos.y].HasWall(Wall.RIGHT))
        {
            Vector2Int nextPos = new Vector2Int(pos.x + 1, pos.y);
            possibleLocations.Add( new Node(nextPos, null, 0, CalcHScore(nextPos, endGoal)));
        }
        return possibleLocations;
    }

    /// <summary>
    /// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
    /// </summary>
    public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node

        public float FScore { //GScore + HScore
            get { return GScore + HScore; }
        }
        public float GScore; //Current Travelled Distance
        public float HScore; //Distance estimated based on Heuristic

        public Node() { }
        public Node(Vector2Int position, Node parent, int GScore, int HScore)
        {
            this.position = position;
            this.parent = parent;
            this.GScore = GScore;
            this.HScore = HScore;
        }
    }
}
