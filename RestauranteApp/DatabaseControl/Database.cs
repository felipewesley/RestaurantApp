using RestauranteApp.Interfaces;
using System.IO;
using System;
using System.Collections.Generic;

namespace RestauranteApp.DatabaseControl
{
    class Database : DbOperations
    {

        public static void Insert<T>(T elemento, Entidade entidade) where T : ParseToCsv
        {
            string path = GetPathOfEntity(entidade);

            try
            {
                using (StreamWriter sw = File.AppendText(path))
                // using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(elemento.Imprimir());
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Não foi possivel salvar os dados! " + e.Message);
            }

            GenerateLog(entidade, TipoOperacao.insert);
        }

        public static string[] Select(Entidade entidade)
        {
            string path = GetPathOfEntity(entidade);

            GenerateLog(Entidade.Comanda, TipoOperacao.select);

            return File.ReadAllLines(path);
        }
        public static string Select(Entidade entidade, int id)
        {

            string path = GetPathOfEntity(entidade);
            string selectedLine = string.Empty;

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] dados = line.Split(',');

                    if (int.Parse(dados[0]) == id)
                    {
                        selectedLine = line;
                        break;
                    }
                }
            }

            GenerateLog(Entidade.Comanda, TipoOperacao.select);

            return selectedLine;
        }
        public static string[] Select(Entidade entidade, int id, TipoOperacao tipoOperacao)
        {

            if (tipoOperacao == TipoOperacao.leituraMuitosRegistros)
            {
                List<string> list = new List<string>();

                string path = GetPathOfEntity(entidade);

                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] dados = line.Split(',');

                        if (int.Parse(dados[0]) == id)
                            list.Add(line);
                    }
                }

                GenerateLog(entidade, TipoOperacao.select, "Leitura de varios registros");

                return list.ToArray();
            }

            GenerateLog(entidade, TipoOperacao.select, "Leitura de varios registros (falhou)");

            return null;
        }

        public static void Update<T>(int id, T elemento, Entidade entidade) where T : ParseToCsv
        {
            string path = GetPathOfEntity(entidade);

            List<string> lines = new List<string>();

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] dados = line.Split(',');

                    if (int.Parse(dados[0]) != id)
                        lines.Add(line);
                }
            }
            using (StreamWriter sw = File.CreateText(path))
                sw.Write(lines);

            Insert(elemento, entidade);

            GenerateLog(entidade, TipoOperacao.update);
        }

        private static void GenerateLog(Entidade entidade, TipoOperacao tipoOperacao)
        {
            string path = DbOperations.GetPathOfEntity(0);

            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    string line = $"{ DateTime.Now },{ tipoOperacao },{ entidade }";
                    sw.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Não foi possivel registrar o log da operação! " + e.Message);
            }
        }
        private static void GenerateLog(Entidade entidade, TipoOperacao tipoOperacao, string aditionalMessage)
        {
            string path = DbOperations.GetPathOfEntity(0);

            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    string line = $"{ DateTime.Now },{ tipoOperacao },{ entidade },{aditionalMessage}";
                    sw.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Não foi possivel registrar o log da operação! " + e.Message);
            }
        }
    }
}
