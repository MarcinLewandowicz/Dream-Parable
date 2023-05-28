using UnityEngine;
using TMPro;

public class CheckNumber : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textForValues;
    [SerializeField] private GameObject textGameObject;
    [SerializeField] private TextMeshProUGUI buttonText;
    private bool isOpened;

    public void FillUpText()
    {
        if (!isOpened)
        {
            textGameObject.SetActive(true);
            textForValues.text = "";
            for (int i = 1; i < 101; i++)
            {
                textForValues.text += CheckValue(i);
                textForValues.text += "<br>";
            }
            isOpened = true;
            buttonText.text = "Close window";
        }
        else
        {
            textForValues.text = "";
            textGameObject.SetActive(false);
            isOpened = false;
            buttonText.text = "Open window";
        }
    }

    public string CheckValue(int value)
    {
        string result;

        bool isDivisibleByThree = value % 3 == 0;
        bool isDivisibleByFive = value % 5 == 0;
        bool isDivisibleByBoth = (isDivisibleByThree && isDivisibleByFive);

        if (isDivisibleByBoth)
        {
            result = value.ToString() + " MarkoPolo";
        }
        else if (isDivisibleByFive)
        {
            result = value.ToString() + " Polo";
        }
        else if (isDivisibleByThree)
        {
            result = value.ToString() + " Marko";
        }
        else
        {
            result = value.ToString();
        }


        return result;
    }
}
