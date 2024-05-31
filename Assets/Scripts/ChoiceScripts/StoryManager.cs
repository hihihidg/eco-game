using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class StoryManager : MonoBehaviour
{
    private DecisionNode currentNode;
    private bool storyEnded = false;

    public TextWriter contextText;
    public GameObject player;
    public GameObject decisionGroup;

    public TMP_Text option1Button;
    public TMP_Text option2Button;

    public OptionDescription OptionDescription1;
    public OptionDescription OptionDescription2;
    public Loader loader;


    void Start()
    {
        string filePath = "eco_game_decisions_real";
        JsonParser.JsonDataList parsedData = JsonParser.ParseJson(filePath);
        List<DecisionNode> decisionTree = new List<DecisionNode>();

        
        foreach (JsonParser.JsonData data in parsedData.JsonData)
        {
            DecisionNode node = new DecisionNode(data.Context);
            node.AddOption(data.Option1Text, data.Option1Impact, data.Option1Description);
            node.AddOption(data.Option2Text, data.Option2Impact, data.Option2Description);
            decisionTree.Add(node);
        }
 

        for (int i = 0; i < parsedData.JsonData.Length; i++)
        {
            DecisionNode currentNode = decisionTree[i];

            // Link nodes
            currentNode.options[0].nextNode = decisionTree[parsedData.JsonData[i].Option1NextNode+1];
            currentNode.options[1].nextNode = decisionTree[parsedData.JsonData[i].Option2NextNode+1];

        }

        currentNode = decisionTree[1];
        DisplayNode();

        // Define decision tree
        /*
        // Define decision tree
        DecisionNode node1 = new DecisionNode("You discover a new energy source.");
        node1.AddOption("Invest in fusion research (Eco +30)", 30,"");
        node1.AddOption("Explore wind energy (Eco +20)", 20,"");

        DecisionNode node2 = new DecisionNode("You encounter a trash-filled street.");
        node2.AddOption("Organize a community clean-up (Eco +15)", 15);
        node2.AddOption("Implement recycling programs (Eco +10)", 10);

        DecisionNode node3 = new DecisionNode("You have the opportunity to develop space travel.");
        node3.AddOption("Focus on sustainable space exploration (Eco +25)", 25);
        node3.AddOption("Prioritize Earth's environmental health (Eco +20)", 20);

        DecisionNode node4 = new DecisionNode("You face a decision regarding transportation.");
        node4.AddOption("Invest in electric vehicle infrastructure (Eco +15)", 15);
        node4.AddOption("Promote public transportation (Eco +10)", 10);

        DecisionNode node5 = new DecisionNode("You come across a factory emitting pollution.");
        node5.AddOption("Advocate for stricter environmental regulations (Eco +20)", 20);
        node5.AddOption("Support green technology adoption (Eco +25)", 25);

        DecisionNode node6 = new DecisionNode("You discover a new method of waste management.");
        node6.AddOption("Implement innovative recycling technologies (Eco +25)", 25);
        node6.AddOption("Educate the community on reducing waste (Eco +20)", 20);

        // Reuse some nodes
        DecisionNode node7 = node2; // Reusing the trash-filled street scenario
        DecisionNode node8 = node3; // Reusing the space travel scenario
        DecisionNode node9 = node4; // Reusing the transportation decision
        DecisionNode node10 = node5; // Reusing the factory pollution scenario

        DecisionNode node11 = new DecisionNode("You face a dilemma about water conservation.");
        node11.AddOption("Implement water-saving technologies (Eco +20)", 20);
        node11.AddOption("Promote rainwater harvesting practices (Eco +15)", 15);

        DecisionNode node12 = new DecisionNode("You encounter a decision on forest conservation.");
        node12.AddOption("Support reforestation efforts (Eco +25)", 25);
        node12.AddOption("Implement sustainable logging practices (Eco +20)", 20);

        DecisionNode node13 = new DecisionNode("You face a challenge in protecting marine ecosystems.");
        node13.AddOption("Establish marine protected areas (Eco +30)", 30);
        node13.AddOption("Promote sustainable fishing practices (Eco +25)", 25);

        // Connect nodes
        node1.options[0].nextNode = node3;
        node1.options[1].nextNode = node4;

        node2.options[0].nextNode = node6;
        node2.options[1].nextNode = node7;

        node3.options[0].nextNode = node8;
        node3.options[1].nextNode = node1;

        node4.options[0].nextNode = node9;
        node4.options[1].nextNode = node10;

        node5.options[0].nextNode = node10;
        node5.options[1].nextNode = node11;

        node6.options[0].nextNode = node11;
        node6.options[1].nextNode = node12;

        node7.options[0].nextNode = node12;
        node7.options[1].nextNode = node13;

        // Start the story from the first node
        currentNode = node1;

        // Display initial context text and options
        DisplayNode();
        */
    }


    public void OptionClicked(int option_number)
    {
        if (storyEnded)
            return;
        ChooseOption(option_number); 
    }


    void ChooseOption(int optionIndex)
    {
        transform.position = new Vector3(-2.1f, transform.position.y, transform.position.z);

        if (optionIndex >= 0 && optionIndex < currentNode.options.Count)
        {
            // Apply impact of the chosen option
            contextText.isAdvisor = false;
            contextText.isImpact = true;
            decisionGroup.SetActive(false);
            player.SetActive(false);
            contextText.fullText = currentNode.options[optionIndex].impact;
            contextText.Reset();

            // Move to next node
            currentNode = currentNode.options[optionIndex].nextNode;
            if (currentNode == null || currentNode.contextText == "")
            {
                contextText.isAdvisor = false;
                Debug.Log("End of story!");
                contextText.fullText = "The End.";
                contextText.Reset();

                storyEnded = true;
            }
            else
            {
                //DisplayNode();
            }

        }
    }

    public void ShowPlayer()
    {

        decisionGroup.SetActive(true);
        player.SetActive(true);
    }


    public void DisplayNode()
    {
        bool ending = false;
        // Check for ending
        switch (currentNode.contextText)
        {
            case "PeakECO":
                loader.LoadSceneWithDelay(0);
                break;
            case "Peak Renewable":
                loader.LoadSceneWithDelay(1);
                break;
            case "PeakFusion":
                loader.LoadSceneWithDelay(2);
                break;
            case "Renewable":
                loader.LoadSceneWithDelay(3);
                break;
            case "Austerity Measures Trains":
                loader.LoadSceneWithDelay(4);
                break;
            case "Weak tax Trains":
                loader.LoadSceneWithDelay(5);
                break;
            case "Austerity Measures Electric cars":
                loader.LoadSceneWithDelay(6);
                break;
            case "Least ECO":
                loader.LoadSceneWithDelay(7);
                break;
            default:
                // Handle default case if needed
                break;
        }

        switch (currentNode.contextText)
        {
            case "PeakECO":
            case "Peak Renewable":
            case "PeakFusion":
            case "Renewable":
            case "Austerity Measures Trains":
            case "Weak tax Trains":
            case "Austerity Measures Electric cars":
            case "Least ECO":
                ending = true;
                break;
            default:
                break;
        }


        Debug.Log("Displaying Node");
        contextText.isAdvisor = false;
        if (!ending)
        {
            contextText.fullText = currentNode.contextText;
        }
        else
        {
            contextText.fullText = "The End.";
        }

        contextText.Reset();

        // Update option buttons
        for (int i = 0; i < currentNode.options.Count; i++)
        {
            if (i == 0)
            {
                OptionDescription1.displayText = currentNode.options[i].optionDescripition;
                option1Button.text = currentNode.options[i].optionText;
            }
            else if (i == 1)
            {
                OptionDescription2.displayText = currentNode.options[i].optionDescripition;
                option2Button.text = currentNode.options[i].optionText;
            }
        }
    }
}