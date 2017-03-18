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
        if (_lost)
            EndText.text = "LOST";
        else if (_won)
        {
            EndText.text = "WON";
        }
            
    }

    public void UpdateLives(int amount)
    {
        _livesLeft += amount;
        UpdateText();

        if (_livesLeft < 1)
            _lost = true;
    }

    public void UpdateGold(int amount)
    {
        _gold += amount;
        UpdateText();
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _enemiesLeft = enemies.Length;
        UpdateText();

        if (_lost) return;

        bool spawnersFinished = true;
        foreach (var spawner in Spawners)
            spawnersFinished &= spawner.Finished;

        if (spawnersFinished && _enemiesLeft < 1)
        {
            _won = true;
            Menu.gameObject.SetActive(true);
        }
    }
}
