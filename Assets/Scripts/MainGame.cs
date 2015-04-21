using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {
    public GameObject colorOrb;
    public float timeInterval;
    public SpriteRenderer[] backgrounds;
    public int selectedBG;

    public bool playing = true;
	// Use this for initialization
	void Start () {
        selectedBG = 0;
        StartCoroutine(spawnColorOrbs());
        StartCoroutine(flashingSelected());
	}
	
	// Update is called once per frame
	void Update () {
        
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

    IEnumerator spawnColorOrbs()
    {
        while (playing)
        {
            GameObject co = Instantiate(colorOrb);
            co.GetComponent<ColorOrb>().mainGame = this;
            yield return new WaitForSeconds(timeInterval);
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
    }

    public void updateColors(int nColor)
    {
        backgrounds[selectedBG].color = ColorOrb.colors[nColor];
        selectedBG++;
        selectedBG %= backgrounds.Length;
    }
}
