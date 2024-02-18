using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button play, autors, exit, back;
    [SerializeField] private GameObject n1, n2, autor, board;
    [SerializeField] private TMP_Text text1, text2;
    [SerializeField] private Image reward1, reward2, reward3, reward4;

    private const string RewardKey1 = "RewardKey1";
    private const string RewardKey2 = "RewardKey2";
    private const string RewardKey3 = "RewardKey3";
    private const string RewardKey4 = "RewardKey4";
    
    public void OnEnable()
    {   
        play.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        exit.onClick.AddListener(Application.Quit);
        autors.onClick.AddListener(AutorsClick);
        back.onClick.AddListener(BackClick);

        if (PlayerPrefs.HasKey("keyScore"))
        {
            text1.text = PlayerPrefs.GetString("keyScore", "0");
            text2.text = PlayerPrefs.GetString("keyScore", "0");

            if (!int.TryParse(text1.text, out int value))
            {
                text1.text = "0";
                text2.text = "0";
            }
        }

        if (PlayerPrefs.HasKey(RewardKey1))
        {
            if (PlayerPrefs.GetInt(RewardKey1) == 1)
            {
                reward1.gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey(RewardKey2))
        {
            if (PlayerPrefs.GetInt(RewardKey2) == 1)
            {
                reward2.gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey(RewardKey3))
        {
            if (PlayerPrefs.GetInt(RewardKey3) == 1)
            {
                reward3.gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey(RewardKey4))
        {
            if (PlayerPrefs.GetInt(RewardKey4) == 1)
            {
                reward4.gameObject.SetActive(true);
            }
        }
        
    }

    private void BackClick()
    {
        play.gameObject.SetActive(true);
        autors.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        n1.gameObject.SetActive(true);
        n2.gameObject.SetActive(true);
        board.gameObject.SetActive(true);
        
        autor.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
    }

    private void AutorsClick()
    {
        play.gameObject.SetActive(false);
        autors.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        n1.gameObject.SetActive(false);
        n2.gameObject.SetActive(false);
        board.gameObject.SetActive(false);
        
        autor.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
    }


    public void OnDisable()
    {
        
    }
}
