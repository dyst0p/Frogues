using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public MapBasedOnTilemap _map;
    public PathFinderNode[,] _nodesGrid;
    private List<Vector2Int> _dirVectors;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _linesParent;
    private List<PathFinderNode> _currentNodes;
    private List<PathFinderNode> _childNodes;

    private void Start()
    {
        InitializeDirVectors();
        InitializeNodesGrid();
        FindAllNodesNeighbors();
    }

    public void ResetNodes()
    {
        foreach (var item in _nodesGrid)
        {
            item._usedToPathFinding = false;
            item._weight = 0;
        }
    }

    public List<Cell> FindWay(Cell userCell, Cell targetCell)
    {
        ResetNodes();
        return AStar(userCell, targetCell);
    }

    private List<Cell> AStar(Cell userCell, Cell targetCell)
    {
        _currentNodes = new List<PathFinderNode>();
        _currentNodes.Add(_nodesGrid[userCell._coordinates.x, userCell._coordinates.y]);
        _nodesGrid[userCell._coordinates.x, userCell._coordinates.y]._usedToPathFinding = true;
        PathFinderNode smallestWeightNode;

        while (_currentNodes.Count != 0)
        {
            smallestWeightNode = _currentNodes[0];

            print(0);

            foreach (var item in _currentNodes)
            {
                if(item._weight < smallestWeightNode._weight)
                    smallestWeightNode = item;
            }

            foreach (var item in smallestWeightNode._neighbors)
            {
                print(1);

                if (item._coordinates == new Vector2Int(targetCell._coordinates.x, targetCell._coordinates.y))
                {

                    print(2);
                    item._previous = smallestWeightNode;
                    List<Cell> path = new List<Cell>();
                    var tempBackTrackNode = item;

                    while (tempBackTrackNode._coordinates != new Vector2Int(userCell._coordinates.x, userCell._coordinates.y))
                    {
                        print(3);
                        path.Insert(0, tempBackTrackNode._cell);
                        tempBackTrackNode = tempBackTrackNode._previous;
                    }

                    return path;
                }
                else if (!item._usedToPathFinding/* && !item.Busy && (map.GetSurfaceByVector(item.Pos) == null || ignoreTraps)*/)
                {
                    print(4);
                    _currentNodes.Add(item);
                    item._weight = Vector2Int.Distance(item._coordinates, userCell._coordinates) + Vector2Int.Distance(item._coordinates, targetCell._coordinates);
                    item._previous = smallestWeightNode;
                    item._usedToPathFinding = true;
                }
            }

            print(5);

            _currentNodes.Remove(smallestWeightNode);
            smallestWeightNode._usedToPathFinding = true;
        }

        print(6);

        return null;
    }

    #region initPathfinder

    private void InitializeDirVectors()
    {
        _dirVectors = new List<Vector2Int>();
        _dirVectors.Add(Vector2Int.up);
        _dirVectors.Add(Vector2Int.right);
        _dirVectors.Add(Vector2Int.down);
        _dirVectors.Add(Vector2Int.left);
    }

    private void InitializeNodesGrid()
    {
        _nodesGrid = new PathFinderNode[_map._sizeX, _map._sizeY];

        for (int i = 0; i < _map._sizeX; i++)
        {
            for (int j = 0; j < _map._sizeY; j++)
            {
                _nodesGrid[i,j] = new PathFinderNode(_map._surfacesLayer[i, j]);
                if(!_map._surfacesLayer[i, j].isEmpty)
                {
                    _nodesGrid[i, j]._busy = false;
                }
            }
        }

    }

    private void FindAllNodesNeighbors() //todo �������� ��� ������������ try catch
    {
        foreach (var node in _nodesGrid)
        {
            foreach (var dir in _dirVectors)
            {
                try
                {
                    node.AddNeighbor(_nodesGrid[node._cell._coordinates.x + dir.x, node._cell._coordinates.y + dir.y]);
                    var line = Instantiate(_lineRenderer);
                    line.SetPosition(0, node._cell.transform.position);
                    line.SetPosition(1, _nodesGrid[node._cell._coordinates.x + dir.x, node._cell._coordinates.y + dir.y]._cell.transform.position);
                    line.transform.parent = _linesParent;
                }
                catch
                {

                }
            }
        }
    }

    #endregion
}

public class PathFinderNode
{
    public Cell _cell;
    public Vector2Int _coordinates;
    public List<PathFinderNode> _neighbors;
    public bool _usedToPathFinding;
    public bool _busy;
    public PathFinderNode _previous;
    public float _weight;

    public PathFinderNode(Cell cell)
    {
        _cell = cell;
        _neighbors = new List<PathFinderNode>();
        _coordinates = _cell._coordinates;
    }

    public void AddNeighbor(PathFinderNode neighbor)
    {
        _neighbors.Add(neighbor);
    }
}
