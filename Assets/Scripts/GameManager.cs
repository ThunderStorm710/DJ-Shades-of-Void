using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    public GameObject gameOverUI; // UI do fim de jogo

    [Header("Gameplay Elements")]
    public int maxPickaxes = 2; // N�mero m�ximo de picaretas
    public int usedPickaxes = 0; // N�mero de picaretas usadas
    public bool crucialWallBroken = false; // Verifica se a parede crucial foi quebrada
    public float recheckDelay = 1f; // Tempo de espera antes de reavaliar



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UsePickaxe()
    {
        usedPickaxes++;
        StartCoroutine(ReevaluateCrucialWall());
    }

    public void BreakCrucialWall()
    {
        crucialWallBroken = true;
    }

    private IEnumerator ReevaluateCrucialWall()
    {
        yield return new WaitForSeconds(recheckDelay);

        if (usedPickaxes >= maxPickaxes && !crucialWallBroken)
        {
            EndGame("Voc� usou todas as suas picaretas e n�o quebrou a parede crucial.");
        }
    }

    void EndGame(string message)
    {
        gameOverUI.SetActive(true);
        //gameOverMessage.text = message;
        Time.timeScale = 0f; // Pausa o jogo
    }
}
