using System;
using System.Text;
using System.Threading.Tasks;

namespace Practica2
{
    internal static class Many
    {
        internal static void BruteHash(string hash, string algorithm, int numThreads)
        {
            DateTime start = DateTime.Now;
            bool flag = false;

            Task[] tasks = new Task[numThreads];

            int length = 26; 
            int chunkSize = length / numThreads; 

            for (int threadId = 0; threadId < numThreads; threadId++)
            {
                int startChar = 97 + (threadId * chunkSize);  
                int endChar = (threadId == numThreads - 1) ? 123 : startChar + chunkSize; 

                tasks[threadId] = Task.Run(() =>
                {
                    byte[] password = new byte[5];

                    for (password[0] = (byte)startChar; password[0] < endChar; password[0]++)
                    {
                        for (password[1] = 97; password[1] < 123; password[1]++)
                        {
                            for (password[2] = 97; password[2] < 123; password[2]++)
                            {
                                for (password[3] = 97; password[3] < 123; password[3]++)
                                {
                                    for (password[4] = 97; password[4] < 123; password[4]++)
                                    {
                                        string passwordString = Encoding.ASCII.GetString(password);
                                        string hashed = Hash.GetStringHash(passwordString, algorithm);

                                        if (hash == hashed)
                                        {
                                            Console.WriteLine($"Найден пароль: {passwordString}");
                                            Console.WriteLine("Время выполнения: " + (DateTime.Now - start));
                                            flag = true; 
                                        }

                                        if (flag)
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
            }

            Task.WhenAll(tasks).Wait();

            if (!flag)
            {
                Console.WriteLine("Пароль не найден.");
            }
        }
    }
}








