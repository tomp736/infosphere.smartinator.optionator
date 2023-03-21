using optionator.core;

namespace optionator.core.tests;

[TestClass]
public class OptionatorTests
{   
    string _json = @"{
    ""Question"": ""What is the capital of France?"",
    ""Options"": {
    ""a"": ""Paris"",
    ""b"": ""London"",
    ""c"": ""New York"",
    ""d"": ""Tokyo""
    },
    ""CorrectAnswers"": [
    ""a""
    ],
    ""Explanations"": {
    ""b"": ""London is the capital of the UK."",
    ""c"": ""New York is a city in the USA."",
    ""d"": ""Tokyo is the capital of Japan.""
    }
}";

    [TestMethod]
    public void TestSerialization()
    {
        // Arrange
        var optionator = new Optionator
        {
            Question = "What is the capital of France?",
            Options = new Dictionary<char, string>
                {
                    { 'a', "Paris" },
                    { 'b', "London" },
                    { 'c', "New York" },
                    { 'd', "Tokyo" }
                },
            CorrectAnswers = new List<char> { 'a' },
            Explanations = new Dictionary<char, string>
                {
                    { 'b', "London is the capital of the UK." },
                    { 'c', "New York is a city in the USA." },
                    { 'd', "Tokyo is the capital of Japan." }
                }
        };

        // Act
        var expectedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(_json), Formatting.Indented);
        var serializedJson = JsonConvert.SerializeObject(optionator, Formatting.Indented);

        // Assert
        Assert.AreEqual(expectedJson, serializedJson);
    }

    [TestMethod]
    public void TestDeserialization()
    {
        // Arrange
        var expectedOptionator = new Optionator
        {
            Question = "What is the capital of France?",
            Options = new Dictionary<char, string>
                {
                    { 'a', "Paris" },
                    { 'b', "London" },
                    { 'c', "New York" },
                    { 'd', "Tokyo" }
                },
            CorrectAnswers = new List<char> { 'a' },
            Explanations = new Dictionary<char, string>
                {
                    { 'b', "London is the capital of the UK." },
                    { 'c', "New York is a city in the USA." },
                    { 'd', "Tokyo is the capital of Japan." }
                }
        };

        // Act
        var deserializedOptionator = JsonConvert.DeserializeObject<Optionator>(_json);

        // Assert
        Assert.AreEqual(expectedOptionator.Question, deserializedOptionator.Question);
        CollectionAssert.AreEqual(expectedOptionator.Options, deserializedOptionator.Options);
        CollectionAssert.AreEqual(expectedOptionator.CorrectAnswers, deserializedOptionator.CorrectAnswers);
        CollectionAssert.AreEqual(expectedOptionator.Explanations, deserializedOptionator.Explanations);
    }
}