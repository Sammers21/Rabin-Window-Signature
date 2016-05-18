using System;
using RabinLib;
using System.Numerics;
using System.Diagnostics;
using System.IO;
namespace Testsomelibs
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    bool rx;
                    do
                    {
                        var sw = new Stopwatch();

                        string rs = "";
                        for (int i = 0; i < 50; i++)
                        {
                            rs += "9";
                        }
                        BigInteger k99 = BigInteger.Parse(rs);

                        sw.Start();
                        BigInteger rnd = Rabin.Rand(k99);

                        rx = Rabin.Miller_Rabin_Test(rnd);

                        sw.Stop();
                        using (FileStream fs = new FileStream("KEYS50.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            if (rx)
                                Console.ForegroundColor = ConsoleColor.Green;
                            else
                                Console.ForegroundColor = ConsoleColor.Red;
                            fs.Seek(fs.Length, SeekOrigin.Begin);
                            Console.WriteLine(sw.Elapsed.TotalSeconds + "\t" + rx);
                            Console.WriteLine("\n\n" + rnd + "\n");
                            using (StreamWriter sww = new StreamWriter(fs))
                            {
                                sww.WriteLine(sw.Elapsed.TotalSeconds + "\t" + rx);
                                sww.WriteLine("\n\n" + rnd + "\n");
                            }
                        }
                        if (rx)
                        {
                            using (FileStream fs = new FileStream("KEYSTRUE50.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                            {

                                fs.Seek(fs.Length, SeekOrigin.Begin);
                                using (StreamWriter sww = new StreamWriter(fs))
                                {
                                    sww.WriteLine("\n" + sw.Elapsed.TotalSeconds + "\t" + rx + "\n");
                                    sww.WriteLine("\n\n" + rnd + "\n");
                                }
                            }
                            if (BigInteger.ModPow(rnd, 1, 8) == 3)
                            {
                                using (FileStream fs = new FileStream("KEYSTRUEMOD350.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                {

                                    fs.Seek(fs.Length, SeekOrigin.Begin);
                                    using (StreamWriter sww = new StreamWriter(fs))
                                    {
                                        sww.WriteLine("\n" + sw.Elapsed.TotalSeconds + "\t" + rx + "\n");
                                        sww.WriteLine("\n\n" + rnd + "\n");
                                    }
                                }
                            }
                            else if (BigInteger.ModPow(rnd, 1, 8) == 7)
                            {
                                using (FileStream fs = new FileStream("KEYSTRUEMOD750.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                                {

                                    fs.Seek(fs.Length, SeekOrigin.Begin);
                                    using (StreamWriter sww = new StreamWriter(fs))
                                    {
                                        sww.WriteLine("\n" + sw.Elapsed.TotalSeconds + "\t" + rx + "\n");
                                        sww.WriteLine("\n\n" + rnd + "\n");
                                    }
                                }
                            }
                        }
                    } while (!rx);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (true);

        }


    }
}
