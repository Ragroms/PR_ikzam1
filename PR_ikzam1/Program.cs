using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System;

1.Напишите программу, в которой генерируется случайное целое число (например, в диапазоне от 1 до 10), а пользователю необходимо его угадать. Если пользователь не угадал число, программа должна выдать запрос о том, хочет ли он попробовать еще раз.
using System;

namespace GuessTheNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int secretNumber = random.Next(1, 11); // Генерируем случайное число от 1 до 10 (11 исключается)
            int guess = 0;
            int attempts = 0;
            bool guessedCorrectly = false;

            Console.WriteLine("Я загадал число от 1 до 10. Попробуй угадать.");

            while (!guessedCorrectly)
            {
                try
                {
                    Console.Write("Твоя догадка: ");
                    guess = int.Parse(Console.ReadLine());
                    attempts++;

                    if (guess < secretNumber)
                    {
                        Console.WriteLine("Загаданное число больше.");
                    }
                    else if (guess > secretNumber)
                    {
                        Console.WriteLine("Загаданное число меньше.");
                    }
                    else
                    {
                        Console.WriteLine($"Поздравляю! Ты угадал число {secretNumber} за {attempts} попыток.");
                        guessedCorrectly = true;
                        break; // Выходим из цикла
                    }

                    Console.Write("Хочешь попробовать еще раз? (да/нет): ");
                    string tryAgain = Console.ReadLine().ToLower();

                    if (tryAgain != "да")
                    {
                        Console.WriteLine($"Было загадано число {secretNumber}. До свидания!");
                        break; // Выходим из цикла
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Пожалуйста, введи целое число от 1 до 10.");
                }
            }

            Console.ReadKey(); // Чтобы консоль не закрывалась сразу
        }
    }
}

2.Напишите программу, в которой пользователь вводит дату своего рождения. Программа должна вычислять, сколько прошло полных лет, месяцев и дней от указанной даты до текущей.
using System;

namespace AgeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Введите дату вашего рождения в формате ДД.ММ.ГГГГ: ");
                string dateString = Console.ReadLine();

                DateTime birthDate = DateTime.ParseExact(dateString, "dd.MM.yyyy", null);
                DateTime today = DateTime.Today;

                // Вычисляем разницу
                int years = today.Year - birthDate.Year;
                int months = today.Month - birthDate.Month;
                int days = today.Day - birthDate.Day;

                // Корректируем значения, если месяц или день рождения еще не наступил в текущем году
                if (months < 0 || (months == 0 && days < 0))
                {
                    years--;
                    months += 12;
                }

                if (days < 0)
                {
                    months--;
                    days += DateTime.DaysInMonth(birthDate.Year + years, birthDate.Month); // Добавляем количество дней в предыдущем месяце
                }

                Console.WriteLine($"Вам полных: {years} лет, {months} месяцев, {days} дней.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный формат даты. Пожалуйста, введите дату в формате ДД.ММ.ГГГГ.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
3.Напишите программу, в которой считывается содержимое текстового файла и создается новый текстовый файл. В новый текстовый файл должен заноситься текст из исходного текстового файла и все пробелы должны быть заменены подчеркиваниями, а заглавные буквы - строчными.
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Укажите путь к исходному текстовому файлу
        string inputFilePath = "input.txt"; // Замените на путь к вашему файлу
        // Укажите путь к новому текстовому файлу
        string outputFilePath = "output.txt"; // Замените на желаемый путь для сохранения

        try
        {
            // Считываем содержимое исходного файла
            string content = File.ReadAllText(inputFilePath);

            // Заменяем пробелы на подчеркивания и преобразуем заглавные буквы в строчные
            string modifiedContent = content.Replace(' ', '_').ToLower();

            // Записываем измененное содержимое в новый файл
            File.WriteAllText(outputFilePath, modifiedContent);

            Console.WriteLine("Файл успешно обработан. Результат сохранен в " + outputFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }
}
4.Напишите метод, который преобразует строку в формат camelCase, то есть первая буква во всех словах должна быть заглавной, а пробелы должны быть удалены. Должна быть реализована возможность сохранять данные в файл и считывать данные из файла. Пример вывода результата программы изображен на рисунке.
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Укажите путь к исходному текстовому файлу
        string inputFilePath = "input.txt"; // Замените на путь к вашему файлу
        // Укажите путь к новому текстовому файлу
        string outputFilePath = "output.txt"; // Замените на желаемый путь для сохранения

        try
        {
            // Считываем содержимое исходного файла
            string content = File.ReadAllText(inputFilePath);
            Console.WriteLine("Считанное содержимое:\n" + content);

            // Преобразуем строку в формат camelCase
            string camelCaseResult = ConvertToCamelCase(content);

            // Записываем результат в новый файл
            File.WriteAllText(outputFilePath, camelCaseResult);
            Console.WriteLine("Результат в формате camelCase:\n" + camelCaseResult);
            Console.WriteLine("Результат сохранен в " + outputFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }
    }

    static string ConvertToCamelCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Разделяем строку на слова, удаляем лишние пробелы и преобразуем каждое слово
        var words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower());

        // Объединяем слова без пробелов
        return string.Concat(words);
    }
}
5.Напишите функцию, которая принимает строку из одного или нескольких слов и возвращает ту же строку, но все слова, содержащие пять или более букв, меняются местами (как в показано в примере). Передаваемые строки должны состоять только из букв и пробелов. Пробелы должны быть включены только в том случае, если присутствует более одного слова.
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите строку из одного или нескольких слов:");
        string input = Console.ReadLine();

        string result = RearrangeLongWords(input);
        Console.WriteLine("Результат: " + result);
    }

    static string RearrangeLongWords(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Разделяем строку на слова
        var words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Список для хранения длинных слов
        List<string> longWords = new List<string>();

        // Заполняем список длинных слов
        foreach (var word in words)
        {
            if (word.Length >= 5)
            {
                longWords.Add(word);
            }
        }

        // Если нет длинных слов, возвращаем исходную строку
        if (longWords.Count == 0)
            return input;

        // Переставляем длинные слова местами
        longWords.Reverse();

        // Создаем новый список для результата
        List<string> resultWords = new List<string>();

        // Индекс для длинных слов
        int longWordIndex = 0;

        foreach (var word in words)
        {
            if (word.Length >= 5)
            {
                resultWords.Add(longWords[longWordIndex]);
                longWordIndex++;
            }
            else
            {
                resultWords.Add(word);
            }
        }

        // Объединяем слова обратно в строку
        return string.Join(" ", resultWords);
    }
}
6.Напишите функцию, которая принимает массив из 10 целых чисел (от 0 до 9) и возвращает строку из этих чисел в виде телефонного номера.
using System;

