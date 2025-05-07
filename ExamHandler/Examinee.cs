using System;

namespace ExamHandler
{
    /// <summary>
    /// Класс для представления данных из файла.
    /// Описывает характеристику студента и его баллы по экзаменам.
    /// </summary>
    public class Examinee
    {
        public string Gender { get; set; }  // Пол.
        public string RaceEthnicity { get; set; }  // Раса/этническая принадлежность.
        public string ParentalLevelOfEducation { get; set; }  // Уровень образования родителей.
        public string Lunch { get; set; }  // Тип обеда.
        public string TestPreparationCourse { get; set; }  // Курс подготовки к экзаменам.
        public int MathScore { get; set; }  // Балл по математике.
        public int ReadingScore { get; set; }  // Балл по чтению.
        public int WritingScore { get; set; }  // Балл по письму.

        /// <summary>
        /// Конструктор по умолчанию, заполняющий все дефолтными значениями.
        /// </summary>
        public Examinee()
        {
            Gender = string.Empty;
            RaceEthnicity = string.Empty;
            ParentalLevelOfEducation = string.Empty;
            Lunch = string.Empty;
            TestPreparationCourse = string.Empty;
            MathScore = 0;
            ReadingScore = 0;
            WritingScore = 0;
        }

        /// <summary>
        /// Конструктор для формирования объекта по параметрам.
        /// </summary>
        /// <param name="gender">Пол.</param>
        /// <param name="raceEthnicity">Раса/этническая принадлежность.</param>
        /// <param name="parentalLevelOfEducation">Уровень образования родителей.</param>
        /// <param name="lunch">Тип обеда.</param>
        /// <param name="testPreparationCourse">Курс подготовки к экзаменам.</param>
        /// <param name="mathScore">Балл по математике.</param>
        /// <param name="readingScore">Балл по чтению.</param>
        /// <param name="writingScore">Балл по письму.</param>
        public Examinee(string gender, string raceEthnicity, string parentalLevelOfEducation,
                        string lunch, string testPreparationCourse, int mathScore,
                        int readingScore, int writingScore)
        {
            Gender = gender;
            RaceEthnicity = raceEthnicity;
            ParentalLevelOfEducation = parentalLevelOfEducation;
            Lunch = lunch;
            TestPreparationCourse = testPreparationCourse;
            MathScore = mathScore;
            ReadingScore = readingScore;
            WritingScore = writingScore;
        }

        /// <summary>
        /// Конструктор для формирования объекта из строки csv файла.
        /// </summary>
        /// <param name="dataLine">Строка csv файла.</param>
        /// <exception cref="ArgumentException">При некорректных входных данных.</exception>
        public Examinee(string dataLine)
        {
            string[] data = dataLine.Split(',');

            if (data.Length != 8)
            {
                throw new ArgumentException("Некорректный формат данных. Ожидается 8 элементов.");
            }
         
            Gender = data[0].Trim('"');
            RaceEthnicity = data[1].Trim('"');
            ParentalLevelOfEducation = data[2].Trim('"');
            Lunch = data[3].Trim('"');
            TestPreparationCourse = data[4].Trim('"');
            MathScore = int.Parse(data[5].Trim('"'));
            ReadingScore = int.Parse(data[6].Trim('"'));
            WritingScore = int.Parse(data[7].Trim('"'));
        }

        /// <summary>
        /// Метод для вычисления общего балла по всем предметам.
        /// </summary>
        /// <returns>Общий балл по всем предметам.</returns>
        public int TotalScore()
        {
            return MathScore + ReadingScore + WritingScore;
        }

        /// <summary>
        /// Метод для получения строки представления объекта.
        /// </summary>
        /// <returns>Cтроковое представление объекта.</returns>
        public override string ToString()
        {
            return $"{Gender},{RaceEthnicity},{ParentalLevelOfEducation},{Lunch},{TestPreparationCourse},{MathScore},{ReadingScore},{WritingScore}";
        }

        /// <summary>
        /// Красивое строковое представление объекта (для вывода подряд множества объектов в читаемом формате).
        /// </summary>
        /// <returns>Cтроковое представление объекта в читаемом формате.</returns>
        public string BeautifulPrint()
        {
            return Gender.PadRight(10) + RaceEthnicity.PadRight(10) + ParentalLevelOfEducation.PadRight(25) + Lunch.PadRight(17) + TestPreparationCourse.PadRight(15) + MathScore.ToString().PadRight(5) + ReadingScore.ToString().PadRight(5) + WritingScore.ToString().PadRight(5);
        }

        /// <summary>
        /// Статический метод для сравнения студентов по общему баллу.
        /// </summary>
        /// <param name="x">Первый студент.</param>
        /// <param name="y">Второй студент.</param>
        /// <returns></returns>
        public static int CompareByTotalScore(Examinee x, Examinee y)
        {
            return x.TotalScore().CompareTo(y.TotalScore());
        }
    }
}
