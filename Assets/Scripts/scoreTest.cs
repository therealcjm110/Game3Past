using UnityEngine;
using System.Collections;
using HighScore;

public class scoreTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HS.Init(this, "Team 10 - Echoes of the Past");

        StartCoroutine(MethodTest());
    }

    IEnumerator MethodTest() {
        yield return new WaitForSeconds(3);

        HS.SubmitHighScore(this, "bobby", 67);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
