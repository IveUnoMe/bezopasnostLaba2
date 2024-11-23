using System;
using System.Linq;
using System.IO;

namespace Practica2
{
    internal static class Operator
    {
        internal static void ThreadChoiceShow()
        {
            bool flag = HashChoiceShow();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите принцип работы:");
                Console.WriteLine("1. Однопоточный");
                Console.WriteLine("2. Многопоточный");
                Console.WriteLine("3. Вернуться");
                string choice = Console.ReadLine();

                Console.Clear();
                if (choice == "1")
                {
                    if (flag)
                    {
                        string hashes = HashEnter();
                        if (!string.IsNullOrEmpty(hashes))
                        {
                            int hashCount = 1;
                            foreach (var hash in hashes.Split(' '))
                            {
                                if (IsValidHash(hash))
                                {
                                    string algorithm = DetermineAlgorithm(hash);
                                    if (algorithm != null)
                                    {
                                        Console.WriteLine($"{hashCount}. Хэш: {hash}");
                                        One.BruteHash(hash.ToLower(), algorithm);
                                        hashCount++;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Хэш {hash} имеет неверный формат и будет пропущен.");
                                }
                            }
                        }
                    }
                    else
                    {
                        string path = HashesReadFromFile();
                        if (!string.IsNullOrEmpty(path))
                        {
                            int hashCount = 1;
                            foreach (var hash in ReadHashesFromFile(path))
                            {
                                if (IsValidHash(hash))
                                {
                                    string algorithm = DetermineAlgorithm(hash);
                                    if (algorithm != null)
                                    {
                                        Console.WriteLine($"{hashCount}. Хэш: {hash}");
                                        One.BruteHash(hash.ToLower(), algorithm);
                                        hashCount++;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Хэш {hash} имеет неверный формат и будет пропущен.");
                                }
                            }
                        }
                    }
                }
                else if (choice == "2")
                {
                    int numThreads = GetNumberOfThreads();

                    if (flag)
                    {
                        string hashes = HashEnter();
                        if (!string.IsNullOrEmpty(hashes))
                        {
                            int hashCount = 1;
                            foreach (var hash in hashes.Split(' '))
                            {
                                if (IsValidHash(hash))
                                {
                                    string algorithm = DetermineAlgorithm(hash);
                                    if (algorithm != null)
                                    {
                                        Console.WriteLine($"{hashCount}. Хэш: {hash}");
                                        Many.BruteHash(hash.ToLower(), algorithm, numThreads);
                                        hashCount++;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Хэш {hash} имеет неверный формат и будет пропущен.");
                                }
                            }
                        }
                    }
                    else
                    {
                        string path = HashesReadFromFile();
                        if (!string.IsNullOrEmpty(path))
                        {
                            int hashCount = 1;
                            foreach (var hash in ReadHashesFromFile(path))
                            {
                                if (IsValidHash(hash))
                                {
                                    string algorithm = DetermineAlgorithm(hash);
                                    if (algorithm != null)
                                    {
                                        Console.WriteLine($"{hashCount}. Хэш: {hash}");
                                        Many.BruteHash(hash.ToLower(), algorithm, numThreads);
                                        hashCount++;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Хэш {hash} имеет неверный формат и будет пропущен.");
                                }
                            }
                        }
                    }
                }
                else if (choice == "3")
                {
                    ThreadChoiceShow();
                }
                else
                {
                    Console.WriteLine("Неверное значение");
                }

                Console.WriteLine("Нажмите любую клавишу для повторного выбора");
                Console.ReadKey();
            }
        }

        private static string DetermineAlgorithm(string hash)
        {
            if (hash.Length == 32)
            {
                return "MD5";
            }
            else if (hash.Length == 64)
            {
                return "SHA-256";
            }
            return null;
        }

        private static bool HashChoiceShow()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите способ ввода хэшей:");
                Console.WriteLine("1. Ввести хэши вручную");
                Console.WriteLine("2. Загрузить хэши из файла");
                Console.WriteLine("3. Выход");
                string choice = Console.ReadLine();

                if (choice == "1") return true;
                if (choice == "2") return false;
                if (choice == "3") Environment.Exit(0);

                Console.WriteLine("Неверное значение, попробуйте снова.");
            }
        }

        private static string HashEnter()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите хэши (через пробел):");
                string input = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }

                Console.WriteLine("Ошибка: не введены хэши. Попробуйте снова!");
                Console.WriteLine("Нажмите любую клавишу для повторного ввода.");
                Console.ReadKey();
            }
        }

        private static string[] ReadHashesFromFile(string filePath)
        {
            try
            {
                var content = File.ReadAllText(filePath).Trim();
                var hashes = content.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                return hashes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return new string[0];
            }
        }

        private static string HashesReadFromFile()
        {
            Console.Clear();
            Console.WriteLine("Введите путь к файлу с хэшами:");
            string filePath = Console.ReadLine().Trim('"');

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Ошибка: файл не найден. Попробуйте снова.");
                return null;
            }

            return filePath;
        }

        private static int GetNumberOfThreads()
        {
            int numThreads = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите количество потоков (не менее двух):");

                if (int.TryParse(Console.ReadLine(), out numThreads) && numThreads > 1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректное количество потоков, попробуйте снова.");
                }
            }
            return numThreads;
        }

        private static bool IsValidHash(string hash)
        {
            if (string.IsNullOrEmpty(hash)) return false;

            if (hash.Length != 32 && hash.Length != 64) return false;

            if (!hash.All(c => Char.IsLetterOrDigit(c))) return false;

            if (hash.All(c => c == hash[0])) return false;

            if (hash.All(Char.IsDigit)) return false;

            if (hash.All(Char.IsLower) || hash.All(Char.IsUpper)) return false;

            return true;
        }
    }
}








