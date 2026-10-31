using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public ScoreView scoreView;
    public AudioClip comboSound;
    public float comboTime = 5f;

    private ScoreModel model;
    private AudioSource audioSource;
    private float lastKillTime;

    private readonly float[] melody =
    {
        1.00f, 1.26f, 1.50f, 1.26f,
        1.68f, 1.50f, 2.00f
    };

    void Awake()
    {
        model = new ScoreModel();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    void Start()
    {
        if (scoreView != null)
        {
            scoreView.Bind(model);
        }
    }

    void Update()
    {
        if (model.Combo > 0 &&
            Time.time - lastKillTime > comboTime)
        {
            model.ResetCombo();
        }
    }

    public void AddScore()
    {
        int newCombo;

        if (model.Combo > 0 &&
            Time.time - lastKillTime <= comboTime)
        {
            newCombo = model.Combo + 1;
        }
        else
        {
            newCombo = 1;
        }

        lastKillTime = Time.time;

        model.AddPoints(newCombo);

        if (newCombo >= 2 && comboSound != null)
        {
            int noteIndex = (newCombo - 2) % melody.Length;

            audioSource.pitch = melody[noteIndex];
            audioSource.PlayOneShot(comboSound);
        }
    }
}