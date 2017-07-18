using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour {
    [SerializeField] TimeElapsed m_timeElapsed;
    //[SerializeField] GameObject m_splashScreen;
    [SerializeField] Text m_timeElapsedText;

    [SerializeField] ScoreTime m_scoreTime;
    float m_endingTime;

    [SerializeField] Orbit m_orbit;
    [SerializeField] RepositionAfterGameOver m_resetGamePiece;
    [SerializeField] DetectIfGamePieceLeavesScreenView m_detectGameOver;
    [SerializeField] GameObject m_gameOverDisplay;
    [SerializeField] Text m_timerObject;
    [SerializeField] GameObject m_newHighScore;
    [SerializeField] GameObject m_menuAndPausePanel;

    //private int floorMask = LayerMask.GetMask("Floor");

    bool m_gameOver = false;

    public static GameManager control;
    //void Awake() {
    //    if (control == null)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //        control = this;
    //    }
    //    else if (control != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    // Use this for initialization
    void Start () {
        //if (control == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //    control = this;
        //}
        //else if (control != this)
        //{
        //    Destroy(gameObject);
        //}
        PauseGame();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_gameOver)
            return;

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
            StartTheGame();
#else
        if (Input.touchCount == 2)
            StartTheGame();
#endif
    }

    void StartTheGame() {
        //m_splashScreen.gameObject.SetActive(false);

        m_timeElapsed.gameObject.SetActive(true);
        m_timeElapsed.Timer();
    }

    public void ResetGame() {
        StartTheGame();
        m_timeElapsedText.color = Color.white;
    }

    public void GoToMainMenu() {
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Time.timeScale = 1;
    }

    public void GameOver() {
        //Debug.Log("m_scoreTime = " + m_ScoreTime);
        //Debug.Log("m_ScoreTime.Keys = " + m_ScoreTime.Keys);
        //Debug.Log("m_ScoreTime.Values = " + m_ScoreTime.Values);
        //Debug.Log("m_ScoreTime[m_timeElapsed.text] = " + m_ScoreTime[m_timeElapsed.text]);

        PauseGame();
        m_endingTime = m_timeElapsed.Timer();
        //m_scoreTime.FindHigherScore("Best Time", m_scoreTime.ShowCurrentHighScore("Best Time"), m_runningTime);
        m_scoreTime.FindHigherScore("Best Time", m_endingTime);
        //Debug.Log(PlayerPrefs.GetFloat("Best Time"));

        //Debug.Log("m_scoreTime.IsNewScoreHigher(m_runningTime) = " + m_scoreTime.IsNewScoreHigher(m_endingTime));
        ////Debug.Log("m_scoreTime.IsNewScoreHigher() = " + m_scoreTime.IsNewScoreHigher());
        //Debug.Log("m_scoreTime.ShowCurrentHighScore('Best Time') = " + m_scoreTime.ShowCurrentHighScore("Best Time"));
        //Debug.Log("PlayerPrefs.GetFloat('Best Time') = " + PlayerPrefs.GetFloat("Best Time"));
        //Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

        //if (m_scoreTime.IsNewScoreHigher(m_runningTime)) {
        //if (m_scoreTime.IsNewScoreHigher()) {
        if (PlayerPrefs.GetFloat("Best Time") <= m_endingTime) {
            //Debug.Log("The running time of " + m_runningTime + " is bigger than ");
            //Debug.Log("PlayerPrefs.GetFloat('Best Time') = " + PlayerPrefs.GetFloat("Best Time"));
            //Debug.Log("m_scoreTime.IsNewScoreHigher(m_runningTime) = " + m_scoreTime.IsNewScoreHigher(m_runningTime));
            m_timeElapsedText.color = Color.green;

            /// Add some fancy text animations.
        } else {
            //Debug.Log("The running time has not beaten the record time of ");
            //Debug.Log("PlayerPrefs.GetFloat('Best Time') = " + PlayerPrefs.GetFloat("Best Time"));
            //Debug.Log("m_scoreTime.IsNewScoreHigher(m_runningTime) = " + m_scoreTime.IsNewScoreHigher(m_runningTime));
            m_timeElapsedText.color = Color.yellow;
        }

        m_orbit.enabled = false;
        m_resetGamePiece.ResetPosition();
        m_detectGameOver.enabled = false;
        //m_displayGameOverScreen.DisplayGameOverMessage();
        StartCoroutine(DisplayGameOverMessage());
    }

    public IEnumerator DisplayGameOverMessage() {
        Debug.Log("Hello");
        /// Add a paused time between these 2 events.
        yield return new WaitForSecondsRealtime(3);
        Debug.Log("Hello from the other side!!!");

        m_detectGameOver.enabled = false;
        /// Add some fancy text animations.
        m_timerObject.enabled = false;
        m_gameOverDisplay.SetActive(true);
        m_gameOverDisplay.GetComponentInChildren<Text>().text = m_timeElapsed.DisplayFormattedTime(m_endingTime);
        m_gameOverDisplay.GetComponentInChildren<Text>().color = Color.yellow;

        if (PlayerPrefs.GetFloat("Best Time") <= m_endingTime) {
            m_gameOverDisplay.GetComponentInChildren<Text>().color = Color.green;
            Debug.Log("Special message should appear... in 3... 2... 1:");
            yield return new WaitForSecondsRealtime(3);
            m_newHighScore.SetActive(true);

        }

        yield return new WaitForSecondsRealtime(5);
        m_detectGameOver.enabled = false;
        m_timeElapsed.enabled = false;
        //m_timeElapsed.GetComponent<TimeElapsed>().enabled = false;
        m_gameOverDisplay.SetActive(false);
        m_newHighScore.SetActive(false);
        m_timerObject.enabled = true;
        m_menuAndPausePanel.SetActive(true);
        //m_resetGamePiece.ReEnable();
    }





    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}