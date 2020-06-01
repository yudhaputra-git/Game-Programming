using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private int score,highscore,level;
    public GameObject panelku;

	[SerializeField]
	private Transform tetrominoContainer;

    [SerializeField]
    private int scoreOneLine = 40;

    [SerializeField]
    private int scoreTwoLine = 100;

    [SerializeField]
    private int scoreThreeLine = 200;
   
    [SerializeField]
    private int scoreFourLine = 400;

    private int lengthDestroyRows = 0;

    public Text texthighscore, textscore, textlevel;
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    private void Start()
    {
        panelku.SetActive(false);
        highscore = PlayerPrefs.GetInt("Highscoreku");
        score = 0;
        PlayerPrefs.SetInt("Levelku",score);
        GenerateTetromino();
    }

    private void Update()
    {
        textscore.text = score.ToString();
        if(score > highscore){
            highscore = score;
        }
        level = score / 100;
        if(level > 1){
            PlayerPrefs.SetInt("Levelku",level);
        }
        PlayerPrefs.SetInt("Highscoreku",highscore);
        texthighscore.text = highscore.ToString();
        textlevel.text = level.ToString();
        if (Input.GetKeyUp(KeyCode.Space))
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void UpdateScore()
    {
        switch (lengthDestroyRows)
        {
            case 1:
                score += scoreOneLine;
                break;
            case 2:
                score += scoreTwoLine;
                break;
            case 3:
                score += scoreThreeLine;
                break;
            case 4:
                score += scoreFourLine;
                break;
        }

		lengthDestroyRows = 0;
    }

    private bool IsRowFullAt(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] == null)
                return false;
        }

        lengthDestroyRows++;

        return true;
    }

    private void DestroyRowAt(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);

            grid[x, y] = null;
        }
    }

    private void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += Vector3.down;

            }
        }
    }

    private void MoveAllRowsDown(int y)
    {
        for (int i = y; i < gridHeight; i++)
            MoveRowDown(i);
    }

    private string GetRandomTetromino()
    {
        int val = Random.Range(0, 7);
        string tetrominoName = "TetrominoT";

        switch (val)
        {
            case 0:
                tetrominoName = "TetrominoI";
                break;
            case 1:
                tetrominoName = "TetrominoJ";
                break;
            case 2:
                tetrominoName = "TetrominoL";
                break;
            case 3:
                tetrominoName = "TetrominoT";
                break;
            case 4:
                tetrominoName = "TetrominoO";
                break;
            case 5:
                tetrominoName = "TetrominoS";
                break;
            case 6:
                tetrominoName = "TetrominoZ";
                break;
        }

        return "Prefabs/" + tetrominoName;
    }

    public void DestroyRow()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            if (IsRowFullAt(y))
            {
                DestroyRowAt(y);

                MoveAllRowsDown(y + 1);

                y--;
            }
        }

		UpdateScore();
    }

    public bool IsReactLimitGrid(TetrominoHandler tetromino)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector3 pos = Round(mino.position);

                if (pos.y >= gridHeight - 1 &&
                    !tetromino.isActiveAndEnabled)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void GameOver(TetrominoHandler tetromino)
    {
        panelku.SetActive(true);
    }

    public Transform GetTransformAtGridPosition(Vector3 pos)
    {
        if (pos.y > gridHeight - 1)
            return null;
        else
            return grid[(int)pos.x, (int)pos.y];
    }

    public void UpdateGrid(TetrominoHandler tetromino)
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetromino.transform)
                        grid[x, y] = null;
                }
            }
        }

        foreach (Transform mino in tetromino.transform)
        {
            Vector3 pos = Round(mino.position);

            if (pos.y < gridHeight)
                grid[(int)pos.x, (int)pos.y] = mino;
        }
    }

    public void GenerateTetromino()
    {
        GameObject tetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)),
                                                       new Vector3(5.0f, 18.0f, 0.0f),
                                                       Quaternion.identity);
		tetromino.transform.SetParent(tetrominoContainer);    
    }

    public bool IsTetrominoInsideAGrid(Vector3 pos)
    {
        return (
            (int)pos.x >= 0 &&
            (int)pos.x < gridWidth &&
            (int)pos.y >= 0
        );
    }

    public Vector3 Round(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x),
            Mathf.Round(pos.y),
            Mathf.Round(pos.z)
        );
    }
}