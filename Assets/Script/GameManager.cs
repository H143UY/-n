using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int money = 0;
    public int Key;
    //public Text MoneyText;
    public Text KeyText;
    // 
    [Header("Enemy imange")]
    public GameObject lv0;
    public GameObject lv1;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        this.RegisterListener(EventID.Key, (sender, param) =>
        {
            Key--;
        });
        this.RegisterListener(EventID.GiaiDoan2, (sender, param) =>
        {
            lv0.SetActive(false);
            lv1.SetActive(true);
        });
    }
    void Update()
    {
        KeyText.text = ":" + Key;
        if (Key == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            this.PostEvent(EventID.QuaMan);
        }
    }
}
