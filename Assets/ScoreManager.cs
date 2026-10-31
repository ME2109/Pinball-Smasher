using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public AudioClip comboSound;

    private AudioSource audioSource;

    private int score = 0;
    private int combo = 0;
    private float lastKillTime = 0f;

    public float comboTime = 5f;

    private readonly float[] melody =
    {
        1.00f,  
        1.26f,  
        1.50f,  
        1.26f,  
        1.68f, 
        1.50f,  
        2.00f   
    };

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;

        UpdateScoreText();
    }

    void Update()
    {
        if (combo > 0 && Time.time - lastKillTime > comboTime)
        {
            combo = 0;
            UpdateScoreText();
        }
    }

    public void AddScore()
    {
        if (combo > 0 && Time.time - lastKillTime <= comboTime)
        {
            combo++;
        }
        else
        {
            combo = 1;
        }

        lastKillTime = Time.time;
        score += combo;

        if (combo >= 2 && comboSound != null)
        {
            int noteIndex = (combo - 2) % melody.Length;

            audioSource.pitch = melody[noteIndex];
            audioSource.PlayOneShot(comboSound);
        }

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (combo > 1)
        {
            scoreText.text = "яв╗р: " + score + "  |  йнлан x" + combo;
        }
        else
        {
            scoreText.text = "яв╗р: " + score;
        }
    }
}