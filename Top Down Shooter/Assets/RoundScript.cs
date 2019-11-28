using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoundScript : MonoBehaviour
{
    public static int round;
    Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        round = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Round: " + round;
    }
}
