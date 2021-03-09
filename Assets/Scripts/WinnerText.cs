using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerText : MonoBehaviour
{

    public GameObject txt;
    // Start is called before the first frame update
    void Start()
    {
      txt.GetComponent<UnityEngine.UI.Text>().text = gameState.winners;
    }

    // Update is called once per frame
    void Update()
    {
      txt.GetComponent<UnityEngine.UI.Text>().text = gameState.winners;

    }
}
