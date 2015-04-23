using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {
    public GameObject colorOrb;
    public float timeInterval;
    public SpriteRenderer[] backgrounds;
    public int selectedBG;
    public Color[] tmpColor;

    public bool playing = true;
	// Use this for initialization
	void Start () {
        selectedBG = 0;
        ColorOrb.colors = tmpColor;
        spawnInitialColorOrbs();
        
        StartCoroutine(flashingSelected());


	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void spawnInitialColorOrbs()
    {
        for (int i = 0; i < ColorOrb.colors.Length; i++)
        {
            spawnOneColorOrb(i);
        }
    }

    void spawnOneColorOrb(int n)
    {
        GameObject co = Instantiate(colorOrb);
        co.GetComponent<ColorOrb>().mainGame = this;
        co.GetComponent<ColorOrb>().setColor(n);
    }

    IEnumerator flashingSelected()
    {
        while (playing)
        {
            Color c = backgrounds[selectedBG].color;
            c.a = 0;
            backgrounds[selectedBG].color = c;
            yield return new WaitForSeconds(0.1f);
            c.a = 1;
            backgrounds[selectedBG].color = c;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void eatColorOrb(ColorOrb co)
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (i == selectedBG)
            {
                backgrounds[i].sortingOrder = -99;
                Debug.Log(i);
            }
            else
            {
                backgrounds[i].sortingOrder = -50;
            }
        }
        co.StartCoroutine(co.colorAnimation());
        spawnOneColorOrb(co.nColor);
    }

    public void updateColors(int nColor)
    {
        backgrounds[selectedBG].color = ColorOrb.colors[nColor];
        selectedBG++;
        selectedBG %= backgrounds.Length;
    }
}
