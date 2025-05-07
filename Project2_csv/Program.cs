/*
* Дисциплина: "Программирование на C#"
* Группа: БПИ-245
* Студент: Панин Михаил Павлович
* Дата: 22.11.2024
* Вариант: 9
*/
using FileControllerLib;
using System.Security;
using ExamHandler;
using DataProcessor;

namespace Project2_csv
{
    internal class Program
    {
        /// <summary>
        /// Метод отображения меню.
        /// </summary>
        /// <param name="dataIsLoad">Загруженны ли какие-дибо даннные.</param>
        public static void ShowMenu(bool dataIsLoad = false)
        {
            Console.WriteLine();
            if (!dataIsLoad)
            {
                Console.WriteLine("1. Ввести адрес файла для загрузки данных");
            }
            else
            {
                Console.WriteLine("1. Заменить набор данных");
            }
            Console.WriteLine("2. Вывести студентов с уровнем образования родителей 'high school' или 'some college'");
            Console.WriteLine("3. Вывести студентов по типу lunch и сохранить их в файл");
            Console.WriteLine("4. Вывести сводную статистику");
            Console.WriteLine("5. Завершить работу программы");
            Console.WriteLine("6. [Доп. задача] Вывести и сохранить студентов, отсортированных по типу теста и группе");
            Console.WriteLine("7. [Доп. задача] Вывести студентов с завершённым курсом и баллами выше средней по экзаменам");
            Console.WriteLine("Введите номер операции:");
        }

        /// <summary>
        /// Главный метод из которого выполняется запуск и управление программы.
        /// </summary>
        public static void Main()
        {
            Examinee[] students = [];
            FileController flc = new();

            while (true)
            {
                try
                {
                    ShowMenu(students.Length > 0);
                    string input = Console.ReadLine()!;

                    if (students.Length == 0 && "234567".Contains(input))
                    {
                        Console.WriteLine("Сначала нужно загрузить файл с данными");
                        Console.WriteLine("C помощью пункта 1 укажите путь до файла с данными");
                    }
                    else
                    {
                        switch (input)
                        {
                            case "1":
                                Console.Write("Введите путь к входному файлу: ");
                                flc.InputFilePath = Console.ReadLine()!;
                                students = Data.LoadData(flc.InputFilePath);
                                break;

                            case "2":
                                Data.PrintStudentWithEducation(students);
                                break;

                            case "3":
                                Data.SaveStudentOfLunch(students);
                                break;

                            case "4":
                                Data.StatisticOfStudents(students);
                                break;

                            case "5":
                                Console.WriteLine("Завершаем программу.");
                                Environment.Exit(0);
                                break;

                            case "6":
                                Data.SortStudents(students);
                                break;

                            case "7":
                                Data.FilterOfStudents(students);
                                break;

                            default:
                                Console.WriteLine("Некорректный ввод.\n");
                                break;
                        }
                    }
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine($"Ошибка: Параметр не может быть null. {ex.Message}\n");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}\n");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Ошибка ввода/вывода: {ex.Message}\n");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Ошибка формата: {ex.Message}\n");
                }
                catch (SecurityException ex)
                {
                    Console.WriteLine($"Ошибка безопасности: {ex.Message}\n");
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine($"Недостаточно памяти: {ex.Message}\n");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Ошибка доступа: {ex.Message}\n");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Ошибка операции: {ex.Message}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}\n");
                }
            }
        }
    }
}
