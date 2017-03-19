using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private BuildController _activeBuilder;

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
        Instantiate(Tower2, _activeBuilder.transform.position, Quaternion.identity);
        Destroy(_activeBuilder.gameObject);
        _activeBuilder = null;
        gameObject.SetActive(false);
    }

    public void OnTower1ButtonClick()
    {
        Instantiate(Tower1, _activeBuilder.transform.position, Quaternion.identity);
        Destroy(_activeBuilder.gameObject);
        _activeBuilder = null;
        gameObject.SetActive(false);
    }

    // Use this for initialization
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
