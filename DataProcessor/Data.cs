using System;
using System.IO;
using ExamHandler;
using FileControllerLib;

namespace DataProcessor
{
    /// <summary>
    /// Cтатический класс для обработки/сохранения/загрузки данных (обектов Examinee).
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// Метод для вывода студентов с уровнем образования 'high school' или 'some college'
        /// </summary>
        /// <param name="students">Студенты среди которых осуществляется выбор.</param>
        public static void PrintStudentWithEducation(Examinee[] students)
        {
            bool found = false;
            foreach (Examinee student in students)
            {
                // Выводим студентов с parental level of education = high school или some college.
                if (student.ParentalLevelOfEducation is "high school" or "some college")
                {
                    Console.WriteLine(student.BeautifulPrint());
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Не найдено студентов с указанным уровнем образования родителей.");
            }
        }

        /// <summary>
        /// Метод для вывода студентов по типу lunch и сохранения их в файл lunch-type.csv.
        /// </summary>
        /// <param name="students">Студенты среди которых осуществляется выбор.</param>
        public static void SaveStudentOfLunch(Examinee[] students)
        {
            Console.Write("Введите тип lunch (standard или free/reduced): ");
            string lunchType = Console.ReadLine()!;
            int count = 0;
            Examinee[] filteredData = new Examinee[students.Length];

            foreach (Examinee student in students)
            {
                if (student.Lunch.ToLower() == lunchType.ToLower())
                {
                    filteredData[count] = student;
                    count++;
                }
            }

            Array.Resize(ref filteredData, count); // Обрезаем массив до реальной длины.

            if (filteredData.Length > 0)
            {
                foreach (Examinee item in filteredData)
                {
                    Console.WriteLine(item.BeautifulPrint());
                }
                SaveData(filteredData, "lunch-type.csv");
                Console.WriteLine("Данные сохранены в lunch-type.csv");
            }
            else
            {
                Console.WriteLine("Некорректный тип lunch. Выходной файл не был создан.");
            }
        }

        /// <summary>
        /// Метод отображающий статистику по данным о студентах.
        /// </summary>
        /// <param name="students">Данные о студентах.</param>
        public static void StatisticOfStudents(Examinee[] students)
        {
            // Количество студентов по группам race/ethnicity.
            Console.WriteLine("\nКоличество студентов по группам (race/ethnicity):");
            string[] uniqueRaces = new string[students.Length];
            int raceCount = 0;

            foreach (Examinee student in students)
            {
                bool exists = false;
                for (int i = 0; i < raceCount; i++)
                {
                    if (uniqueRaces[i] == student.RaceEthnicity)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    uniqueRaces[raceCount] = student.RaceEthnicity;
                    raceCount++;
                }
            }

            
            Array.Resize(ref uniqueRaces, raceCount); // Обрезаем массив до реальной длины.

            foreach (string race in uniqueRaces)
            {
                int raceCountInGroup = 0;
                foreach (Examinee exam in students)
                {
                    if (exam.RaceEthnicity == race)
                    {
                        raceCountInGroup++;
                    }
                }
                Console.WriteLine($"{race}: {raceCountInGroup}");
            }

            // Студент с максимальным и минимальным баллом.
            Examinee? maxScore = null;
            Examinee? minScore = null;

            foreach (Examinee student in students)
            {
                if (maxScore == null || student.TotalScore() > maxScore.TotalScore())
                {
                    maxScore = student;
                }
                if (minScore == null || student.TotalScore() < minScore.TotalScore())
                {
                    minScore = student;
                }
            }

            Console.WriteLine($"\nСтудент с максимальным баллом: \n{maxScore?.BeautifulPrint()}\n");
            Console.WriteLine($"Студент с минимальным баллом: \n{minScore?.BeautifulPrint()}");

            // Количество мужчин по диапазону баллов по математике.
            Console.WriteLine("\nКоличество мужчин по диапазонам баллов по математике:");

            int[] maleScoreRanges = new int[10]; // Диапазоны от 0 до 100 с шагом 10

            foreach (Examinee student in students)
            {
                if (student.Gender.ToLower() == "male")
                {
                    int numOfGroup = student.MathScore / 10; // Преобразуем в диапазон (например, X0 -> X)
                    if (numOfGroup is >= 0 and < 10)
                    {
                        maleScoreRanges[numOfGroup]++;
                    }
                }
            }

            // Выводим результаты по диапазонам.
            for (int i = 0; i < maleScoreRanges.Length; i++)
            {
                Console.WriteLine($"{i * 10}-{(i + 1) * 10}: {maleScoreRanges[i]} студентов");
            }
        }

        /// <summary>
        /// Метод сортирующий студентов по типу курса подготовки и группе race/ethnicity.
        /// </summary>
        /// <param name="students">Студенты среди которых осуществляется сортировка.</param>
        public static void SortStudents(Examinee[] students)
        {
            Examinee[] sortedStudents = new Examinee[students.Length]; // Массив для хранения отсортированных студентов.
            Array.Copy(students, sortedStudents, students.Length);

            // Сортировка по TestPreparationCourse.
            for (int i = 0; i < sortedStudents.Length - 1; i++)
            {
                for (int j = i + 1; j < sortedStudents.Length; j++)
                {
                    if (string.Compare(sortedStudents[i].TestPreparationCourse, sortedStudents[j].TestPreparationCourse) > 0)
                    {
                        // Обмен значениями
                        (sortedStudents[j], sortedStudents[i]) = (sortedStudents[i], sortedStudents[j]);
                    }
                }
            }

            string currentTestCourse = "";
            int start = 0;  // Начало текущей группы.

            for (int i = 0; i < sortedStudents.Length; i++)
            {
                // Когда встречаем новый курс -> сортируем предыдущую группу.
                if (sortedStudents[i].TestPreparationCourse != currentTestCourse)
                {
                    if (i > start) // Если группа не пустая.
                    {
                        // Сортируем группу по RaceEthnicity
                        for (int j = start; j < i - 1; j++)
                        {
                            for (int k = j + 1; k < i; k++)
                            {
                                if (string.Compare(sortedStudents[j].RaceEthnicity, sortedStudents[k].RaceEthnicity) > 0)
                                {
                                    (sortedStudents[k], sortedStudents[j]) = (sortedStudents[j], sortedStudents[k]);
                                }
                            }
                        }
                    }

                    // Обновляем курс и начинаем новую группу
                    currentTestCourse = sortedStudents[i].TestPreparationCourse;
                    start = i;
                }
            }

            // Cортируем последнюю группу.
            if (sortedStudents.Length > start)
            {
                for (int j = start; j < sortedStudents.Length - 1; j++)
                {
                    for (int k = j + 1; k < sortedStudents.Length; k++)
                    {
                        if (string.Compare(sortedStudents[j].RaceEthnicity, sortedStudents[k].RaceEthnicity) > 0)
                        {
                            (sortedStudents[k], sortedStudents[j]) = (sortedStudents[j], sortedStudents[k]);
                        }
                    }
                }
            }

            Console.WriteLine("Перепорядоченные студенты:");
            foreach (Examinee student in sortedStudents)
            {
                Console.WriteLine(student.BeautifulPrint());
            }

            SaveData(sortedStudents, "Sorted_Students.csv"); // Сохраняем результат в CSV файл.
        }


        /// <summary>
        /// Метод выбирающий студентов по типу курса подготовки и баллам выше среднего.
        /// </summary>
        /// <param name="students">Студенты среди которых осуществляется выбор.</param>
        public static void FilterOfStudents(Examinee[] students)
        {
            double avgMathScore = 0;
            double avgReadingScore = 0;
            double avgWritingScore = 0;

            foreach (Examinee student in students)
            {
                avgMathScore += student.MathScore;
                avgReadingScore += student.ReadingScore;
                avgWritingScore += student.WritingScore;
            }

            avgMathScore /= students.Length;
            avgReadingScore /= students.Length;
            avgWritingScore /= students.Length;

            Examinee[] completedStudents = new Examinee[students.Length];
            int completedCount = 0;

            foreach (Examinee student in students)
            {
                if (student.TestPreparationCourse == "completed" &&
                    student.MathScore > avgMathScore &&
                    student.ReadingScore > avgReadingScore &&
                    student.WritingScore > avgWritingScore)
                {
                    completedStudents[completedCount] = student;
                    completedCount++;
                }
            }

            // Обрезаем массив до реальной длины.
            Array.Resize(ref completedStudents, completedCount);

            Console.WriteLine("Студенты с завершенным курсом подготовки и баллами выше среднего:");
            foreach (Examinee student in completedStudents)
            {
                Console.WriteLine(student.BeautifulPrint());
            }

            // Сохраняем выборку в файл.
            Console.WriteLine("Введите имя файла для сохранения выборки:");
            string filePath = Console.ReadLine()!;
            SaveData(completedStudents, filePath);
        }

        /// <summary>
        /// Метод для сохранения данных в CSV файл.
        /// </summary>
        /// <param name="students">Массив данных студентов для сохранения.</param>
        /// <param name="filePath">Путь до файла сохранения.</param>
        public static void SaveData(Examinee[] students, string filePath)
        {
            try
            {
                FileController.ValidateFilePath(filePath, isOutPutFile: true);

                using StreamWriter writer = new(filePath);
                writer.WriteLine("Gender,Race/Ethnicity,Parental Level of Education,Lunch,Test Preparation Course,Math Score,Reading Score,Writing Score");
                foreach (Examinee student in students)
                {
                    writer.WriteLine(student.ToString());
                }
                Console.WriteLine("Записано в файл " + Path.GetFullPath(filePath)?.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Console.Error.WriteLine("Ошибка: Путь к файлу не может быть null.");
                Console.Error.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine("Ошибка: Некорректный путь к файлу.");
                Console.Error.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine("Ошибка: Нет доступа к файлу или директории.");
                Console.Error.WriteLine(ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine("Ошибка: Директория не найдена.");
                Console.Error.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine("Ошибка при работе с файлом.");
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Неизвестная ошибка.");
                Console.Error.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Метод загрузка данных из файла. 
        /// </summary>
        /// <param name="filePath">Путь до входного файла.</param>
        /// <returns>Массив полученных данных студентов.</returns>
        public static Examinee[] LoadData(string filePath)
        {
            try
            {
                // Чтение файла и получение всех строк.
                FileController.ValidateFilePath(filePath);
                string[] lines = File.ReadAllLines(filePath);
                Examinee[] students = new Examinee[lines.Length - 1]; // Минус 1 т.к. первая строка - заголовок.
                int realIndex = 0;
                // Пропускаем первую строку - заголовок.
                for (int i = 1; i < lines.Length; i++)
                {
                    try
                    {
                        Examinee student = new(lines[i]);
                        // Добавляем студента в массив.
                        students[realIndex] = student; // Минус 1 т.к. первая строка - заголовок.
                        realIndex++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"В строке {i} некорректные значения: {ex.Message}");
                    }
                }
                Array.Resize(ref students, realIndex);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Данные загруженны");
                Console.ForegroundColor = ConsoleColor.White;
                return students;
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine("Файл не найден. Пожалуйста, проверьте путь.");
                return [];
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine($"Ошибка доступа: {ex.Message}");
                return [];
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"Директория не найдена: {ex.Message}");
                return [];
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return [];
            }
            catch (ArgumentNullException ex)
            {
                Console.Error.WriteLine($"Ошибка: Путь к файлу не может быть null. {ex.Message}");
                return [];
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Неизвестная ошибка: {ex.Message}");
                return [];
            }
        }
    }
}