class Program
{
    static void Main(string[] args)
    {
        int[] phoneDigits = new int[10];

        Console.WriteLine("Введите 10 целых чисел от 0 до 9, разделенных пробелами:");
        string input = Console.ReadLine();

        // Преобразуем введенные данные в массив целых чисел
        string[] inputStrings = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (inputStrings.Length != 10 || !TryParseDigits(inputStrings, phoneDigits))
        {
            Console.WriteLine("Ошибка: необходимо ввести ровно 10 целых чисел от 0 до 9.");
            return;
        }

        string phoneNumber = FormatPhoneNumber(phoneDigits);
        Console.WriteLine("Форматированный номер телефона: " + phoneNumber);
    }

    static bool TryParseDigits(string[] inputStrings, int[] digits)
    {
        for (int i = 0; i < inputStrings.Length; i++)
        {
            if (!int.TryParse(inputStrings[i], out digits[i]) || digits[i] < 0 || digits[i] > 9)
            {
                return false;
            }
        }
        return true;
    }

    static string FormatPhoneNumber(int[] digits)
    {
        // Форматируем номер телефона в виде (XXX) XXX-XXXX
        return $"({digits[0]}{digits[1]}{digits[2]}) {digits[3]}{digits[4]}{digits[5]}-{digits[6]}{digits[7]}{digits[8]}{digits[9]}";
    }
}
7.Напишите функцию, которая будет принимать любое неотрицательное целое число в качестве аргумента и возвращать его с его цифрами в порядке убывания. Пример вывода результата программы показан на рисунке.  
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите неотрицательное целое число:");
        string input = Console.ReadLine();

        if (ulong.TryParse(input, out ulong number))
        {
            string result = SortDigitsDescending(number);
            Console.WriteLine("Цифры в порядке убывания: " + result);
        }
        else
        {
            Console.WriteLine("Ошибка: необходимо ввести неотрицательное целое число.");
        }
    }

    static string SortDigitsDescending(ulong number)
    {
        // Преобразуем число в строку, затем в массив символов
        char[] digits = number.ToString().ToCharArray();

        // Сортируем массив символов в порядке убывания
        Array.Sort(digits);
        Array.Reverse(digits);

        // Преобразуем отсортированный массив обратно в строку
        return new string(digits);
    }
}
8.Создайте тестовый проект на MSUnit. Напишите тест для метода Add(int a, int b), в проекте bilet_n, который возвращает сумму двух чисел. Убедитесь, что тест проверяет корректность работы метода для положительных, отрицательных и нулевых значений.
CSS:
namespace bilet_n
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
MSUnit:
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bilet_n;

