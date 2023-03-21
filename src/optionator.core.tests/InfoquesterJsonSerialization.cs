using optionator.core;

namespace optionator.core.tests;

[TestClass]
public class InfoquesterTests
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
    ""IncorrectAnswers"": {
    ""b"": ""London is the capital of the UK."",
    ""c"": ""New York is a city in the USA."",
    ""d"": ""Tokyo is the capital of Japan.""
    }
}";

    [TestMethod]
    public void TestSerialization()
    {
        // Arrange
        var infoquester = new Infoquester
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
            IncorrectAnswers = new Dictionary<char, string>
                {
                    { 'b', "London is the capital of the UK." },
                    { 'c', "New York is a city in the USA." },
                    { 'd', "Tokyo is the capital of Japan." }
                }
        };

        // Act
        var expectedJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(_json), Formatting.Indented);
        var serializedJson = JsonConvert.SerializeObject(infoquester, Formatting.Indented);

        // Assert
        Assert.AreEqual(expectedJson, serializedJson);
    }

    [TestMethod]
    public void TestDeserialization()
    {
        // Arrange
        var expectedInfoquester = new Infoquester
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
            IncorrectAnswers = new Dictionary<char, string>
                {
                    { 'b', "London is the capital of the UK." },
                    { 'c', "New York is a city in the USA." },
                    { 'd', "Tokyo is the capital of Japan." }
                }
        };

        // Act
        var deserializedInfoquester = JsonConvert.DeserializeObject<Infoquester>(_json);

        // Assert
        Assert.AreEqual(expectedInfoquester.Question, deserializedInfoquester.Question);
        CollectionAssert.AreEqual(expectedInfoquester.Options, deserializedInfoquester.Options);
        CollectionAssert.AreEqual(expectedInfoquester.CorrectAnswers, deserializedInfoquester.CorrectAnswers);
        CollectionAssert.AreEqual(expectedInfoquester.IncorrectAnswers, deserializedInfoquester.IncorrectAnswers);
    }
}