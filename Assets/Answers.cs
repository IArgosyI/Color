using UnityEngine;
using System.Collections;

public class Answers : MonoBehaviour {
    public static int[][] answers;
	// Use this for initialization
	void Start () {
        answers = new int[2][];
        answers[0] = new int[5];
        answers[1] = new int[5];
        answers[0][0] = 0;
        answers[0][1] = 1;
        answers[0][2] = 2;
        answers[0][3] = 3;
        answers[0][4] = 4;
        answers[1][0] = 5;
        answers[1][1] = 1;
        answers[1][2] = 6;
        answers[1][3] = 7;
        answers[1][4] = 8;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