namespace bilet_n.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [TestMethod]
        public void Add_PositiveNumbers_ReturnsCorrectSum()
        {
            int result = _calculator.Add(5, 10);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Add_NegativeNumbers_ReturnsCorrectSum()
        {
            int result = _calculator.Add(-5, -10);
            Assert.AreEqual(-15, result);
        }

        [TestMethod]
        public void Add_PositiveAndNegativeNumbers_ReturnsCorrectSum()
        {
            int result = _calculator.Add(5, -3);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Add_Zero_ReturnsSameNumber()
        {
            int result1 = _calculator.Add(0, 5);
            Assert.AreEqual(5, result1);

            int result2 = _calculator.Add(5, 0);
            Assert.AreEqual(5, result2);

            int result3 = _calculator.Add(0, 0);
            Assert.AreEqual(0, result3);
        }
    }
}
9.Создайте тестовый проект на NUnit. Напишите тест для метода Divide(int a, int b) в проекте bilet_n, который выбрасывает исключение DivideByZeroException, если b равен нулю. Проверьте, что исключение действительно выбрасывается.
CSS:
namespace bilet_n
{
    public class Calculator
    {
        public int Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Division by zero is not allowed.");
            }
            return a / b;
        }
    }
}
NUnit:
using NUnit.Framework;
using bilet_n;

namespace bilet_n.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Divide_WhenDividingByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            int a = 10;
            int b = 0;

            // Act & Assert
            var ex = Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
            Assert.That(ex.Message, Is.EqualTo("Division by zero is not allowed."));
        }
    }
}
10.Создайте тестовый проект на MSUnit. Создайте параметризованный тест для метода IsEven(int number) проекта bilet_n, который проверяет, является ли число четным. Используйте атрибут [DataTestMethod] и[DataRow] для передачи различных входных данных.
CSS:
namespace bilet_n
{
    public class Calculator
    {
        public bool IsEven(int number)
        {
            return number % 2 == 0;
        }
    }
}
MSUnit:
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bilet_n;

