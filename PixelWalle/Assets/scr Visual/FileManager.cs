using UnityEngine;
using TMPro;
using AnotherFileBrowser.Windows;
using System.IO;
using System;

public class FileManager : MonoBehaviour
{
    public TMP_InputField codeInputField;

    private string GetValidInitialDirectory()
    {
       
        string path = Application.persistentDataPath.Replace('/', '\\');

        
        if (!Directory.Exists(path))
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        return path;
    }

    public void SaveCode()
    {
        var bp = new BrowserProperties()
        {
            title = "Guardar Código",
            initialDir = GetValidInitialDirectory(),
            filter = "Archivos PixelWalle (*.gw)|*.txt|Todos los archivos (*.*)|*.*",
            filterIndex = 0,
            restoreDirectory = true
        };

        try
        {
            new FileBrowser().SaveFileBrowser(bp, "codigo.gw", path =>
            {
                if (!string.IsNullOrEmpty(path))
                {
                    try
                    {
                        if (!path.EndsWith(".gw")) path += ".gw";
                        File.WriteAllText(path, codeInputField.text);
                        Debug.Log($"Código guardado en: {path}");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error al guardar archivo: {e.Message}");
                    }
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al abrir diálogo de guardado: {e.Message}");
        }
    }

    public void LoadCode()
    {
        var bp = new BrowserProperties()
        {
            title = "Cargar Código",
            initialDir = GetValidInitialDirectory(),
            filter = "Archivos PixelWalle (*.gw)|*.gw|Todos los archivos (*.*)|*.*",
            filterIndex = 0,
            restoreDirectory = true
        };

        try
        {
            new FileBrowser().OpenFileBrowser(bp, path =>
            {
                if (!string.IsNullOrEmpty(path))
                {
                    try
                    {
                        codeInputField.text = File.ReadAllText(path);
                        Debug.Log($"Código cargado desde: {path}");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error al cargar archivo: {e.Message}");
                    }
                }
            });
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al abrir diálogo de carga: {e.Message}");
        }
    }
}

