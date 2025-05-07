namespace FileControllerLib
{
    /// <summary>
    /// Класс для работы с файлами, позволяющий устанавливать пути к входному и выходному файлам.
    /// </summary>
    public class FileController
    {
        /// <summary>
        /// Путь входного файла.
        /// </summary>
        private string _inputFilePath;
        /// <summary>
        /// Путь выходного файла.
        /// </summary>
        private string _outputFilePath;

        /// <summary>
        /// Конструктор с пустыми путями.
        /// </summary>
        public FileController()
        {
            _inputFilePath = string.Empty;
            _outputFilePath = string.Empty;
        }

        /// <summary>
        /// Конструктор с указанным входным путем.
        /// </summary>
        /// <param name="inputFilePath">Путь к входному файлу.</param>
        public FileController(string inputFilePath) : this()
        {
            InputFilePath = inputFilePath;
        }

        /// <summary>
        /// Конструктор с указанными входным и выходным путями.
        /// </summary>
        /// <param name="inputFilePath">Путь к входному файлу.</param>
        /// <param name="outputFilePath">Путь к выходному файлу.</param>
        public FileController(string inputFilePath, string outputFilePath) : this(inputFilePath)
        {
            OutputFilePath = outputFilePath;
        }

        /// <summary>
        /// Получает или задает путь к входному файлу.
        /// </summary>
        /// <value>Путь к входному файлу.</value>
        public string InputFilePath
        {
            get => _inputFilePath;
            set
            {
                // Валидирует путь к файлу.
                ValidateFilePath(value);
                _inputFilePath = value;
            }
        }

        /// <summary>
        /// Получает или задает путь к выходному файлу.
        /// </summary>
        /// <value>Путь к выходному файлу.</value>
        public string OutputFilePath
        {
            get => _outputFilePath;
            set
            {
                ValidateFilePath(value);
                _outputFilePath = value;
            }
        }

        /// <summary>
        /// Валидирует (проверяет) путь к файлу и его расширение.
        /// </summary>
        /// <param name="path">Путь к файлу для проверки.</param>
        /// <param name="isOutPutFile">Является ли файл выходным.</param>
        /// <exception cref="ArgumentNullException">Если путь пуст или равен null.</exception>
        /// <exception cref="ArgumentException">Если путь содержит недопустимые символы, файл не существует или имеет неправильное расширение.</exception>
        public static void ValidateFilePath(string path, bool isOutPutFile = false)
        {
            // Проверка на пустой путь.
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Путь не может быть пустым.");
            }

            // Проверка на наличие недопустимых символов в пути.
            if (path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new ArgumentException("В пути содержатся недопустимые символы.");
            }

            // Проверка, что файл существует.
            if (!File.Exists(path) && isOutPutFile == false)
            {
                throw new ArgumentException("Указанный файл не существует.");
            }

            // Проверка, что файл имеет расширение .csv.
            if (Path.GetExtension(path).ToLower() != ".csv")
            {
                throw new ArgumentException("Файл должен иметь расширение '.csv.' ");
            }
        }
    }
}
