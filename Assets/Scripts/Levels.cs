using UnityEngine;
using System.Collections;

public class Levels : MonoBehaviour {

    int level = 1;
    public GameObject[] buttons;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        for (int i = level; i < buttons.Length; i++)
        {
            Destroy(buttons[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLevel(int x)
    {
        Application.LoadLevel("level"+x);
    }
}
