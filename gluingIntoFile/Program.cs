namespace gluingIntoFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Если путь к каталогу передаётся аргументом командной строки,
            // то можно использовать args[0]. Иначе задаём вручную.
            // Пример: string directoryPath = args.Length > 0 ? args[0] : @"C:\MyProjects";
            string directoryPath = @"C:\Users\Asus\Desktop\работа\ai-hh\repos\hh-mono-2";

            // Проверяем, что каталог существует
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Указанная папка не найдена: {directoryPath}");
                return;
            }

            // Файл, куда будем склеивать результат
            string outputFile = Path.Combine(directoryPath, "MergedResult.cs");

            // Получаем все .cs-файлы из указанной папки (рекурсивно)
            var csFiles = Directory.GetFiles(directoryPath, "*.cs", SearchOption.AllDirectories);

            using (var writer = new StreamWriter(outputFile, false))
            {
                foreach (var file in csFiles)
                {
                    // Чтобы удобнее ориентироваться, добавим комментарий с названием файла
                    writer.WriteLine($"// ========== BEGIN {file} ========== ");

                    // Считываем текст текущего файла
                    string fileContent = File.ReadAllText(file);
                    writer.WriteLine(fileContent);

                    writer.WriteLine($"// ========== END {file} ========== ");
                    writer.WriteLine(); // пустая строка, чтобы отделять файлы
                }
            }

            Console.WriteLine($"Склеивание завершено. Результат: {outputFile}");
        }
    }
}
