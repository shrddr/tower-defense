using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public int StartingLives;
    public int StartingGold;
    public Text LivesText;
    public Text EnemiesText;
    public Text GoldText;
    public Text EndText;
    public Canvas Menu;
    public Spawner[] Spawners;
    public AudioSource Audio;
    public AudioClip WonClip;
    public AudioClip LostClip;

    private int _livesLeft;
    private int _enemiesLeft;
    private int _gold;
    private bool _won;
    private bool _lost;

    void Start()
	{
        _livesLeft = StartingLives;
	    _gold = StartingGold;
	    _enemiesLeft = 0;
        UpdateText();
        Menu.gameObject.SetActive(false);
    }

    void UpdateText()
    {
        LivesText.text = "Lives: " + _livesLeft;
        EnemiesText.text = "Enemies: " + _enemiesLeft;
        GoldText.text = "Gold: " + _gold;
    }

    public void UpdateLives(int amount)
    {
        _livesLeft += amount;
        UpdateText();

        if (_lost) return;
        if (_livesLeft <= 0) Lose();     
    }

    public void UpdateGold(int amount)
    {
        _gold += amount;
        UpdateText();
    }

    void Update()
    {
        if (_won || _lost) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _enemiesLeft = enemies.Length;
        UpdateText();

        bool spawnersFinished = true;
        foreach (var spawner in Spawners)
            spawnersFinished &= spawner.Finished;

        if (spawnersFinished && _enemiesLeft < 1) Win();
    }

    void Win()
    {
        _won = true;
        EndText.text = "WON";
        Audio.clip = WonClip;
        Audio.Play();
        Menu.gameObject.SetActive(true);
    }

    void Lose()
    {
        _lost = true;
        EndText.text = "LOST";
        Audio.clip = LostClip;
        Audio.Play();
        Menu.gameObject.SetActive(true);
    }
}
