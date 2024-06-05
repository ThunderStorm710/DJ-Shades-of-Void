using UnityEngine;
using TMPro;
using System.Collections; // Necess�rio para usar Coroutines

public class Briefing : MonoBehaviour
{
    [System.Serializable]
    public class Objective
    {
        public string description;
        public bool isComplete;
    }

    public Objective[] objectives; // Array de objetivos
    public TextMeshProUGUI briefingText; // Refer�ncia ao TextMeshProUGUI
    public float fadeDuration = 1.0f; // Dura��o do fade em segundos
    private int currentObjectiveIndex = 0; // �ndice do objetivo atual

    // Fun��o para completar um objetivo
    public void CompleteObjective()
    {
        Debug.Log("Mais 1 objetivo");
        int index = currentObjectiveIndex;
        if (index < objectives.Length && !objectives[index].isComplete)
        {
            objectives[index].isComplete = true;
            if (index == currentObjectiveIndex)
            {
                StartCoroutine(UpdateBriefingWithFade(index)); // Come�a a Coroutine para atualizar o briefing com fade
                currentObjectiveIndex++;
            }
        }
    }

    // Coroutine para atualizar o briefing com anima��o de cores
    IEnumerator UpdateBriefingWithFade(int index)
    {
        // Fade de branco para amarelo
        float timer = 0;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            briefingText.color = Color.Lerp(Color.white, Color.yellow, t);
            timer += Time.deltaTime;
            yield return null; // Espera at� o pr�ximo frame
        }

        briefingText.color = Color.yellow;
        yield return new WaitForSeconds(0.5f); // Pausa com o texto amarelo

        // Atualiza o texto do briefing
        if (index + 1 < objectives.Length)
        {
            currentObjectiveIndex = index + 1; // Avan�a para o pr�ximo objetivo
        }
        UpdateBriefing();

        // Fade de amarelo para branco
        timer = 0;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            briefingText.color = Color.Lerp(Color.yellow, Color.white, t);
            timer += Time.deltaTime;
            yield return null;
        }

        briefingText.color = Color.white;
    }

    // Fun��o para atualizar o texto de briefing sem anima��o
    void UpdateBriefing()
    {
        briefingText.text = objectives[currentObjectiveIndex].description;
    }

    void Start()
    {
        briefingText.color = Color.white; // Inicia com a cor branca
        UpdateBriefing(); // Atualiza o briefing inicial
    }
}
