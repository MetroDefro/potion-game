using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.SceneManagement;
using System.IO;

public class MainCanvasPresenter : MonoBehaviour
{
    private int MaxTime = 250;

    private MainCanvasView view;

    private List<Herb> inPotHerbs = new List<Herb>();
    private List<UsedItem> usedItems = new List<UsedItem>();
    private bool[] isVisitedNormal = new bool[5];
    private bool[] isVisitedHidden = new bool[5];

    private int countNormal = 0;
    private int countHidden = 0;
    private int countFail = 0;
    private int score = 0;

    private float spanTime;

    private void Awake()
    {
        view = GetComponent<MainCanvasView>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if(Mathf.FloorToInt(MaxTime - spanTime) <= 0)
        {
            view.ShowXWrongImage(false);
            view.ShowXMoreImage(false);
            view.ShowResultPanel(false, null, null, null);
            view.ShowEndPanel(true, countHidden + countNormal, countHidden, countFail, score);
            Time.timeScale = 0;
        }

        view.SetTimerText(MaxTime - (spanTime += Time.deltaTime));

        if (Input.GetMouseButtonDown(0)) 
        {
            view.ShowXWrongImage(false);
            view.ShowXMoreImage(false);
            view.ShowResultPanel(false, null, null, null);
        }

        view.Ingredient.transform.position = Input.mousePosition;
    }

    private void Initialize()
    {
        foreach (IngredientButton button in view.IngredientsButtons)
            button.Initialize((Herb herb) => OnClickIngredientButton(herb));

        view.ScoreText.text = score.ToString();

        Entry entry = new Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(_ => OnClickPot());
        EventTrigger eventTrigger = view.PotField.gameObject.AddComponent<EventTrigger>();
        eventTrigger.triggers.Add(entry);

        view.RetryButton.onClick.AddListener(() => SceneManager.LoadScene("StartScene"));

        view.Initialize();

        Time.timeScale = 1;
    }

    private void Retry()
    {
        countNormal = 0;
        countHidden = 0;
        countFail = 0;
        score = 0;
        spanTime = 0;
        view.SetTimerText(MaxTime);

        view.ScoreText.text = score.ToString();
        ClearPot();
        view.ShowEndPanel(false);

        view.Initialize();

        Time.timeScale = 1;
    }

    private void OnClickIngredientButton(Herb herb)
    {
        view.SetHerbNameAndDescription(herb.Name, herb.Description);

        view.IngredientsButtons[herb.Index].SetEnable(false);

        if(view.Ingredient.Herb)
            view.IngredientsButtons[view.Ingredient.Herb.Index].SetEnable(true);

        view.Ingredient.SetHerb(herb);
        view.Ingredient.transform.position = Input.mousePosition;
    }

    private void OnClickPot()
    {
        if (!view.Ingredient.gameObject.activeSelf)
        {
            if(inPotHerbs.Count > 0)
            {
                Grading();
                view.ScoreText.text = score.ToString();
                ClearPot();
            }
        }
        else
        {
            if(inPotHerbs.Count == 4)
            {
                view.ShowXMoreImage(true);
            }
            else
            {
                Herb herb = view.Ingredient.Herb;
                inPotHerbs.Add(herb);

                UsedItem item = Instantiate(view.UsedItemPrefab, view.UsedLayoutGroup.transform);
                usedItems.Add(item);
                item.Initialize(herb.sprite, () =>
                {
                    inPotHerbs.Remove(herb);
                    usedItems.Remove(item);
                    Destroy(item.gameObject);
                });

                AudioManager.Instance.PlayInsertBGM();

                view.ShowUsedLayoutGroup(true);
            }

            view.IngredientsButtons[view.Ingredient.Herb.Index].SetEnable(true);
            view.Ingredient.DeleteHerb();
        }

        view.SetHerbNameAndDescription(string.Empty, string.Empty);
    }

    private void ClearPot()
    {
        inPotHerbs.Clear();

        foreach (var item in usedItems)
        {
            Destroy(item.gameObject);
        }
        usedItems.Clear();

        view.ShowUsedLayoutGroup(false);
    }

    private void Succese(int potionIndex)
    {
        countNormal++;
        if (!isVisitedNormal[potionIndex])
        {
            score += 180;
        }
        else
        {
            score += 100;
        }
        view.ShowResultPanel(true, view.Potions[potionIndex].Sprite, view.Potions[potionIndex].Name
            , view.Potions[potionIndex].Description);

        isVisitedNormal[potionIndex] = true;

        AudioManager.Instance.PlayCompleteBGM();
    }

    private void HiddenSuccese(int potionIndex)
    {
        countHidden++;
        if (!isVisitedHidden[potionIndex])
        {
            score += 270;
        }
        else
        {
            score += 150;
        }
        view.ShowResultPanel(true, view.Potions[potionIndex].HiddenSprite, view.Potions[potionIndex].HiddenName
            , view.Potions[potionIndex].HiddenDescription);

        isVisitedHidden[potionIndex] = true;

        AudioManager.Instance.PlayCompleteBGM();
    }

    private void Fail()
    {
        countFail++;
        if(score - 5 >= 0)
            score -= 5;

        view.ShowXWrongImage(true);
    }

    private void Grading()
    {
        if (inPotHerbs.Count == 3 || inPotHerbs.Count == 4)
        {
            for(int potionIndex = 0;  potionIndex < view.Potions.Length; potionIndex++ )
            {
                if(inPotHerbs.Count == 3)
                {
                    int count = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (inPotHerbs.Find(o => o.Index == view.Potions[potionIndex].HerbIndexes[i]))
                            count++;
                    }

                    if (count == 3)
                    {
                        Succese(potionIndex);
                        return;
                    }
                }
                else if(inPotHerbs.Count == 4)
                {
                    int count = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (inPotHerbs.Find(o => o.Index == view.Potions[potionIndex].HiddenHerbIndexes[i]))
                            count++;
                    }

                    if (count == 4)
                    {
                        HiddenSuccese(potionIndex);
                        return;
                    }
                }
            }
        }

        Fail();
    }
}