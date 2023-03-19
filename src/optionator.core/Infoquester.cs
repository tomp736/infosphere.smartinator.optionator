namespace optionator.core;

/// <summary>
/// Represents a quiz question with multiple options, correct answers, and incorrect answers, related to the Infosphere in the Futurama universe.
/// </summary>
public class Infoquester
{
    public string Question { get; set; }
    public Dictionary<char, string> Options { get; set; }    
    public List<char> CorrectAnswers { get; set; }
    public Dictionary<char, string> IncorrectAnswers { get; set; }
}