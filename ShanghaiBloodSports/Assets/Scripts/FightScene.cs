using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightScene : MonoBehaviour
{
    public GameObject fightStateObject;
    private FightState fightState;
    private bool fightActive;

    // Start is called before the first frame update
    void Start()
    {
        fightState = GameObject.FindWithTag("FightState").GetComponent<FightState>();
        fightActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // debug to step through scenes
        if (Input.GetButtonDown("Fire3"))
        {
            gameObject.SetActive(false);
            OnP1RoundWin();
        }
        else if (Input.GetButtonDown("Fire4"))
        {
            gameObject.SetActive(false);
            OnP2RoundWin();
        }
    }

    public void Fight()
    {
        gameObject.SetActive(true);
        fightActive = true;
    }

    void OnP1RoundWin()
    {
        fightActive = false;
        fightState.IncrementP1Score();
    }

    void OnP2RoundWin()
    {
        fightActive = false;
        fightState.IncrementP2Score();
    }

    public bool IsFightActive()
    {
        return fightActive;
    }
}