namespace bilet_n.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [TestMethod]
        [DataRow(2, true)]
        [DataRow(3, false)]
        [DataRow(0, true)]
        [DataRow(-4, true)]
        [DataRow(-5, false)]
        public void IsEven_ReturnsCorrectResult(int number, bool expected)
        {
            // Act
            var result = _calculator.IsEven(number);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
11.Создайте тестовый проект на NUnit. Напишите тест для метода Factorial(int n) проекта bilet_n, который вычисляет факториал числа. Проверьте корректность работы метода для различных входных данных, включая граничные случаи.
CSS:
namespace bilet_n
{
    public class Calculator
    {
        public long Factorial(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "Факториал не определен для отрицательных чисел.");
            if (n == 0 || n == 1)
                return 1;

            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
NUnit:
using NUnit.Framework;
using bilet_n;

namespace bilet_n.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Factorial_OfZero_ReturnsOne()
        {
            // Act
            var result = _calculator.Factorial(0);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Factorial_OfOne_ReturnsOne()
        {
            // Act
            var result = _calculator.Factorial(1);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Factorial_OfPositiveNumber_ReturnsCorrectResult()
        {
            // Act & Assert
            Assert.AreEqual(120, _calculator.Factorial(5)); // 5! = 120
            Assert.AreEqual(720, _calculator.Factorial(6)); // 6! = 720
            Assert.AreEqual(5040, _calculator.Factorial(7)); // 7! = 5040
        }

        [Test]
        public void Factorial_OfNegativeNumber_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Factorial(-1));
            Assert.That(ex.Message, Does.Contain("Факториал не определен для отрицательных чисел."));
        }
    }
}
12.Создайте тестовый проект на xUnit. Напишите тест для метода IsPalindrome(string text) проекта bilet_n, который проверяет, является ли строка палиндромом. Проверьте корректность работы метода для различных строк.
CSS:
using System;

namespace bilet_n
{
    public class StringUtilities
    {
        public bool IsPalindrome(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            int left = 0;
            int right = text.Length - 1;

            while (left < right)
            {
                if (char.ToLower(text[left]) != char.ToLower(text[right]))
                    return false;

                left++;
                right--;
            }

            return true;
        }
    }
}
xUnit:
using Xunit;
using bilet_n;

namespace bilet_n.Tests
{
    public class StringUtilitiesTests
    {
        private readonly StringUtilities _stringUtilities;

        public StringUtilitiesTests()
        {
            _stringUtilities = new StringUtilities();
        }

        [Theory]
        [InlineData("racecar", true)]
        [InlineData("A man a plan a canal Panama", true)]
        [InlineData("hello", false)]
        [InlineData("No lemon, no melon", true)]
        [InlineData("12321", true)]
        [InlineData("12345", false)]
        [InlineData("", false)] // пустая строка
        [InlineData(null, false)] // null строка
        public void IsPalindrome_ShouldReturnExpectedResult(string input, bool expected)
        {
            // Act
            var result = _stringUtilities.IsPalindrome(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
13.Напишите тестовые случаи для функции, которая проверяет, является ли строка валидным email-адресом. Функция возвращает true, если email валиден, и false в противном случае.
CSS:
using System;
using System.Text.RegularExpressions;

public class EmailValidator
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Регулярное выражение для проверки email
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
}
NUnit:
using NUnit.Framework;

[TestFixture]
public class EmailValidatorTests
{
    [TestCase("test@example.com", ExpectedResult = true)]
    [TestCase("user.name+tag+sorting@example.com", ExpectedResult = true)]
    [TestCase("user@subdomain.example.com", ExpectedResult = true)]
    [TestCase("user@domain.co.in", ExpectedResult = true)]
    [TestCase("plainaddress", ExpectedResult = false)]
    [TestCase("@missingusername.com", ExpectedResult = false)]
    [TestCase("username@.com", ExpectedResult = false)]
    [TestCase("username@domain..com", ExpectedResult = false)]
    [TestCase("username@domain.c", ExpectedResult = false)]
    [TestCase("username@domain.c123", ExpectedResult = false)]
    [TestCase("", ExpectedResult = false)]
    [TestCase(" ", ExpectedResult = false)]
    [TestCase("user!#$%&'*+/=?^_`{|}~@example.com", ExpectedResult = true)]
    [TestCase("user name@example.com", ExpectedResult = false)]
    public bool TestIsValidEmail(string email)
    {
        return EmailValidator.IsValidEmail(email);
    }
}
14.Напишите тестовые случаи для функции, которая принимает сумму покупки и возвращает размер скидки в зависимости от суммы:
a.Если сумма меньше 1000, скидка 0%.
b. Если сумма от 1000 до 5000, скидка 5%.
c. Если сумма больше 5000, скидка 10%.
CSS:
public class DiscountCalculator
{
    public static decimal CalculateDiscount(decimal purchaseAmount)
    {
        if (purchaseAmount < 1000)
        {
            return 0m; // Скидка 0%
        }
        else if (purchaseAmount >= 1000 && purchaseAmount <= 5000)
        {
            return purchaseAmount * 0.05m; // Скидка 5%
        }
        else // purchaseAmount > 5000
        {
            return purchaseAmount * 0.10m; // Скидка 10%
        }
    }
}
NUnit:
using NUnit.Framework;

[TestFixture]
public class DiscountCalculatorTests
{
    [TestCase(999, ExpectedResult = 0)]
    [TestCase(0, ExpectedResult = 0)]
    [TestCase(-100, ExpectedResult = 0)] // Если отрицательные суммы допустимы
    [TestCase(1000, ExpectedResult = 50)]
    [TestCase(2500, ExpectedResult = 125)]
    [TestCase(5000, ExpectedResult = 250)]
    [TestCase(5001, ExpectedResult = 500.1)]
    [TestCase(10000, ExpectedResult = 1000)]
    [TestCase(15000, ExpectedResult = 1500)]
    public decimal TestCalculateDiscount(decimal purchaseAmount)
    {
        return DiscountCalculator.CalculateDiscount(purchaseAmount);
    }
}
15.Напишите тестовые случаи для функции, которая принимает массив чисел и возвращает максимальное число. Если массив пуст, функция возвращает null.
CSS:
using System;

public class MaxNumberFinder
{
    public static int? FindMax(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
        {
            return null; // Возвращаем null для пустого массива
        }

        int max = numbers[0];
        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }

        return max;
    }
}
NUnit:
using NUnit.Framework;

[TestFixture]
public class MaxNumberFinderTests
{
    [Test]
    public void TestFindMax_EmptyArray_ReturnsNull()
    {
        Assert.IsNull(MaxNumberFinder.FindMax(new int[0]));
        Assert.IsNull(MaxNumberFinder.FindMax(null));
    }

    [TestCase(new int[] { 5 }, ExpectedResult = 5)]
    [TestCase(new int[] { -3 }, ExpectedResult = -3)]
    [TestCase(new int[] { 0 }, ExpectedResult = 0)]
    public int TestFindMax_SingleElementArray(int[] numbers)
    {
        return MaxNumberFinder.FindMax(numbers).Value;
    }

    [TestCase(new int[] { 1, 2, 3, 4, 5 }, ExpectedResult = 5)]
    [TestCase(new int[] { -1, -2, -3, -4 }, ExpectedResult = -1)]
    [TestCase(new int[] { 10, 20, 30 }, ExpectedResult = 30)]
    [TestCase(new int[] { 100, 200, 300 }, ExpectedResult = 300)]
    public int TestFindMax_MultipleElementsArray(int[] numbers)
    {
        return MaxNumberFinder.FindMax(numbers).Value;
    }

    [TestCase(new int[] { 1, 2, 2, 1 }, ExpectedResult = 2)]
    [TestCase(new int[] { 7, 7, 7 }, ExpectedResult = 7)]
    public int TestFindMax_ArrayWithDuplicates(int[] numbers)
    {
        return MaxNumberFinder.FindMax(numbers).Value;
    }

    [TestCase(new int[] { -10, -20, 0, 10 }, ExpectedResult = 10)]
    [TestCase(new int[] { -5, -1, -3 }, ExpectedResult = -1)]
    public int TestFindMax_NegativeAndPositiveNumbers(int[] numbers)
    {
        return MaxNumberFinder.FindMax(numbers).Value;
    }
}
16.Напишите тестовые случаи для функции, которая проверяет, соответствует ли пароль следующим требованиям:
a.Длина пароля не менее 8 символов.
b. Пароль содержит хотя бы одну цифру.
c. Пароль содержит хотя бы одну заглавную букву.
d. Пароль содержит хотя бы один специальный символ (!@#$%^&*).
Функция должна возвращать true, если пароль валиден, и false в противном случае.
CSS:
using System;
using System.Text.RegularExpressions;

public class PasswordValidator
{
    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            return false; // Длина пароля не менее 8 символов
        }

        if (!Regex.IsMatch(password, @"\d"))
        {
            return false; // Пароль должен содержать хотя бы одну цифру
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return false; // Пароль должен содержать хотя бы одну заглавную букву
        }

        if (!Regex.IsMatch(password, @"[!@#$%^&*]"))
        {
            return false; // Пароль должен содержать хотя бы один специальный символ
        }

        return true; // Пароль валиден
    }
}
NUnit:
using NUnit.Framework;

[TestFixture]
public class PasswordValidatorTests
{
    [TestCase("short", ExpectedResult = false)]
    [TestCase("abcdefgh", ExpectedResult = false)]
    [TestCase("12345678", ExpectedResult = false)]
    [TestCase("ABCDEFGH", ExpectedResult = false)]
    [TestCase("abcde!fg", ExpectedResult = false)]
    [TestCase("abc12345", ExpectedResult = false)]
    public bool TestIsValidPassword_InvalidPasswords(string password)
    {
        return PasswordValidator.IsValidPassword(password);
    }

    [TestCase("Abcdef1!", ExpectedResult = true)]
    [TestCase("Password123!", ExpectedResult = true)]
    [TestCase("1Secure@Password", ExpectedResult = true)]
    [TestCase("ValidPass2$", ExpectedResult = true)]
    [TestCase("StrongP@ssw0rd", ExpectedResult = true)]
    public bool TestIsValidPassword_ValidPasswords(string password)
    {
        return PasswordValidator.IsValidPassword(password);
    }

    [TestCase("", ExpectedResult = false)]
    [TestCase("      ", ExpectedResult = false)] // Только пробелы
    public bool TestIsValidPassword_EmptyAndWhitespace(string password)
    {
        return PasswordValidator.IsValidPassword(password);
    }
}
17.Напишите тестовые случаи для функции, которая принимает температуру в градусах Цельсия и конвертирует её в градусы Фаренгейта по формуле:
F = (C * 9 / 5) + 32.
CSS:	
public class TemperatureConverter
{
    public static double CelsiusToFahrenheit(double celsius)
    {
        return (celsius * 9 / 5) + 32;
    }
}
NUnit:
using NUnit.Framework;

[TestFixture]
public class TemperatureConverterTests
{
    [TestCase(0, ExpectedResult = 32)]
    [TestCase(25, ExpectedResult = 77)]
    [TestCase(100, ExpectedResult = 212)]
    public double TestCelsiusToFahrenheit_PositiveTemperatures(double celsius)
    {
        return TemperatureConverter.CelsiusToFahrenheit(celsius);
    }

    [TestCase(-40, ExpectedResult = -40)]
    [TestCase(-10, ExpectedResult = 14)]
    [TestCase(-273.15, ExpectedResult = -459.67)]
    public double TestCelsiusToFahrenheit_NegativeTemperatures(double celsius)
    {
        return TemperatureConverter.CelsiusToFahrenheit(celsius);
    }

    [TestCase(0.1, ExpectedResult = 32.18)]
    [TestCase(-0.1, ExpectedResult = 31.82)]
    public double TestCelsiusToFahrenheit_ZeroAndNearZero(double celsius)
    {
        return TemperatureConverter.CelsiusToFahrenheit(celsius);
    }

    [TestCase(37.5, ExpectedResult = 99.5)]
    [TestCase(20.3, ExpectedResult = 68.54)]
    public double TestCelsiusToFahrenheit_FloatingPointTemperatures(double celsius)
    {
        return TemperatureConverter.CelsiusToFahrenheit(celsius);
    }
}
18.Протестируйте программу bilet_n методом черного ящика, опишите тестовый сценарий.
CSS:
using System;

public class BiletChecker
{
    public static bool IsLuckyTicket(string ticketNumber)
    {
        // Проверка на корректность входных данных
        if (string.IsNullOrWhiteSpace(ticketNumber) || ticketNumber.Length != 6 || !int.TryParse(ticketNumber, out _))
        {
            throw new ArgumentException("Номер билета должен быть строкой длиной 6 символов, состоящей только из цифр.");
        }

        // Разделение номера билета на две части
        int firstHalfSum = ticketNumber[0] - '0' + ticketNumber[1] - '0' + ticketNumber[2] - '0';
        int secondHalfSum = ticketNumber[3] - '0' + ticketNumber[4] - '0' + ticketNumber[5] - '0';

        return firstHalfSum == secondHalfSum;
    }
}
NUnit:
using NUnit.Framework;

[TestFixture]
public class BiletCheckerTests
{
    [TestCase("123321", ExpectedResult = true)]
    [TestCase("123456", ExpectedResult = false)]
    [TestCase("111111", ExpectedResult = true)]
    public bool TestIsLuckyTicket_ValidTickets(string ticketNumber)
    {
        return BiletChecker.IsLuckyTicket(ticketNumber);
    }

    [Test]
    public void TestIsLuckyTicket_InvalidLength_TooShort()
    {
        Assert.Throws<ArgumentException>(() => BiletChecker.IsLuckyTicket("12345"));
    }

    [Test]
    public void TestIsLuckyTicket_InvalidLength_TooLong()
    {
        Assert.Throws<ArgumentException>(() => BiletChecker.IsLuckyTicket("1234567"));
    }

    [Test]
    public void TestIsLuckyTicket_InvalidCharacters()
    {
        Assert.Throws<ArgumentException>(() => BiletChecker.IsLuckyTicket("12a456"));
    }

    [Test]
    public void TestIsLuckyTicket_EmptyString()
    {
        Assert.Throws<ArgumentException>(() => BiletChecker.IsLuckyTicket(""));
    }
}
19.Протестируйте программу bilet_n методом черного ящика, опишите тестовый сценарий.
CSS:
using System;

public class BiletChecker
{
    public static bool IsLuckyTicket(string ticketNumber)
    {
        // Проверка на корректность входных данных
        if (string.IsNullOrWhiteSpace(ticketNumber) || ticketNumber.Length != 6 || !int.TryParse(ticketNumber, out _))
        {
            throw new ArgumentException("Номер билета должен быть строкой длиной 6 символов, состоящей только из цифр.");
        }

        // Разделение номера билета на две части
        int firstHalfSum = ticketNumber[0] - '0' + ticketNumber[1] - '0' + ticketNumber[2] - '0';
        int secondHalfSum = ticketNumber[3] - '0' + ticketNumber[4] - '0' + ticketNumber[5] - '0';

        return firstHalfSum == secondHalfSum;
    }
}
NUnit:
using NUnit.Framework;

[TestFixture]
public class BiletCheckerTests
{
    [TestCase("123321", ExpectedResult = true)]
    [TestCase("123456", ExpectedResult = false)]
    [TestCase("111111", ExpectedResult = true)]
    public bool TestIsLuckyTicket_ValidTickets(string ticketNumber)
    {
        return BiletChecker.IsLuckyTicket(ticketNumber);
    }

    [Test]
    public void TestIsLuckyTicket_InvalidLength_TooShort()
    {
        Assert.Throws<ArgumentException>(() => BiletChecker.IsLuckyTicket("12345"));
    }

    [Test]
    public void TestIsLuckyTicket_InvalidLength_TooLong()
    {
        Assert.Throws<ArgumentException>(() => BiletChecker.IsLuckyTicket("1234567"));
    }

    [Test]
    public void TestIsLuckyTicket_InvalidCharacters()
    {
        Assert.Throws<ArgumentException>(() => BiletChecker.IsLuckyTicket("12a456"));
    }

    [Test]
    public void TestIsLuckyTicket_EmptyString()
    {
        Assert.Throws<ArgumentException>(() => BiletChecker.IsLuckyTicket(""));
    }
}
20.Протестируйте программу bilet_n методом черного ящика, опишите тестовый сценарий.
Смотри 19-й вопрос
21. Протестируйте программу bilet_n методом черного ящика, опишите тестовый сценарий.
Смотри 19-й вопрос
22. Протестируйте программу bilet_n методом черного ящика, опишите тестовый сценарий.
Смотри 19-й вопрос
23. Cоздайте приложение графический калькулятор на языке C# с помощью Windows Forms.
Forms:
public partial class Form1 : Form
{
    private double firstValue;
    private string operation;

    public Form1()
    {
        InitializeComponent();
    }

    private void btn0_Click(object sender, EventArgs e) => AppendToDisplay("0");
    private void btn1_Click(object sender, EventArgs e) => AppendToDisplay("1");
    private void btn2_Click(object sender, EventArgs e) => AppendToDisplay("2");
    private void btn3_Click(object sender, EventArgs e) => AppendToDisplay("3");
    private void btn4_Click(object sender, EventArgs e) => AppendToDisplay("4");
    private void btn5_Click(object sender, EventArgs e) => AppendToDisplay("5");
    private void btn6_Click(object sender, EventArgs e) => AppendToDisplay("6");
    private void btn7_Click(object sender, EventArgs e) => AppendToDisplay("7");
    private void btn8_Click(object sender, EventArgs e) => AppendToDisplay("8");
    private void btn9_Click(object sender, EventArgs e) => AppendToDisplay("9");

    private void btnAdd_Click(object sender, EventArgs e)
    {
        firstValue = Convert.ToDouble(textBox1.Text);
        operation = "+";
        textBox1.Text = "0";
    }

    private void btnSubtract_Click(object sender, EventArgs e)
    {
        firstValue = Convert.ToDouble(textBox1.Text);
        operation = "-";
        textBox1.Text = "0";
    }

    private void btnMultiply_Click(object sender, EventArgs e)
    {
        firstValue = Convert.ToDouble(textBox1.Text);
        operation = "*";
        textBox1.Text = "0";
    }

    private void btnDivide_Click(object sender, EventArgs e)
    {
        firstValue = Convert.ToDouble(textBox1.Text);
        operation = "/";
        textBox1.Text = "0";
    }

    private void btnEqual_Click(object sender, EventArgs e)
    {
        double secondValue = Convert.ToDouble(textBox1.Text);
        double result;

        switch (operation)
        {
            case "+":
                result = firstValue + secondValue;
                break;
            case "-":
                result = firstValue - secondValue;
                break;
            case "*":
                result = firstValue * secondValue;
                break;
            case "/":
                if (secondValue == 0)
                {
                    textBox1.Text = "Ошибка";
                    return;
                }
                result = firstValue / secondValue;
                break;
            default:
                return;
        }

        textBox1.Text = result.ToString();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        textBox1.Text = "0";
        firstValue = 0;
        operation = string.Empty;
    }

    private void AppendToDisplay(string number)
    {
        if (textBox1.Text == "0")
            textBox1.Text = number;
        else
            textBox1.Text += number;
    }
}
24.Cоздайте приложение графический редактор на языке C# с помощью Windows Forms.
Forms:
using System;
using System.Drawing;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private Color currentColor = Color.Black;
    private bool isDrawing = false;
    private Point lastPoint;

    public Form1()
    {
        InitializeComponent();
        drawingPanel.MouseDown += DrawingPanel_MouseDown;
        drawingPanel.MouseMove += DrawingPanel_MouseMove;
        drawingPanel.MouseUp += DrawingPanel_MouseUp;
        btnColor.Click += BtnColor_Click;
        btnClear.Click += BtnClear_Click;
    }

    private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }
    }

    private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDrawing)
        {
            using (Graphics g = drawingPanel.CreateGraphics())
            {
                g.DrawLine(new Pen(currentColor, 2), lastPoint, e.Location);
            }
            lastPoint = e.Location;
        }
    }

    private void DrawingPanel_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDrawing = false;
        }
    }

    private void BtnColor_Click(object sender, EventArgs e)
    {
        using (ColorDialog colorDialog = new ColorDialog())
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
            }
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        drawingPanel.Invalidate(); // Очищает область рисования
    }
}
25.Cоздайте приложение для рисования изображений на языке C# с помощью Windows Forms.
Forms:
using System;
using System.Drawing;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private Image loadedImage; // Загруженное изображение
    private Color currentColor = Color.Black; // Текущий цвет кисти
    private bool isDrawing = false; // Флаг рисования
    private Point lastPoint; // Последняя точка для рисования

    public Form1()
    {
        InitializeComponent();

        // Обработчики событий
        drawingPanel.MouseDown += DrawingPanel_MouseDown;
        drawingPanel.MouseMove += DrawingPanel_MouseMove;
        drawingPanel.MouseUp += DrawingPanel_MouseUp;
        btnLoadImage.Click += BtnLoadImage_Click;
        btnClear.Click += BtnClear_Click;
        btnColor.Click += BtnColor_Click;
    }

    private void BtnLoadImage_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                loadedImage = Image.FromFile(openFileDialog.FileName);
                drawingPanel.Invalidate(); // Перерисовываем панель
            }
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        loadedImage = null; // Удаляем изображение
        drawingPanel.Invalidate(); // Очищаем панель
    }

    private void BtnColor_Click(object sender, EventArgs e)
    {
        using (ColorDialog colorDialog = new ColorDialog())
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color; // Устанавливаем выбранный цвет
            }
        }
    }

    private void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }
    }

    private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDrawing)
        {
            using (Graphics g = drawingPanel.CreateGraphics())
            {
                Pen pen = new Pen(currentColor, 2);
                g.DrawLine(pen, lastPoint, e.Location); // Рисуем линию между точками
                lastPoint = e.Location;
            }
        }
    }

    private void DrawingPanel_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDrawing = false;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (loadedImage != null)
        {
            Graphics g = drawingPanel.CreateGraphics();
            g.DrawImage(loadedImage, new Rectangle(0, 0, drawingPanel.Width, drawingPanel.Height));
        }
    }
}
26.Cоздайте приложение текстовый редактор, применяя технологию разработки многооконного приложения на языке C# с помощью Windows Forms.
Forms:
using System;
using System.IO;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files|*.txt|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files|*.txt|All Files|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, textBox1.Text);
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, Clipboard.GetText());
                textBox1.SelectionStart = selectionStart + Clipboard.GetText().Length;
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
            int selectionStart = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Remove(selectionStart, textBox1.SelectionLength);
            textBox1.SelectionStart = selectionStart;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Простой текстовый редактор на C# Windows Forms", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
