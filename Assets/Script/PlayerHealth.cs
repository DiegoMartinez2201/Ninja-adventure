using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float invulnerabilityTime = 1f;
    public AudioClip gameOverClip;
    public string gameOverMessage = "PERDISTE";

    private int currentHealth;
    private float nextDamageTime;
    private bool isDead;

    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || Time.time < nextDamageTime)
        {
            return;
        }

        currentHealth -= damage;
        nextDamageTime = Time.time + invulnerabilityTime;

        Debug.Log("Vida del jugador: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jugador murio");
        isDead = true;
        ShowGameOverScreen();
        PlayGameOverAudio();
        gameObject.SetActive(false);
    }

    private void ShowGameOverScreen()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObject = new GameObject("Game Over Canvas");
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
        }

        GameObject panelObject = new GameObject("Game Over Panel");
        panelObject.transform.SetParent(canvas.transform, false);

        Image panel = panelObject.AddComponent<Image>();
        panel.color = new Color(0f, 0f, 0f, 0.75f);

        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        GameObject textObject = new GameObject("Game Over Text");
        textObject.transform.SetParent(panelObject.transform, false);

        Text text = textObject.AddComponent<Text>();
        text.text = gameOverMessage;
        text.alignment = TextAnchor.MiddleCenter;
        text.fontSize = 72;
        text.color = Color.white;
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        RectTransform textRect = text.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;
    }

    private void PlayGameOverAudio()
    {
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource source in audioSources)
        {
            source.Stop();
        }

        if (gameOverClip == null)
        {
            return;
        }

        GameObject audioObject = new GameObject("Game Over Audio");
        AudioSource gameOverAudio = audioObject.AddComponent<AudioSource>();
        gameOverAudio.playOnAwake = false;
        gameOverAudio.clip = gameOverClip;
        gameOverAudio.Play();
    }
}
