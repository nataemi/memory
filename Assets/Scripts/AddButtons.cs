using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButtons : MonoBehaviour
{

    [SerializeField]
    private Transform puzzleField;

    GridLayoutGroup glg;

    [SerializeField]
    private GameObject btn;

    public static int rowNumber;

    public static int columnNumber;

    public static bool sizeSetted;

    private void Awake()
    {
        for (int i = 0; i < rowNumber * columnNumber; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);
        }

    }

    private void Start()
    {
        glg = puzzleField.gameObject.GetComponent<GridLayoutGroup>();
        glg.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        glg.constraintCount = columnNumber;
    }

    public static void SetSize(int rowNum, int colNum){
        rowNumber = rowNum;
        columnNumber =colNum;
        sizeSetted = true;
    }

}
