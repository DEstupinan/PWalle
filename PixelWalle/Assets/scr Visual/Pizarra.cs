using UnityEngine;
using UnityEngine.UI;

public class Pizarra : MonoBehaviour
{

    public int boardSize = 256;


    public RawImage display;


    private Texture2D drawingTexture;

    public Walle walle;
    public Color32[,] pixels;
    public int[,] walleBoard;

    void Start()
    {

    }


    public void InitializeBoard(int x)
    {
        if (x != null) boardSize = x;
        drawingTexture = new Texture2D(
            boardSize,
            boardSize,
            TextureFormat.RGBA32,
            false
        );


        drawingTexture.filterMode = FilterMode.Point;
        drawingTexture.wrapMode = TextureWrapMode.Clamp;


        pixels = new Color32[boardSize, boardSize];


        ClearBoard(Color.white);


        display.texture = drawingTexture;
        ApplyChanges();

    }


    public void ClearBoard(Color fillColor)
    {
        Color32 clearColor = fillColor;

        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                pixels[x, y] = clearColor;
            }
        }
        ApplyChanges();
    }


    public void SetPixel(int x, int y)
    {
        for (int i = x - walle.Size / 2; i <= x + walle.Size / 2; i++)
        {
            for (int j = y - walle.Size / 2; j <= y + walle.Size / 2; j++)
            {
                if (Check(i, j) && !walle.none) pixels[i, boardSize - 1 - j] = walle.BrushColor;
            }
        }



    }
    public bool Check(int x, int y)
    {
        if (x < 0 || x >= boardSize || y < 0 || y >= boardSize) return false;
        return true;
    }


    public void ApplyChanges()
    {
        Color32[] linearPixels = new Color32[boardSize * boardSize];

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                linearPixels[y * boardSize + x] = pixels[x, y];
            }
        }

        drawingTexture.SetPixels32(linearPixels);
        drawingTexture.Apply();
    }

    // Guarda la pizarra como imagen PNG
    /* public void SaveToFile(string filePath)
     {
         // Convertimos la textura a bytes en formato PNG
         byte[] pngData = drawingTexture.EncodeToPNG();

         // Escribimos los bytes en un archivo
         System.IO.File.WriteAllBytes(filePath, pngData);

         Debug.Log($"Imagen guardada en: {filePath}");
     }*/

    // Carga una imagen en la pizarra
    /* public void LoadFromFile(string filePath)
     {
         if (System.IO.File.Exists(filePath))
         {
             // Leemos todos los bytes del archivo
             byte[] fileData = System.IO.File.ReadAllBytes(filePath);

             // Cargamos la imagen en la textura
             drawingTexture.LoadImage(fileData);

             // Actualizamos el array de píxeles
             pixels = drawingTexture.GetPixels32();

             // Forzamos re-aplicación
             drawingTexture.Apply();
         }
         else
         {
             Debug.LogError("Archivo no encontrado: " + filePath);
         }
     }*/
}