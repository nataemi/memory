using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;

    public List<Button> btns = new List<Button>();

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public Text amountOfPoints;
    public Text addPoints;
    public Text subtractPoints;
    public Text endText;

    private bool firstGuess, secoundGuess;

    private int countCorrectGuesses;
    private int gameGuess;

    private int pointsCount;

    private int firstGuessIndex, secoundGuessIndex;

    private string firstGuessPuzzle, secoundGuessPuzzle;

    private void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Sprites/round");
    }

    // https://www.cs.umd.edu/class/spring2018/cmsc425/Lects/lect03-unity.pdf awake vs start; start wywoluje sie po awake
    void Start()
    {
        GetButtons();
        AddListener();
        AddGamePuzzles();
        Shuffle(gamePuzzles);

        pointsCount = 0;
        gameGuess = btns.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] buttonsObjects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < buttonsObjects.Length; i++)
        {
            btns.Add(buttonsObjects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }

            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }



    void AddListener()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }

    public void PickPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuess)
        {

            firstGuess = true;

            firstGuessIndex = int.Parse(name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].interactable = false;

        }
        else if (!secoundGuess)
        {

            secoundGuess = true;

            secoundGuessIndex = int.Parse(name);

            secoundGuessPuzzle = gamePuzzles[secoundGuessIndex].name;

            btns[secoundGuessIndex].image.sprite = gamePuzzles[secoundGuessIndex];

            StartCoroutine(CheckIfPuzzlesMatch());

            btns[firstGuessIndex].interactable = true;

        }
    }

    IEnumerator CheckIfPuzzlesMatch()
    {

        yield return new WaitForSeconds(1f);

        if (firstGuessPuzzle.Equals(secoundGuessPuzzle) && firstGuessIndex != secoundGuessIndex)
        {

            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secoundGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secoundGuessIndex].image.color = new Color(0, 0, 0, 0);

            pointsCount += 10;
            ShowAddPointsText();

            checkIfGameIsWon();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secoundGuessIndex].image.sprite = bgImage;

            pointsCount -= 2;
            ShowSubtractPointsText();

            checkIfGameIsLost();
        }

        amountOfPoints.text = pointsCount.ToString();

        firstGuess = secoundGuess = false;

    }

    void checkIfGameIsWon()
    {

        countCorrectGuesses++;

        if (countCorrectGuesses == gameGuess)
        {

            showEndPanel("GRATULACJE! \n Udało Ci się uzyskać " + pointsCount + " punktów.");

            AddButtons.sizeSetted = false;

            Invoke("restartGame", 2f);

        }

   
    }

    void checkIfGameIsLost(){

        if (pointsCount <= -10)
        {
            showEndPanel("NIE TYM RAZEM! \n Spróbuj jeszcze raz.  \n Może tym razem nie strzelaj?");

            AddButtons.sizeSetted = false;

            Invoke("restartGame", 2f);
        }
    }

    void restartGame(){
        MySceneManager.RestartGame();
    }

    void Shuffle(List<Sprite> list)
    {

        for (int i = 0; i < list.Count; i++)
        {

            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void ShowAddPointsText()
    {
        addPoints.gameObject.SetActive(true);
        Invoke("DisableAddPointsText", 2f);//invoke after 5 seconds
    }
    void DisableAddPointsText()
    {
        addPoints.gameObject.SetActive(false);
    }

    void ShowSubtractPointsText()
    {
        subtractPoints.gameObject.SetActive(true);
        Invoke("DisableSubtractPointsText", 2f);//invoke after 5 seconds
    }

    void DisableSubtractPointsText()
    {
        subtractPoints.gameObject.SetActive(false);
    }

    void showEndPanel(string endTextString){

        GameObject[] buttonsObjects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < buttonsObjects.Length; i++)
        {
            btns.Add(buttonsObjects[i].GetComponent<Button>());
            btns[i].image.color = new Color(0, 0, 0, 0);
        }

        endText.gameObject.SetActive(true);
        endText.text = endTextString;

    }
}