27.Cоздайте приложение форма авторизации через логин и пароль, на языке C# с помощью Windows Forms.
Forms:
using System;
using System.Windows.Forms;

namespace LoginFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Простейшая проверка логина и пароля
            if (username == "admin" && password == "password")
            {
                lblMessage.Text = "Успешный вход!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Неверный логин или пароль.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
28.Cоздайте приложение для генерации паролей на языке C# с помощью Windows Forms.
Forms:
using System;
using System.Text;
using System.Windows.Forms;

namespace PasswordGenerator
{
    public partial class Form1 : Form
    {
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int length = (int)numericUpDownLength.Value;
            bool includeUppercase = chkUppercase.Checked;
            bool includeNumbers = chkNumbers.Checked;
            bool includeSpecialChars = chkSpecialChars.Checked;

            txtPassword.Text = GeneratePassword(length, includeUppercase, includeNumbers, includeSpecialChars);
        }

        private string GeneratePassword(int length, bool includeUppercase, bool includeNumbers, bool includeSpecialChars)
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numberChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+[]{}|;:,.<>?";

            StringBuilder characterSet = new StringBuilder(lowercaseChars);

            if (includeUppercase)
                characterSet.Append(uppercaseChars);
            if (includeNumbers)
                characterSet.Append(numberChars);
            if (includeSpecialChars)
                characterSet.Append(specialChars);

            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = characterSet[random.Next(characterSet.Length)];
            }

            return new string(password);
        }
    }
}
29.Cоздайте приложение для создания базы данных имен и фамилий на языке C# с помощью Windows Forms.
Установить пакет NuGet под названием SQLite
Можно использовать команду диспетчера пакетов: Install - Package System.Data.SQLite
Forms:
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace NameDatabaseApp
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source=names.db;Version=3;";

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Names (Id INTEGER PRIMARY KEY AUTOINCREMENT, FirstName TEXT NOT NULL, LastName TEXT NOT NULL)";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Пожалуйста, введите имя и фамилию.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Names (FirstName, LastName) VALUES (@FirstName, @LastName)";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.ExecuteNonQuery();
                }
            }

            txtFirstName.Clear();
            txtLastName.Clear();
            ShowNames();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxNames.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedItem = listBoxNames.SelectedItem.ToString();
            string[] parts = selectedItem.Split(' ');
            string firstName = parts[0];
            string lastName = parts[1];

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Names WHERE FirstName = @FirstName AND LastName = @LastName";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.ExecuteNonQuery();
                }
            }

            ShowNames();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowNames();
        }

        private void ShowNames()
        {
            listBoxNames.Items.Clear();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string selectQuery = "SELECT FirstName, LastName FROM Names";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBoxNames.Items.Add($"{reader["FirstName"]} {reader["LastName"]}");
                    }
                }
            }
        }
    }
}
30.Cоздайте прототип приложения по обмену валюты на языке C# с помощью Windows Forms.
Forms:
using System;
using System.Windows.Forms;

