using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject winText;

    public float maxTime = 3f;

    private float currentTime = 0f;
    private bool finished = false;

    void Start()
    {
        winText.SetActive(false);
    }

    void Update()
    {
        if (!finished)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= maxTime)
            {
                FinishGame();
            }
            else
            {
                UpdateTimerText();
            }
        }
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(currentTime);
        timerText.text = seconds.ToString();
    }

    void FinishGame()
    {
        finished = true;
        timerText.text = maxTime.ToString();
        winText.SetActive(true);

        Time.timeScale = 0f; // <-- pausa todo
    }
}