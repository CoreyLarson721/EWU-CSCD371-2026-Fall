namespace PrincessBrideTrivia.Tests;

[TestClass]
public class ProgramTests
{
    [TestMethod]
    public void LoadQuestions_ValidFilePath_ReturnsCorrectNumberOfQuestions()
    {
        string filePath = Path.GetRandomFileName();
        try
        {
            // Arrange
            GenerateQuestionsFile(filePath, 2);

            // Act
            Question[] questions = Program.LoadQuestions(filePath);

            // Assert 
            Assert.HasCount(2, questions);
        }
        finally
        {
            File.Delete(filePath);
        }
    }

    [TestMethod]
    [DataRow("1", true)]
    [DataRow("2", false)]
    public void DisplayResult_ValidUserGuess_ReturnsExpectedBoolean(string userGuess, bool expectedResult)
    {
        // Arrange
        Question question = new();
        question.CorrectAnswerIndex = "1";

        // Act
        bool displayResult = Program.DisplayResult(userGuess, question);

        // Assert
        Assert.AreEqual(expectedResult, displayResult);
    }

    [TestMethod]
    public void GetFilePath_WhenCalled_ReturnsExistingFilePath()
    {
        // Arrange

        // Act
        string filePath = Program.GetFilePath();

        // Assert
        Assert.IsTrue(File.Exists(filePath));
    }

    [TestMethod]
    [DataRow(1, 1, "100%")]
    [DataRow(5, 10, "50%")]
    [DataRow(1, 10, "10%")]
    [DataRow(0, 10, "0%")]
    public void GetPercentCorrect_ValidCorrectAndTotalCounts_ReturnsFormattedPercentageString(int numberOfCorrectGuesses,
        int numberOfQuestions, string expectedString)
    {
        // Arrange

        // Act
        string percentage = Program.GetPercentCorrect(numberOfCorrectGuesses, numberOfQuestions);

        // Assert
        Assert.AreEqual(expectedString, percentage);
    }


    private static void GenerateQuestionsFile(string filePath, int numberOfQuestions)
    {
        for (int i = 0; i < numberOfQuestions; i++)
        {
            string[] lines =
            [
                "Question " + i + " this is the question text",
                "Answer 1",
                "Answer 2",
                "Answer 3",
                "2",
            ];
            File.AppendAllLines(filePath, lines);
        }
    }

    [TestMethod]
    public void AddQuestion_ValidQuestion_AddsQuestionToFile()
    {
        // Arrange
        string filePath = "Trivia.txt";

        Question question = new();
        question.Text = "Testing Question";
        question.Answers =
        [
            "No",
        "Yes",
        "Maybe"
        ];
        question.CorrectAnswerIndex = "2";

        // Act
        bool result = Program.AddQuestion(question);

        // Assert
        Assert.IsTrue(result);
        Assert.IsTrue(File.Exists(filePath));

        string[] lines = File.ReadAllLines(filePath);

        Assert.Contains("Testing Question", lines);
        Assert.Contains("No", lines);
        Assert.Contains("Yes", lines);
        Assert.Contains("Maybe", lines);
        Assert.Contains("2", lines);
        Assert.Contains("2", lines);
    }

    [TestMethod]
    public void AddQuestion_ValidQuestion_WritesFiveLines()
    {
        // Arrange
        string filePath = "Trivia.txt";

        Question question = new()
        {
            Text = "Testing Question",
            Answers =
            [
                "No",
            "Yes",
            "Maybe"
            ],
            CorrectAnswerIndex = "2"
        };

        // Get the number of lines before adding the question
        string[] linesBefore = File.ReadAllLines(filePath);
        int originalLineCount = linesBefore.Length;

        // Act
        bool result = Program.AddQuestion(question);

        // Assert
        Assert.IsTrue(result);

        string[] linesAfter = File.ReadAllLines(filePath);

        // The question should have added exactly 5 lines
        Assert.HasCount(originalLineCount + 5, linesAfter);

        // Check the five newly added lines
        int newQuestionIndex = originalLineCount;

        Assert.AreEqual("Testing Question", linesAfter[newQuestionIndex]);
        Assert.AreEqual("No", linesAfter[newQuestionIndex + 1]);
        Assert.AreEqual("Yes", linesAfter[newQuestionIndex + 2]);
        Assert.AreEqual("Maybe", linesAfter[newQuestionIndex + 3]);
        Assert.AreEqual("2", linesAfter[newQuestionIndex + 4]);
    }
}