using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int currentStage = 1;  
    public int throwsLeft = 3;    
    public int totalPoints = 0;   
    public TextMeshProUGUI stageText;   
    public TextMeshProUGUI scoreText;   
    public GameObject nextLevelPanel;  
    public GameObject nextButton;      
    public GameObject exitButton;      
    public GameObject winMessage;     

    private Ball ballScript;

    void Start()
    {
        
        ballScript = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();

        
        throwsLeft = 3;
        totalPoints = 0;

         
        UpdateUI();

         
        if (nextLevelPanel != null) nextLevelPanel.SetActive(false);
        if (nextButton != null) nextButton.SetActive(false);
        if (winMessage != null) winMessage.SetActive(false);
    }

    void UpdateUI()
    {
         
        if (stageText != null)
            stageText.text = $"Stage: {currentStage}";
        if (scoreText != null)
            scoreText.text = $"Score: {totalPoints}";
    }

    public void CheckLevelProgress()
    {
        
        if (throwsLeft <= 0 || ballScript.CheckAllPinsFallen())
        {
            throwsLeft = 3;  

            if (currentStage < 5)  
            {
                ShowNextLevelPanel();
            }
            else
            {
                ShowWinMessage(); 
            }
        }
    }

    void ShowNextLevelPanel()
    {
         if (nextLevelPanel != null) nextLevelPanel.SetActive(true);
        if (nextButton != null) nextButton.SetActive(true);
        if (exitButton != null) exitButton.SetActive(true);
    }

    public void ShowWinMessage()
    {
         if (nextLevelPanel != null) nextLevelPanel.SetActive(true);
        if (winMessage != null) winMessage.SetActive(true);
        if (nextButton != null) nextButton.SetActive(false);  
        if (exitButton != null) exitButton.SetActive(true);   

        
        if (scoreText != null)
            scoreText.text = $"Your Final Score: {totalPoints}";
    }

    public void GoToNextStage()
    {
       
        if (currentStage < 5)
        {
            currentStage++;
            SceneManager.LoadScene($"Game_{currentStage}");
        }
    }

    public void ExitGame()
    {
     
        Application.Quit();
    }
}
