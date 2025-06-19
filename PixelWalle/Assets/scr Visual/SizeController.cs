using UnityEngine;
using TMPro;

public class SizeController : MonoBehaviour
{
    public TMP_InputField inputField;
    public int minValue = 1;
    public int maxValue = 1920;

    void Start()
    {
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        inputField.onEndEdit.AddListener(ValidateFinalInput);
    }

    void  ValidateFinalInput(string text)
    {

        if (int.TryParse(text, out int result))
        {
            
            result = Mathf.Clamp(result, minValue, maxValue);
            inputField.text = result.ToString();
        }
        else
        {
            inputField.text = minValue.ToString();
        }
    }
}
