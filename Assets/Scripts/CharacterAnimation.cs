using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {
    public int order;
	// Use this for initialization
	void Start () {
        StartCoroutine(MoveToTheLeft());
        StartCoroutine(JumpUpAndDown());

	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > 560) transform.position -= new Vector3(590, 0, 0);
	}

    IEnumerator MoveToTheLeft()
    {
        while (true)
        {
            transform.position += new Vector3(0.5f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator JumpUpAndDown()
    {
        yield return new WaitForSeconds(order*0.2f);
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 20; i > 0; i--)
            {
                transform.position += new Vector3(0, 2.0f*i/20, 0);
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 1; i <= 20; i++)
            {
                transform.position += new Vector3(0, -2.0f*i / 20, 0);
                yield return new WaitForSeconds(0.01f);
            }
            GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            yield return new WaitForSeconds(0.4f);
        }
    }
}
