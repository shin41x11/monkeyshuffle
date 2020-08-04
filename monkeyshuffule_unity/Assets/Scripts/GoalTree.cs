using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ゴールの木
public class GoalTree : MonoBehaviour
{

    private int goalMonkey; //

    private Text goalMonkeyText;

    // Start is called before the first frame update
    void Start()
    {
        goalMonkey = 0;
        goalMonkeyText = GameObject.Find("Player1GoalCounter").GetComponent<Text>();
        goalMonkeyText.text = "×" + goalMonkey;
    }

    // Update is called once per frame
    void Update()
    { 
    }

    /// <summary>
    /// 待機状態の猿をゲームに出現させる
    /// </summary>
    public void goal()
    {
        goalMonkey++;
        goalMonkeyText.text = "×" + goalMonkey;

    }


}
