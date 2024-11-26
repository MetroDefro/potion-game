using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasView : MonoBehaviour
{
    public TextMeshProUGUI ScoreText => scoreText;

    public HorizontalLayoutGroup UsedLayoutGroup => usedLayoutGroup;
    public UsedItem UsedItemPrefab => usedItemPrefab;

    public IngredientButton[] IngredientsButtons => ingredientsButtons;
    public Ingredient Ingredient => ingredient;
    public RectTransform PotField => potField;

    public Potion[] Potions => potions;

    public Button RetryButton => retryButton;


    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private HorizontalLayoutGroup usedLayoutGroup;
    [SerializeField] private UsedItem usedItemPrefab;

    [SerializeField] private IngredientButton[] ingredientsButtons;
    [SerializeField] private Ingredient ingredient;
    [SerializeField] private RectTransform potField;

    [SerializeField] private Potion[] potions;

    [SerializeField] private RectTransform resultPanel;
    [SerializeField] private Image resultImage;
    [SerializeField] private TextMeshProUGUI potionNameText;
    [SerializeField] private TextMeshProUGUI potionDescriptionText;

    [SerializeField] private RectTransform endPanel;
    [SerializeField] private TextMeshProUGUI countAllText;
    [SerializeField] private TextMeshProUGUI countHiddenText;
    [SerializeField] private TextMeshProUGUI countFailText;
    [SerializeField] private TextMeshProUGUI resultScoreText;
    [SerializeField] private Button retryButton;

    [SerializeField] private Image xMoreImage;
    [SerializeField] private Image xWrongImage;

    public void Initialize()
    {
        ShowUsedLayoutGroup(false);
        ShowXWrongImage(false);
        ShowXMoreImage(false);
        ShowResultPanel(false, null, null, null);
        ShowEndPanel(false);
    }


    public void SetHerbNameAndDescription(string name, string description)
    {
        nameText.text = name;
        descriptionText.text = description;
    }

    public void SetTimerText(float span)
    {
        timerText.text = Mathf.FloorToInt(span).ToString();
    }

    public void ShowResultPanel(bool isShow, Sprite sprite = null, string name = null, string description = null)
    {
        resultPanel.gameObject.SetActive(isShow);
        resultImage.sprite = sprite;
        potionNameText.text = name;
        potionDescriptionText.text = description;
    }    
    
    public void ShowXWrongImage(bool isShow)
    {
        xWrongImage.gameObject.SetActive(isShow);
    }    
    
    public void ShowXMoreImage(bool isShow)
    {
        xMoreImage.gameObject.SetActive(isShow);
    }

    public void ShowUsedLayoutGroup(bool isShow)
    {
        usedLayoutGroup.gameObject.SetActive(isShow);
    }

    public void ShowEndPanel(bool isShow, int countAll = 0, int countHidden = 0, int countFail = 0, int score = 0)
    {
        endPanel.gameObject.SetActive(isShow);
        countAllText.text = countAll.ToString();
        countHiddenText.text = countHidden.ToString();
        countFailText.text = countFail.ToString();
        resultScoreText.text = score.ToString();
    }
}
