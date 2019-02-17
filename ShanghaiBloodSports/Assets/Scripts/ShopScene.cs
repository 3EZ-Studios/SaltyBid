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

    // Start is called before the first frame update
    void Start()
    {
        fightState = GameObject.FindWithTag("FightState").GetComponent<FightState>();
        fightState.SetShopComponent(GetComponent<ShopScene>());
        
        p1Inventory = new ShopInventory(fightState.GetRoundsToWin());
        p2Inventory = new ShopInventory(fightState.GetRoundsToWin());

        foreach (Transform tf in canvas.transform)
        {
            tf.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // debug to step through scenes
        if (Input.GetButtonDown("Fire3"))
        {
            OnShopComplete();
        }
    }

    public void Show(int p1Score, int p2Score)
    {
        // construct shelf display using inventory.Get()

        foreach (Transform tf in canvas.transform)
        {
            tf.gameObject.SetActive(true);
        }
        gameObject.SetActive(true);
    }

    void OnShopComplete()
    {
        foreach (Transform tf in canvas.transform)
        {
            tf.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
        fightState.OnShopEnd();
    }
}
