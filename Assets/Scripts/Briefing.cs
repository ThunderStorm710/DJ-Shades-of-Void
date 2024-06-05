using UnityEngine;
using TMPro;
using System.Collections; // Necessário para usar Coroutines

public class Briefing : MonoBehaviour
{
    [System.Serializable]
    public class Objective
    {
        public string description;
        public bool isComplete;
    }

    public Objective[] objectives; // Array de objetivos
    public TextMeshProUGUI briefingText; // Referência ao TextMeshProUGUI
    public float fadeDuration = 1.0f; // Duração do fade em segundos
    private int currentObjectiveIndex = 0; // Índice do objetivo atual

    // Função para completar um objetivo
    public void CompleteObjective()
    {
        Debug.Log("Mais 1 objetivo");
        int index = currentObjectiveIndex;
        if (index < objectives.Length && !objectives[index].isComplete)
        {
            objectives[index].isComplete = true;
            if (index == currentObjectiveIndex)
            {
                StartCoroutine(UpdateBriefingWithFade(index)); // Começa a Coroutine para atualizar o briefing com fade
                currentObjectiveIndex++;
            }
        }
    }

    // Coroutine para atualizar o briefing com animação de cores
    IEnumerator UpdateBriefingWithFade(int index)
    {
        // Fade de branco para amarelo
        float timer = 0;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            briefingText.color = Color.Lerp(Color.white, Color.yellow, t);
            timer += Time.deltaTime;
            yield return null; // Espera até o próximo frame
        }

        briefingText.color = Color.yellow;
        yield return new WaitForSeconds(0.5f); // Pausa com o texto amarelo

        // Atualiza o texto do briefing
        if (index + 1 < objectives.Length)
        {
            currentObjectiveIndex = index + 1; // Avança para o próximo objetivo
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

    // Função para atualizar o texto de briefing sem animação
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
