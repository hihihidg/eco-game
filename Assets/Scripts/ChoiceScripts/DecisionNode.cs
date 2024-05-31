using System;
using System.Collections.Generic;

public class DecisionNode
{
    public string contextText;
    public List<Option> options;

    public DecisionNode(string context)
    {
        contextText = context;
        options = new List<Option>();
    }

    public void AddOption(string optionText, string impact, string optionDescripition, DecisionNode nextNode = null)
    {
        options.Add(new Option(optionText, impact, optionDescripition, nextNode));
    }

    public void ApplyImpact(int optionIndex)
    {
        // Applies impact of choice on environment
        if (optionIndex >= 0 && optionIndex < options.Count)
        {
            string impact = options[optionIndex].impact;
            Console.WriteLine("Applied impact: " + impact);
        }
        else
        {
            Console.WriteLine("Invalid option index!");
        }
    }
}

public class Option
{
    // Applies impact of choice on environment

    public string optionText;
    public string optionDescripition;

    public string impact;
    public DecisionNode nextNode;

    public Option(string optionText, string impact, string optionDescripition, DecisionNode node = null)
    {
        this.optionText = optionText;
        this.optionDescripition = optionDescripition;
        this.impact = impact;
        nextNode = node;
    }
}