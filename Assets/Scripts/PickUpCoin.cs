using UnityEngine;
using UnityEngine.UI;

public class PickUpCoin : MonoBehaviour
{
    public static int countCoins;
    private Text coinText;
    void Start()
    {
        coinText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = $"x{countCoins}";
    }
}
