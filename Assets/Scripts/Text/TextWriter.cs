using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    public TMP_Text textMeshProComponent;

    [TextArea(3, 10)]
    public string fullText;
    public float delayBetweenCharacters = 0.003f;
    public bool isAdvisor = false;
    public bool isImpact = false;
    public SpriteRenderer advisorIcon;
    public StoryManager storyManager;
    public Image panel;
    public TMP_Text continuetext;



    private int currentCharacterIndex;

    public bool dialogueAllDone = false;
    private bool dialogueFinished = false;

    private Coroutine currentCoroutine;
    public Material context_renderer;

    private void Start()
    {
        Application.targetFrameRate = 32;
        currentCharacterIndex = 0;
        //currentCoroutine = StartCoroutine(WriteText());
    }

    public void Reset()
    {
        StopAllCoroutines();
        textMeshProComponent.text = "";
        dialogueAllDone = false;
        dialogueFinished = false;
        currentCharacterIndex = 0;
        panel.enabled = true;

        if (isAdvisor)
        {
            advisorIcon.enabled = true;
            textMeshProComponent.fontSize = 5;
            delayBetweenCharacters = 0;
        }
        else
        {
            advisorIcon.enabled = false;
            textMeshProComponent.fontSize = 6.5f;
            delayBetweenCharacters = 0.001f;

        }
        if (isImpact)
        {
            context_renderer.SetColor("_FaceColor", new Color(0f / 255f, 255f / 255f, 212f / 255f));
        }
        else
        {
            context_renderer.SetColor("_FaceColor", new Color(134f / 255f, 202f / 255f, 62f / 255f));

        }

        currentCoroutine = StartCoroutine(WriteText());


    }

    private void Update()
    {
        if (!dialogueAllDone && dialogueFinished &&  (Input.GetMouseButtonDown(0)))
        {
            dialogueFinished = false;
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            textMeshProComponent.text = ""; // Clear text
            currentCoroutine = StartCoroutine(WriteText());
        }

    }

    IEnumerator WriteText()
    {
        dialogueFinished = false;
        int charactersAddedThisFrame = 0; // Track characters added in this frame

        for (int i = currentCharacterIndex; i < fullText.Length; i++)
        {
     

            if (fullText[i] == '\n' || (textMeshProComponent.text.Length >= 280 && fullText[i] == ' '))
            {
                dialogueFinished = true;
                currentCharacterIndex = i + 1;
                continuetext.text = "Click to continue";
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Wait for space input
                continuetext.text = "";
            }
            else
            {
                continuetext.text = "";

                textMeshProComponent.text += fullText[i];
                charactersAddedThisFrame++;

                if (delayBetweenCharacters != 0 && charactersAddedThisFrame >= 3)
                {
                    charactersAddedThisFrame = 0; // Reset characters added this frame
                    yield return null; // Yield to next frame
                }
                if (delayBetweenCharacters == 0 && charactersAddedThisFrame >= 8)
                {
                    charactersAddedThisFrame = 0; // Reset characters added this frame
                    yield return null; // Yield to next frame
                }
            }
        }

        // Reset current character index when the text ends
        continuetext.text = "Click to continue";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        currentCharacterIndex = 0;
        textMeshProComponent.text = "";
        dialogueAllDone = true;
        panel.enabled = false;
        continuetext.text = "";
        advisorIcon.enabled = false;

        if (isImpact)
        {
            isImpact = false;
            storyManager.DisplayNode();
        }
        else
        {
            Debug.Log("Show Player");
            storyManager.ShowPlayer();
        }
    }

}