namespace PrincessBrideTrivia;

public class Program
{
    public static void Main(string[] args)
    {
        bool continueRunning = true;

        while (continueRunning)
        {
            Console.Write(
                "1. Take Quiz\n" +
                "2. Add To Quiz\n" +
                "3. Quit\n");

            string choice = GetUserInput();

            switch (choice)
            {
                case "1":
                    TakeQuiz();
                    break;

                case "2":
                    AddToQuiz();
                    break;

                case "3":
                    continueRunning = false;
                    continue;

                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                    continue;
            }

            bool repeatOptions = true;
            while (repeatOptions)
            {

                repeatOptions = false;
                Console.Write("Would you like to take more actions? (Y/N): ");
                string answer = GetUserInput();

                switch (answer.Trim().ToLower())
                {
                    case "y":
                        continueRunning = true;
                        break;
                    case "n":
                        continueRunning = false;
                        break;
                    default:
                        repeatOptions = true;
                        Console.WriteLine("Please Enter Proper Answer Y/N!");
                        continue;

                }

                Console.WriteLine();
            }


        }
    }

    public static void TakeQuiz()
    {
        string filePath = GetFilePath();
        Console.WriteLine(Path.GetFullPath(filePath));

        Question[] questions = LoadQuestions(filePath);

        int numberCorrect = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            bool result = AskQuestion(questions[i]);
            if (result)
            {
                numberCorrect++;
            }
        }
        Console.WriteLine("You got " + GetPercentCorrect(numberCorrect, questions.Length) + " correct");
    }

    public static string GetPercentCorrect(int numberCorrectAnswers, int numberOfQuestions)
    {
        float ratio = (float)numberCorrectAnswers / (float)numberOfQuestions;
        int percentage = (int)(ratio * 100);
        return percentage + "%";
    }

    public static bool AskQuestion(Question question)
    {
        DisplayQuestion(question);

        string userGuess = GetUserInput();
        return DisplayResult(userGuess, question);
    }

    public static string GetUserInput()
    {
        return Console.ReadLine();
    }

    public static bool DisplayResult(string userGuess, Question question)
    {
        if (userGuess == question.CorrectAnswerIndex)
        {
            Console.WriteLine("Correct");
            return true;
        }

        Console.WriteLine("Incorrect");
        return false;
    }

    public static void DisplayQuestion(Question question)
    {
        Console.WriteLine("Question: " + question.Text);
        for (int i = 0; i < question.Answers.Length; i++)
        {
            Console.WriteLine((i + 1) + ": " + question.Answers[i]);
        }
    }

    public static string GetFilePath()
    {
        return "Trivia.txt";
    }

    public static Question[] LoadQuestions(string filePath) { 
    
        string[] lines = File.ReadAllLines(filePath);

        Question[] questions = new Question[lines.Length / 5];
        for (int i = 0; i < questions.Length; i++)
        {
            int lineIndex = i * 5;
            string questionText = lines[lineIndex];

            string answer1 = lines[lineIndex + 1];
            string answer2 = lines[lineIndex + 2];
            string answer3 = lines[lineIndex + 3];

            string correctAnswerIndex = lines[lineIndex + 4];

            Question question = new()
            {
                Text = questionText,
                Answers = new string[3]
            };
            question.Answers[0] = answer1;
            question.Answers[1] = answer2;
            question.Answers[2] = answer3;
            question.CorrectAnswerIndex = correctAnswerIndex;

            questions[i] = question;
        }

        return questions;
    }

    //Added feature -> Add New Questions
    public static void AddToQuiz()
    {
        Console.WriteLine("New Question:");
        string question = GetUserInput();
        string[] options = new string[3];

        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine("Option " + (i + 1) + ": ");
            options[i] = GetUserInput();

        }

        Console.WriteLine("Correct Option: ");
        string answer = GetUserInput();

        Question q = new()
        {
            Text = question,
            Answers = options,
            CorrectAnswerIndex = answer
        };

        AddQuestion(q);
    }

    public static bool AddQuestion(Question question)
    {
        string filePath = "Trivia.txt";

        foreach (string option in question.Answers)
        {
            Console.WriteLine(option);
        }

        Console.WriteLine("Correct answer: " + question.CorrectAnswerIndex);

        try
        {
            using StreamWriter writer = new(filePath, append: true);
            writer.WriteLine(); //Escape to new row
            writer.WriteLine(question.Text);

            foreach (string option in question.Answers)
            {
                writer.WriteLine(option);
            }

            writer.Write(question.CorrectAnswerIndex);

            Console.WriteLine("Question Added Successfully");

            writer.Close();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error Adding Question: " + ex.Message);
            return false;
        }
    }

}
