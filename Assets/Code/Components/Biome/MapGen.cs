using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//This is an adapatation to C# from 
//https://github.com/p-rivero/SimpleMapGenerator


public class MapGen
{
    private class Cell
    {
        public int row;
        public int column;
        public bool visited;

        public Cell(int i_row, int i_column)
        {
            row = i_row;
            column = i_column;
            visited = false;
        }
    }

    private Vector2Int m_current;
    private int width;
    private int height;
    private int boardHeight;
    private int boardWidth;
    IList<IList<bool>> board;
    IList<IList<Cell>> cells;

    // Generate a maze with the desired height and width
    public MapGen(int i_h, int i_w)
    {
        Debug.Assert(i_h >= 5);
        Debug.Assert(i_w >= 5);
        Debug.Assert(i_h % 2 == 1);
        Debug.Assert(i_w % 2 == 1);


        height = i_h / 2;
        width = i_w / 2;

        boardHeight = i_h;
        boardWidth = i_w;
        cells = new List<IList<Cell>>();
        for (int r = 0; r < height; r++)
        {
            List<Cell> row = new List<Cell>();
            for (int c = 0; c < width; c++)
            {
                row.Add(new Cell(r, c));
            }
            cells.Add(row);
        }

        board = new List<IList<bool>>();
        for (int r = 0; r < boardHeight; r++)
        {
            List<bool> boardRow = new List<bool>();
            for (int c = 0; c < boardWidth; c++)
            {
                //!(r & 1) || !(c & 1))
                bool b = (r % 2 == 0) || (c % 2 == 0);
                boardRow.Add(b);
            }
            board.Add(boardRow);
        }


        m_current = new Vector2Int();
        m_current.x = UnityEngine.Random.Range(0, height);
        m_current.y = UnityEngine.Random.Range(0, width);

        generateMap();

    }

    private int boardCoord(int i_n)
    {
        return 2 * i_n + 1;
    }

    private void generateMap()
    {
        Stack<Vector2Int> backtrace = new Stack<Vector2Int>();

        do
        {
            cells[m_current.x][m_current.y].visited = true; // Mark current cell as visited
            Vector2Int? next = findNextCell();    // Find a random adjacent cell to visit next

            if (next != null)
            {
                backtrace.Push(m_current);     // Store current cell to return later
                removeWall(m_current, next.Value); // Remove wall between current and next cell
                m_current = next.Value;
            }
            else
            {
                // No more available cells to visit: backtrack
                m_current = backtrace.Peek();
                backtrace.Pop();
            }
        }
        while (backtrace.Count > 0);

    }

    bool IsValid(Vector2Int i_coordinate)
    {
        return !(i_coordinate.x < 0 
            || i_coordinate.y < 0 
            || i_coordinate.x > width - 1 
            || i_coordinate.y > height - 1 
            || cells[i_coordinate.x][i_coordinate.y].visited);
    }

    List<Vector2Int> getAvailableNeighbors()
    {
        List<Vector2Int> neightbors = new List<Vector2Int>();
        if(IsValid(m_current + Vector2Int.right))
        {
            neightbors.Add(m_current + Vector2Int.right);
        }
        if (IsValid(m_current + Vector2Int.up))
        {
            neightbors.Add(m_current + Vector2Int.up);
        }
        if (IsValid(m_current - Vector2Int.right))
        {
            neightbors.Add(m_current - Vector2Int.right);
        }
        if (IsValid(m_current - Vector2Int.up))
        {
            neightbors.Add(m_current - Vector2Int.up);
        }
        return neightbors;
    }

    private Vector2Int? findNextCell()
    {
        List<Vector2Int> available = getAvailableNeighbors();
        if(available.Count > 0)
        {
            return available[UnityEngine.Random.Range(0, available.Count)];
        }
        return null;
    }

    private void removeWall(Vector2Int i_a, Vector2Int i_b)
    {
        int row = boardCoord(i_a.x);
        int col = boardCoord(i_a.y);
        int deltaRow = i_b.x - i_a.x;
        int deltaCol = i_b.y - i_a.y;

        // Remove wall
        board[row + deltaRow][col + deltaCol] = false;
    }

    // Remove some walls from the maze
    public void addExtraPaths(int probExtraPath, bool removeIsolated)
    {
        Debug.Assert(probExtraPath >= 0 && probExtraPath <= 100);

        for (int r = 1; r < boardHeight - 1; r++)
        {
            for (int c = 1; c < boardWidth - 1; c++)
            {
                bool yPath = !board[r][c - 1] && !board[r][c + 1];
                bool xPath = !board[r - 1][c] && !board[r + 1][c];
                bool yWall = board[r][c - 1] && board[r][c+1];
                bool xWall = board[r - 1][c] && board[r + 1][c];

                // If there's a path in one axis and walls in the other axis,
                // this wall can be removed
                if (xPath && yWall && UnityEngine.Random.Range(0, 100) < probExtraPath) board[r][c] = false;
                if (yPath && xWall && UnityEngine.Random.Range(0, 100) < probExtraPath) board[r][c] = false;
            }
        }

        if (removeIsolated)
        {
            // Remove all remaining 1x1 walls (all 4 directions don't have walls)
            for (int r = 1; r < boardHeight - 1; r++)
            {
                for (int c = 1; c < boardWidth - 1; c++)
                {
                    bool yPath = !board[r][c - 1] && !board[r][c + 1];
                    bool xPath = !board[r - 1][c] && !board[r + 1][c];

                    if (xPath && yPath)
                    {
                        board[r][c] = false;
                    }
                }
            }
        }
    }
    
    // Get the resulting boolean matrix
    public IList<IList<bool>> getMap()
    {
        return board;
    }
}
