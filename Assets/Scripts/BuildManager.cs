using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private BuildController _activeBuilder;
    private LevelController _levelController;

    public static BuildManager Instance { get; set; }

    public GameObject Tower1;
    public GameObject Tower2;

    public void OpenBuildMenu(BuildController buildController)
    {
        _activeBuilder = buildController;
        gameObject.transform.position = _activeBuilder.gameObject.transform.position;
        gameObject.SetActive(true);
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
        _activeBuilder.Deactivate();
        _activeBuilder = null;
    }

    public void OnTower2ButtonClick()
    {
        if (!TryToPay(Tower2))
            return;

        BuildTower(Tower2);
    }

    private bool TryToPay(GameObject towerToBuild)
    {
        var price = towerToBuild.GetComponent<TowerController>().GoldPrice;
        if (_levelController.Gold >= price)
        {
            _levelController.UpdateGold(-price);
            return true;
        }

        return false;
    }

    private void BuildTower(GameObject towerToBuild)
    {
        Instantiate(towerToBuild, _activeBuilder.transform.position, Quaternion.identity);
        Destroy(_activeBuilder.gameObject);
        _activeBuilder = null;
        gameObject.SetActive(false);
    }

    public void OnTower1ButtonClick()
    {
        if (!TryToPay(Tower1))
            return;

        BuildTower(Tower1);
    }

    // Use this for initialization
    private void Start()
    {
        gameObject.SetActive(false);
        _levelController = LevelController.Instance;
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
