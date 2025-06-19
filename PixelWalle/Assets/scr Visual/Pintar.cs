using UnityEngine;
using System.Collections.Generic;

public class Pintar : MonoBehaviour
{
    public Walle walle;
    public Pizarra board;

    public void Spawn(int x, int y)
    {
        if (Check(x, y))
        {
            walle.position.x = x;
            walle.position.y = y;
        }
        else
        {
            walle.position.x = 0;
            walle.position.y = 0;
        }

    }
    public void ChangeColor(string c)
    {

        switch (c)
        {
            case "Red": walle.BrushColor = Color.red; walle.none = false; break;
            case "Blue": walle.BrushColor = Color.blue; walle.none = false; break;
            case "Green": walle.BrushColor = Color.green; walle.none = false; break;
            case "Yellow": walle.BrushColor = Color.yellow; walle.none = false; break;
            case "Orange":
                Color orange;
                ColorUtility.TryParseHtmlString("#FFA500", out orange);
                walle.BrushColor = orange;
                walle.none = false; break;
            case "Purple":
                Color purple;
                ColorUtility.TryParseHtmlString("#800080", out purple);
                walle.BrushColor = purple;
                walle.none = false; break;
            case "Black": walle.BrushColor = Color.black; walle.none = false; break;
            case "White": walle.BrushColor = Color.white; walle.none = false; break;
            case "Transparent": walle.none = true; break;
            default: break;
        }
    }
    public void DrawLine(int x, int y, int distances)
    {
        while (distances > 0)
        {

            board.SetPixel(walle.position.x, walle.position.y);
            if (Check(walle.position.x + x, board.boardSize - 1 - (y + walle.position.y)))
            {
                walle.position.x += x;
                walle.position.y += y;
            }

            distances--;
        }
        board.ApplyChanges();

    }
    public void Size(int x)
    {
        if (x % 2 == 0) x--;
        walle.Size = x;
    }
    public void DrawRectangle(int x, int y, int distance, int width, int height)
    {

        while (distance > 0)
        {
            if (Check(walle.position.x + x, board.boardSize - 1 - (y + walle.position.y)))
            {
                walle.position.x += x;
                walle.position.y += y;
            }

            distance--;
        }

        for (int i = walle.position.x - width; i <= walle.position.x + width; i++)
        {
            for (int j = walle.position.y - height; j <= walle.position.y + height; j++)
            {
                if (i == walle.position.x - width || i == walle.position.x + width
                || j == walle.position.y - height || j == walle.position.y + height)


                    if (Check(i, j)) board.SetPixel(i, j);

            }
        }

        board.ApplyChanges();
    }
    public void DrawCircle(int x, int y, int radius)
    {
        int distances = radius;
        while (distances > 0)
        {


            if (Check(walle.position.x + x, board.boardSize - 1 - (y + walle.position.y)))
            {
                walle.position.x += x;
                walle.position.y += y;
            }

            distances--;
        }
        int radiusSq = radius * radius;
        float margin = radius - 1;
        for (int i = walle.position.x - radius; i <= walle.position.x + radius; i++)
        {
            for (int j = walle.position.y - radius; j <= walle.position.y + radius; j++)
            {
                int dx = i - walle.position.x;
                int dy = j - walle.position.y;
                int distanceSq = dx * dx + dy * dy;


                if (Mathf.Abs(distanceSq - radiusSq) <= margin)
                {

                    if (Check(i, j)) board.SetPixel(i, j);
                }
            }
        }

        board.ApplyChanges();
    }
    public void Fill()
    {
        Color originalColor = board.pixels[walle.position.x, walle.position.y];
        if (originalColor == walle.BrushColor) return;
        if (walle.none) return;
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(walle.position);

        bool[,] visited = new bool[board.boardSize, board.boardSize];

        Vector2Int[] directions = {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0)
    };

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            int x = current.x;
            int y = current.y;

