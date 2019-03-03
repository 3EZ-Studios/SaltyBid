using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopScene : MonoBehaviour
{
    public string fightSceneName;
    public Canvas canvas;
    
    private FightState fightState;
    private ShopInventory p1Inventory;
    private ShopInventory p2Inventory;
    private bool p1Picked;
    private bool p2Picked;

    // Start is called before the first frame update
    void Start()
    {
        fightState = GameObject.FindWithTag("FightState").GetComponent<FightState>();
        fightState.SetShopComponent(GetComponent<ShopScene>());
        
        p1Inventory = new ShopInventory(fightState.GetRoundsToWin());
        p2Inventory = new ShopInventory(fightState.GetRoundsToWin());
        p1Picked = false;
        p2Picked = false;

        foreach (Transform tf in canvas.transform)
        {
            tf.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("P1Select0"))
        {
            P1PickTrinket(0);
        }
        else if (Input.GetButtonDown("P1Select1"))
        {
            P1PickTrinket(1);
        }
        else if (Input.GetButtonDown("P1Select2"))
        {
            P1PickTrinket(2);
        }
        else if (Input.GetButtonDown("P1Select3"))
        {
            P1PickTrinket(3);
        }
        else if (Input.GetButtonDown("P1SelectNothing"))
        {
            p1Picked = true;
            CheckShopComplete();
        }

        if (Input.GetButtonDown("P2Select0"))
        {
            P2PickTrinket(0);
        }
        else if (Input.GetButtonDown("P2Select1"))
        {
            P2PickTrinket(1);
        }
        else if (Input.GetButtonDown("P2Select2"))
        {
            P2PickTrinket(2);
        }
        else if (Input.GetButtonDown("P2Select3"))
        {
            P2PickTrinket(3);
        }
        else if (Input.GetButtonDown("P2SelectNothing"))
        {
            p2Picked = true;
            CheckShopComplete();
        }
    }

    private void P1PickTrinket(int index)
    {
        Trinket picked = p1Inventory.Buy(fightState.p1Score, index);
        // attach trinket to player, then...
        p1Picked = picked != null;
        CheckShopComplete();
    }

    private void P2PickTrinket(int index)
    {
        Trinket picked = p2Inventory.Buy(fightState.p2Score, index);
        // attach trinket to player, then...
        p2Picked = picked != null;
        CheckShopComplete();
    }

    public void Show(int p1Score, int p2Score)
    {
        ShowShelves(1, p1Score, p1Inventory);
        ShowShelves(2, p2Score, p2Inventory);

        foreach (Transform tf in canvas.transform)
        {
            tf.gameObject.SetActive(true);
        }
        gameObject.SetActive(true);
    }

    private void ShowShelves(int player, int score, ShopInventory inventory)
    {
        var bottomShelfIdx = score;
        if (IsTopShelf(score))
        {
            bottomShelfIdx = score - 1;
        }

        var trinkets = inventory.Get(bottomShelfIdx, 2);

        var currentShelf = canvas.transform.Find($"P{player}ShopCurrent");
        FillShelf(currentShelf, trinkets[0], !IsTopShelf(score));
        
        var nextShelf = canvas.transform.Find($"P{player}ShopNext");
        FillShelf(nextShelf, trinkets[1], IsTopShelf(score));
    }

    private void FillShelf(Transform shelf, List<Trinket> trinkets, bool active)
    {
        var idx = 0;
        foreach (Trinket trinket in trinkets)
        {
            var itemSlot = shelf.Find($"Item{idx}").gameObject;

            itemSlot.GetComponent<SpriteRenderer>().sprite = trinket?.Sprite;

            idx += 1;
        }
    }

    private bool IsTopShelf(int score)
    {
        return score == fightState.GetRoundsToWin() - 1;
    }

    private void CheckShopComplete()
    {
        if (p1Picked && p2Picked)
        {
            OnShopComplete();
        }
    }

    void OnShopComplete()
    {
        p1Picked = false;
        p2Picked = false;

        foreach (Transform tf in canvas.transform)
        {
            tf.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
        fightState.OnShopEnd();
    }
}
