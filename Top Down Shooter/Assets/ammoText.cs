using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoText : MonoBehaviour
{
    public static int ammo;
    Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        ammo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Ammo: " + ammo;
    }
}