            if (x < 0 || x >= board.boardSize || y < 0 || y >= board.boardSize ||
                board.pixels[x, y] != originalColor || visited[x, y])
            {
                continue;
            }

            board.pixels[x, y] = walle.BrushColor;
            visited[x, y] = true;

            foreach (var dir in directions)
            {
                queue.Enqueue(new Vector2Int(x + dir.x, y + dir.y));
            }
        }
        board.ApplyChanges();
    }
    public int GetActualX()
    {
        return walle.position.x;
    }
    public int GetActualY()
    {
        return walle.position.y;
    }
    public int GetCanvasSize()
    {
        return board.boardSize;
    }
    public int IsBrushSize(int size)
    {
        if (walle.Size == size) return 1;
        return 0;
    }
    public int IsCanvasColor(string color, int horizontal, int vertical)
    {
        Color targetColor = Color.clear;
        switch (color)
        {
            case "Red": targetColor = Color.red; break;
            case "Blue": targetColor = Color.blue; break;
            case "Green": targetColor = Color.green; break;
            case "Yellow": targetColor = Color.yellow; break;
            case "Orange":
                Color orange;
                ColorUtility.TryParseHtmlString("#FFA500", out orange);
                targetColor = orange;
                break;
            case "Purple":
                Color purple;
                ColorUtility.TryParseHtmlString("#800080", out purple);
                targetColor = purple;
                break;
            case "Black": targetColor = Color.black; break;
            case "White": targetColor = Color.white; break;
            case "Transparent":
                return 0;
            default: break;
        }
        if (!Check(walle.position.x + horizontal, walle.position.y + vertical)) return 0;
        if (board.pixels[walle.position.x + horizontal, walle.position.y + vertical] == targetColor) return 1;
        return 0;
    }
    public int IsBrushColor(string color)
    {
        Color targetColor = Color.clear;
        switch (color)
        {
            case "Red": targetColor = Color.red; break;
            case "Blue": targetColor = Color.blue; break;
            case "Green": targetColor = Color.green; break;
            case "Yellow": targetColor = Color.yellow; break;
            case "Orange":
                Color orange;
                ColorUtility.TryParseHtmlString("#FFA500", out orange);
                targetColor = orange;
                break;
            case "Purple":
                Color purple;
                ColorUtility.TryParseHtmlString("#800080", out purple);
                targetColor = purple;
                break;
            case "Black": targetColor = Color.black; break;
            case "White": targetColor = Color.white; break;
            case "Transparent":
                if (walle.none) return 1;
                else return 0;
            default: break;
        }
        if (targetColor == walle.BrushColor) return 1;
        return 0;
    }
    public int GetColorCount(string color, int x1, int y1, int x2, int y2)
    {
        Color targetColor = Color.clear;
        switch (color)
        {
            case "Red": targetColor = Color.red; break;
            case "Blue": targetColor = Color.blue; break;
            case "Green": targetColor = Color.green; break;
            case "Yellow": targetColor = Color.yellow; break;
            case "Orange":
                Color orange;
                ColorUtility.TryParseHtmlString("#FFA500", out orange);
                targetColor = orange;
                break;
            case "Purple":
                Color purple;
                ColorUtility.TryParseHtmlString("#800080", out purple);
                targetColor = purple;
                break;
            case "Black": targetColor = Color.black; break;
            case "White": targetColor = Color.white; break;
            case "Transparent": return 0;
            default: break;
        }
        if (!Check(x1, y1) || !Check(x2, y2)) return 0;
        int minX = Mathf.Min(x1, x2);
        int maxX = Mathf.Max(x1, x2);
        int minY = Mathf.Min(y1, y2);
        int maxY = Mathf.Max(y1, y2);
        int count = 0;
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if (board.pixels[x, y] == targetColor)
                {
                    count++;
                }
            }
        }
        return count;
    }
    public bool Check(int x, int y)
    {
        if (x < 0 || x >= board.boardSize || y < 0 || y >= board.boardSize) return false;
        return true;
    }

}
