using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SpawnerManager spawner;
    BoardManager board;
    private ShapeManager activeShape;

    [Header("Counters")]
    [Range(0.02f, 1f)]
    [SerializeField] private float DownTime = .5f;
    private float DownCounter;
    private float DownLevelCounter; //aşağı inme level sayacı 

    [Range(0.02f, 1f)]
    [SerializeField] float LeftRightClickTime = .25f;
    float LeftRightClickCounter;

    [Range(0.02f, 1f)]
    [SerializeField] float LeftRightRotationTime = .25f;
    float LeftRightRotationCounter;

    [Range(0.02f, 1f)]
    [SerializeField] float DownClickTime = .25f;
    float DownClickCounter;

    public static bool gameOver = false;

    [Header("Rotation")]
    public bool isClockwise = true;
    public IconOnOffManager rotateIcon;


    public GameObject gameOverPanel;

    private ScoreManager scoreManager;

    private void Start()
    {
        board = GameObject.FindObjectOfType<BoardManager>();
        spawner = GameObject.FindObjectOfType<SpawnerManager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        if (spawner && activeShape == null)
        {
            activeShape = spawner.CreateFunctionFNC();
            if (activeShape != null)
                activeShape.transform.position = (Vector3)MakeTheVectorIntFNC((Vector2)activeShape.transform.position);
        }

        if (gameOverPanel)
        {
            gameOverPanel.SetActive(false);
        }


        DownLevelCounter = DownTime;
    }

    private void Update()
    {
        if (!board || !spawner || !activeShape || gameOver || !scoreManager) 
        
        return;
        InputControlFNC();
    }

    void InputControlFNC()
    {
        // RIGHT
        if ((Input.GetKey(KeyCode.RightArrow) && Time.time > LeftRightClickCounter) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            activeShape.RightMoveFNC();
            LeftRightClickCounter = Time.time + LeftRightClickTime;

            if (!board.IsPositionAcceptable(activeShape))
            {
                SoundManager.instance.OutVoiceEffect(1);
                activeShape.LeftMoveFNC();
            }
            else
            {
                SoundManager.instance.OutVoiceEffect(3);
            }
        }
        // LEFT
        else if ((Input.GetKey(KeyCode.LeftArrow) && Time.time > LeftRightClickCounter) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            activeShape.LeftMoveFNC();
            LeftRightClickCounter = Time.time + LeftRightClickTime;

            if (!board.IsPositionAcceptable(activeShape))
            {
                SoundManager.instance.OutVoiceEffect(1);
                activeShape.RightMoveFNC();
            }
            else
            {
                SoundManager.instance.OutVoiceEffect(3);
            }
        }
        // ROTATE (UP)
        else if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time > LeftRightRotationCounter)
        {
            // seçili yöne göre döndür
            activeShape.IsTurnClockwise(isClockwise);
            LeftRightRotationCounter = Time.time + LeftRightRotationTime;

            if (!board.IsPositionAcceptable(activeShape))
            {
                SoundManager.instance.OutVoiceEffect(1);
                // uymadıysa geri al
                activeShape.IsTurnClockwise(!isClockwise);
            }
            else
            {
                SoundManager.instance.OutVoiceEffect(3);
            }
        }
        // DOWN (basılı tutunca + otomatik düşme)
        else if (((Input.GetKey(KeyCode.DownArrow) && Time.time > DownClickCounter)) || Time.time > DownCounter)
        {
            DownCounter = Time.time + DownLevelCounter;
            DownClickCounter = Time.time + DownClickTime;

            if (activeShape)
            {
                activeShape.DownMoveFNC();

                if (!board.IsPositionAcceptable(activeShape))
                {
                    activeShape.UpMoveFNC();

                    if (board.IsItOutFNC(activeShape))
                    {
                        SoundManager.instance.OutVoiceEffect(6);
                        Debug.Log("GAME OVER");
                        activeShape = null;
                        gameOver = true;

                        if (gameOverPanel)
                        {
                            gameOverPanel.SetActive(true);

                            SoundManager.instance.OutVoiceEffect(6);

                        }

                        
                        return;
                    }




                    SettledInFNC();
                }
            }
        }
    }

    private void SettledInFNC()
    {
        LeftRightClickCounter = Time.time;
        DownClickCounter = Time.time;
        LeftRightRotationCounter = Time.time;

        board.ShapeIntoGrillFNC(activeShape);
        SoundManager.instance.OutVoiceEffect(5);

        board.CleanAllRowFNC();

        if (board.completedRow > 0)
        {

            scoreManager.RawScore(board.completedRow);


            if (scoreManager.IsLevelUp)
            {
                SoundManager.instance.OutVoiceEffect(2);
                DownLevelCounter = DownTime - Mathf.Clamp(((float)scoreManager.level - 1) * .1f,0.05f,1f);

            }
            else
            {
                if (board.completedRow > 1)
                {
                    SoundManager.instance.OutVocalVoice();
                }
                
            }



           
            SoundManager.instance.OutVoiceEffect(4);
        }

        if (spawner)
        {
            activeShape = spawner.CreateFunctionFNC();
            if (activeShape != null)
                activeShape.transform.position = (Vector3)MakeTheVectorIntFNC((Vector2)activeShape.transform.position);
        }
    }

    Vector2 MakeTheVectorIntFNC(Vector2 vector)
    {
        
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    // UI butonu: saat yönü / ters yön seç
    public void RotationIconSideFNC()
    {
        if (!activeShape || !board) return;

        // yönü değiştir
        isClockwise = !isClockwise;

        if (rotateIcon) rotateIcon.iconOnOffFNC(isClockwise);

        // test amaçlı döndür
        activeShape.IsTurnClockwise(isClockwise);

        // uymadıysa geri al + yönü geri çevir
        if (!board.IsPositionAcceptable(activeShape))
        {
            activeShape.IsTurnClockwise(!isClockwise);
            isClockwise = !isClockwise; // yönü eski haline getir
            if (rotateIcon) rotateIcon.iconOnOffFNC(isClockwise);
            SoundManager.instance.OutVoiceEffect(1);
        }
        else
        {
            SoundManager.instance.OutVoiceEffect(3);
        }
    }
}