namespace CurrencyExchangeApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadCurrencies();
        }

        private void LoadCurrencies()
        {
            cmbFromCurrency.Items.AddRange(new string[] { "USD", "EUR", "RUB" });
            cmbToCurrency.Items.AddRange(new string[] { "USD", "EUR", "RUB" });
            cmbFromCurrency.SelectedIndex = 0; // USD по умолчанию
            cmbToCurrency.SelectedIndex = 1; // EUR по умолчанию
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtAmount.Text, out double amount))
            {
                double result = ExchangeCurrency(amount, cmbFromCurrency.Text, cmbToCurrency.Text);
                txtResult.Text = result.ToString("F2");
            }
            else
            {
                MessageBox.Show("Введите корректную сумму!", "Ошибка");
            }
        }

        private double ExchangeCurrency(double amount, string fromCurrency, string toCurrency)
        {
            // Фиксированные курсы валют
            double usdToEur = 0.85, usdToRub = 75;
            double eurToUsd = 1 / usdToEur, eurToRub = usdToRub * usdToEur;
            double rubToUsd = 1 / usdToRub, rubToEur = 1 / eurToRub;

            if (fromCurrency == toCurrency) return amount;

            return fromCurrency switch
            {
                "USD" => toCurrency == "EUR" ? amount * usdToEur : amount * usdToRub,
                "EUR" => toCurrency == "USD" ? amount * eurToUsd : amount * eurToRub,
                "RUB" => toCurrency == "USD" ? amount * rubToUsd : amount * rubToEur,
                _ => 0,
            };
        }
    }
}


