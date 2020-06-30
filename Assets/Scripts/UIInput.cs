using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class UIInput : MonoBehaviour
{
    [SerializeField] private GameObject winImg;
    private void Start()
    {
        Player.OnPlayerWin.AddListener(Win);
        winImg.SetActive(false);
    }

    public void OnReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PickUpCoin.countCoins = 0;
        winImg.SetActive(false);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }


    
    public void Win()
    {
        winImg.SetActive(true);
        StartCoroutine(RefreshScene());
        
    }

    private IEnumerator RefreshScene()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            OnReload();
            yield break;
        }
    }
}
