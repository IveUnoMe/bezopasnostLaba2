using System;
using System.Text;

namespace Practica2
{
    internal static class One
    {
        private static readonly char[] Dictionary = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        internal static void BruteHash(string hash, string algorithm)
        {
            DateTime start = DateTime.Now;
            int length = Dictionary.Length;

            for (int ch1 = 0; ch1 < length; ch1++)
            {
                for (int ch2 = 0; ch2 < length; ch2++)
                {
                    for (int ch3 = 0; ch3 < length; ch3++)
                    {
                        for (int ch4 = 0; ch4 < length; ch4++)
                        {
                            for (int ch5 = 0; ch5 < length; ch5++)
                            {
                                string password = $"{Dictionary[ch1]}{Dictionary[ch2]}{Dictionary[ch3]}{Dictionary[ch4]}{Dictionary[ch5]}";
                                string hashed = Hash.GetStringHash(password, algorithm);

                                if (hash == hashed)
                                {
                                    Console.WriteLine($"Найден пароль {password}");
                                    Console.WriteLine("Время выполнения: " + (DateTime.Now - start));
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}