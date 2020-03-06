using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace InsertQuake
{
    public class Program
    {

        static void Main(string[] args)
        {
            int partida = 0;
            int tipo = 0;
            var linha = "";
            int id = 0;
            int posicao;
            var NickName = "";
            var NickName1 = "";
            var Idposicao = "";
            var Idposicao2 = "";

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\willy\Desktop\Teste\QuakeLog.txt");

            while ((linha = file.ReadLine()) != null)
            {
                //tipo partida +numero partida
                if ( linha.Contains("InitGame"))
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
                if (linha.Contains("ShutdownGame:"))
                {
                    partida++;
                    //Console.WriteLine("ESTA É A PARTIDA {0}", partida);
                }

                //MORTES
                if (linha.Contains("Kill: "))
                {
                    Regex rx = new Regex(@"\d+\:\d+\s\w+\:\s\d+\s\d+\s(\d+)\:\s(\w+)\s\w+\s(\w+)\s\w+\s(\w+)");

                    MatchCollection matches = rx.Matches(linha);

                    foreach (Match match in matches)
                    {
                        GroupCollection groups = match.Groups;
                        Console.WriteLine("INSERT INTO BS_007_MORTES2 (CLIENTE_MATOU, CLIENTE_MORTO, ID_ARMA, ID_PARTIDA) VALUES ('{0}', '{1}', '{2}', '{3}');",
                                          groups[2].Value,
                                          groups[3].Value,
                                          groups[1].Value,
                                            partida);
                    }
                }

                ////MUNDO
                if (linha.Contains("<world>"))
                {
                    Regex rx = new Regex(@"\d+\:\d+\s\w+\:\s\d+\s\d+\s(\d+)\:\s\<(\w+)\>\s\w+\s(\w+)\s\w+\s(\w+)");

                    MatchCollection matches = rx.Matches(linha);

                    foreach (Match match in matches)
                    {
                        GroupCollection groups = match.Groups;
                        Console.WriteLine("INSERT INTO BS_007_MORTES2 (CLIENTE_MATOU, CLIENTE_MORTO, ID_ARMA, ID_PARTIDA) VALUES ('{0}', '{1}', '{2}', '{3}');",
                                          groups[2].Value,
                                          groups[3].Value,
                                          groups[1].Value,
                                            partida);
                    }
                }


                //cliente


                if (linha.Contains("ClientUserinfoChanged"))
                {

                    //20:34 ClientUserinfoChanged: 2 n\Isgalamido\t\0\model\xian/default\hmodel\xian/default\g_redteam\\g_blueteam\\c1\4\c2\5\hc\100\w\0\l\0\tt\0\tl\0
                    Regex rx = new Regex(@".*ClientUserinfoChanged:\s(\d+)\s\w\\(\w+)\\\w\\\w\\\w+\\(\w+)");

                    MatchCollection matches = rx.Matches(linha);

                    foreach (Match match in matches)
                    {

                        GroupCollection groups = match.Groups;
                        NickName1 = groups[2].Value;
                        Idposicao2 = groups[1].Value;

                        if (NickName1 != NickName)
                        {
                            Console.WriteLine("INSERT INTO BS_003_CLIENTE (ID_POSICAO, NICKNAME, PERSONAGEM, PARTIDA ) VALUES ('{0}', '{1}', '{2}', {3});",
                                             groups[1].Value,
                                             groups[2].Value,
                                             groups[3].Value,
                                             partida);
                            NickName = groups[2].Value;
                            Idposicao = groups[1].Value;

                        }
                        else
                        {
                            Console.WriteLine("ALTER TABLE INTO BS_003_CLIENTE (ID_POSICAO, NICKNAME, PERSONAGEM, PARTIDA ) VALUES ('{0}', '{1}', '{2}', {3});",
                                             groups[1].Value,
                                             groups[2].Value,
                                             groups[3].Value,
                                             partida);
                            NickName = groups[2].Value;
                            Idposicao = groups[1].Value;

                        }

                    }


                }



            }

            file.Close();
            Console.ReadLine();
        }
    }
}




