using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Levels : MonoBehaviour {

    int level = 1;
    public GameObject[] buttons;
    public Sprite[] levelUnlocked;
    public Sprite[] levelLocked;
	// Use this for initialization
	void Start () {
        buttons[0].GetComponent<Image>();
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            level = 1;
        }
        for (int i = 0; i < level; i++)
        {
            //Destroy(buttons[i]);
            buttons[i].GetComponent<Image>().sprite = levelUnlocked[i];
        }
        for (int i = level; i < buttons.Length; i++)
        {
            //Destroy(buttons[i]);
            buttons[i].GetComponent<Image>().sprite = levelLocked[i];
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLevel(int x)
    {
        Instantiate(Music.sfx);
        Debug.Log(x + " " + level);
        if(x<=level)Application.LoadLevel("level"+x);
    }
}
