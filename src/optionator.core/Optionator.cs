using System.Text.Json.Serialization;

namespace optionator.core;

/// <summary>
/// Represents a quiz question with multiple options, correct answers, and incorrect answers, related to the Infosphere in the Futurama universe.
/// </summary>
public class Optionator
{
    [JsonPropertyName("question")]
    public string Question { get; set; }
    
    [JsonPropertyName("options")]
    public Dictionary<char, string> Options { get; set; } 

    [JsonPropertyName("correct_answers")]   
    public List<char> CorrectAnswers { get; set; }


    [JsonPropertyName("explanations")]   
    public Dictionary<char, string> Explanations { get; set; }
}