using DataManipulatorGenerator;
using DataManipulatorDAL;

namespace DataManipulatorConsole.Services
{
    internal class FileDataService
    {
        private  DataDAL DataDAL { get; set; }
        public FileDataService(DataDAL dal)
        {
            DataDAL = dal;
        }

        public FileDataService()
        {
            DataDAL = new DataDAL();
        }

        public async Task ResolveAll(Settings settings)
        {
            // Сгенерировать NumberOfFiles текстовых файлов со следующей структурой, каждый из которых содержит NumberOfLines строк

            await GenerateDataSet(settings.DataPath, settings.NumberOfLines, settings.NumberOfFiles);

            //Реализовать объединение файлов в один. При объединении должна быть возможность
            //удалить из всех файлов строки с заданным сочетанием символов, например, «abc» с выводом
            //информации о количестве удаленных строк
            await CombineDataSet
                                (Directory.GetFiles(settings.DataPath),
                                settings.MergedFilePath,
                                settings.DeletePattern,
                                true);

            //Создать процедуру импорта файлов с таким набором полей в таблицу в СУБД. При импорте
            //должен выводится ход процесса(сколько строк импортировано, сколько осталось)
            await ExportDataSetToDBFromFile(new string[] { settings.DataPath + "\\data1.txt" });

            //Реализовать хранимую процедуру в БД (или скрипт с внешним sql-запросом), который считает
            //сумму всех целых чисел и медиану всех дробных чисел
            await GetStatisticInfo();
        }

        private async Task GenerateDataSet(string DataPath, int numberOfLines, int numberOfFiles)
        {
            if (!Directory.Exists(DataPath))
                Directory.CreateDirectory(DataPath);

            IEnumerable<Task> tasks = Enumerable.Range(0, numberOfFiles)
                .Select(async (number) =>
                {
                    using (StreamWriter sw = File.CreateText($"{DataPath}/data{number}.txt"))
                    for (int i = 0; i < numberOfLines; i++)
                    {
                        await sw.WriteLineAsync(BaseDataGenerator.GenerateData().ToString()).ConfigureAwait(false);

                    }
                });

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private async Task CombineDataSet(IEnumerable<string> files, string mergedFile, string deletePattern, bool isDeletingByPattern)
        {
            files = files.Except(new string[] { mergedFile });
            if (!Directory.Exists(new FileInfo(mergedFile).DirectoryName))
            {
                throw new DirectoryNotFoundException($"Injected directory {mergedFile} not exists.");
            }

            File.WriteAllText(mergedFile, "");
            int deletedLines = 0;

            using StreamWriter sw = new StreamWriter(mergedFile);
            foreach(var file in files.Where(f => File.Exists(f)))
            {
                List<string> lines = (await File.ReadAllLinesAsync(file)).ToList();
                if (isDeletingByPattern)
                {
                    int linesCount = lines.Count;
                    lines = lines.Where(l => !l.Contains(deletePattern)).ToList();
                    deletedLines += linesCount - lines.Count;
                }                   

                await sw.WriteLineAsync(String.Join("\n", lines)).ConfigureAwait(false);
            }

            Console.WriteLine($"Deleted {deletedLines} lines.");
        }

        private async Task GetStatisticInfo()
        {
            Console.WriteLine($"Median of all floating point numberas is {await DataDAL.GetMedianOfDouble()}\n" +
                $"Sum of all integers is {await DataDAL.GetSumIntegrs()}");
        }

        private async Task ExportDataSetToDBFromFile(IEnumerable<string> files)
        {
            foreach (string file in files)
            {
                if (!File.Exists(file))
                {
                    throw new ArgumentException("File not exit", nameof(file));
                }

                DataParser parser = new DataParser("||");
                string[] lines = File.ReadAllLines(file);
                for (int i = 0; i < lines.Length; i++)
                {
                    try
                    {
                        await DataDAL.Create(parser.ParseString(lines[i]));
                    }
                    catch (Exception ex) when (ex is FormatException ||
                               ex is IndexOutOfRangeException ||
                               ex is OverflowException)
                    {
                        Console.WriteLine($"Line {i} in file {file} was in incorrect foramt.");
                        continue;
                    }
                    Console.WriteLine($"Imported {i + 1} lines, line left - {lines.Length - i - 1} from {file}");
                }
            }
        }
    }
}
