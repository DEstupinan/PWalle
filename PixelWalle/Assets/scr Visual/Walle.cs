using UnityEngine;

public class Walle : MonoBehaviour
{
    public Color BrushColor;
    public bool none = true;
    public int Size;
    public Vector2Int position;
    void Start()
    {

    }
    public void Reset()
    {
        none=true;
        Size=1;
        position=new Vector2Int(0,0);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Move(int x, int y)
    {
        position.x += x;
        position.y += y;
    }
}
