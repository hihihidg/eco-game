using UnityEngine;

[CreateAssetMenu(fileName = "ImpactDescriptions", menuName = "Custom/ImpactDescriptions", order = 1)]
public class ImpactDescriptions : ScriptableObject
{
    [System.Serializable]
    public struct ImpactDescription
    {
        public int impactValue;
        public string description;
    }

    public ImpactDescription[] descriptions;
}