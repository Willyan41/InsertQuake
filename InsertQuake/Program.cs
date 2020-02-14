using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace InsertQuake
{
    class Program
    {

        //static void Main(string[] args)
        //{

        //    var linha = "";

        //    System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\willy\Desktop\teste\Armas.txt");

        //    while ((linha = file.ReadLine()) != null)
        //    {

        //        var armas = linha.Split(';');
        //        var insert = $"INSERT INTO BS_002_ARMAS (NOME_ARMA) VALUES ('{armas[0]}');";

        //        Console.WriteLine(insert);

        //    }

        //    file.Close();
        //    Console.ReadLine();


        //}
        //}
        //}

        static void Main(string[] args)
        {
            int partida = 0;
            int tipo = 0;
            var linha = "";
            int id = 0;
            int posicao;
            



            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\willy\Desktop\teste\LogQuake.txt");

            while ((linha = file.ReadLine()) != null)
            {
                //tipo partida + numero partida
                while ((linha = file.ReadLine()) != null && linha.Contains("InitGame"))
                {

                    //0:00 InitGame: \sv_floodProtect\1\sv_maxPing\0\sv_minPing\0\sv_maxRate\10000\sv_minRate\0\sv_hostname\Code Miner Server\g_gametype\0\sv_privateClients\2\sv_maxclients\16\sv_allowDownload\0\dmflags\0\fraglimit\20\timelimit\15\g_maxGameClients\0\capturelimit\8\version\ioq3 1.36 linux - x86_64 Apr 12 2009\protocol\68\mapname\q3dm17\gamename\baseq3\g_needpass\0

                    Regex rx = new Regex(@".*g_gametype\\(\d+)\\");
                    MatchCollection matches = rx.Matches(linha);

                    foreach (Match match in matches)
                    {

                        GroupCollection groups = match.Groups;
                        int teste = 1 + (int.Parse(groups[1].Value));
                        Console.WriteLine("INSERT INTO BS_004_PARTIDA (TIPO) VALUES ('{0}'); ",
                                          (int.Parse(groups[1].Value) + 1));

                    }

                }
                if ((linha = file.ReadLine()) != null && linha.Contains("ShutdownGame:"))
                {
                    partida++;
                    //Console.WriteLine("ESTA É A PARTIDA {0}", partida);
                }

                //MORTES
                while ((linha = file.ReadLine()) != null && linha.Contains("Kill"))
                {
                    Regex rx = new Regex(@"\d+\:\d+ Kill: (\d+) (\d+) (\d+)");

                    MatchCollection matches = rx.Matches(linha);


                    foreach (Match match in matches)
                    {
                        GroupCollection groups = match.Groups;
                        Console.WriteLine("INSERT INTO BS_006_MORTES (ID_CLIENTE_MATOU, ID_CLIENTE_MORTO, ID_ARMA, ID_PARTIDA) VALUES ('{0}', '{1}', '{2}', '{3}');",
                                          groups[1].Value,
                                          groups[2].Value,
                                          groups[3].Value,
                                            partida);
                    }
                }


                //cliente


                while ((linha = file.ReadLine()) != null && linha.Contains("ClientUserinfoChanged"))
                {

                    //20:34 ClientUserinfoChanged: 2 n\Isgalamido\t\0\model\xian/default\hmodel\xian/default\g_redteam\\g_blueteam\\c1\4\c2\5\hc\100\w\0\l\0\tt\0\tl\0
                    Regex rx = new Regex(@".*ClientUserinfoChanged:\s(\d+)\s\w\\(\w+)\\\w\\\w\\\w+\\(\w+)");

                    MatchCollection matches = rx.Matches(linha);


                    foreach (Match match in matches)
                    {
                        GroupCollection groups = match.Groups;
                        Console.WriteLine("INSERT INTO BS_001_CLIENTE (ID_POSICAO, NICKNAME, PERSONAGEM ) VALUES ('{0}', '{1}', '{2}');",
                                          groups[1].Value,
                                          groups[2].Value,
                                          groups[3].Value,
                                          partida);
                    }



                }

            }
            file.Close();
            Console.ReadLine();


        }
    }
}




