
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionDescription : MonoBehaviour
{
    public TextWriter contextText;
    public GameObject player;
    public string displayText;

    

    public void OnMouseOver()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if ((contextText.dialogueAllDone) && distance < 40)
        {
            contextText.isAdvisor = true;
            contextText.fullText = displayText;
            contextText.Reset();
        }
    }


}