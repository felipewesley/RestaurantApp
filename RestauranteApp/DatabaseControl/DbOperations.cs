using RestauranteApp.Interfaces;
using System.IO;

namespace RestauranteApp.DatabaseControl
{
    abstract class DbOperations
    {
        public static string GetPathOfEntity(Entidade entity)
        {
            // string path = @"C:\workspace\RestauranteApp\RestauranteApp\Dados\LogDados.csv";
            string path = @"C:\workspace\RestauranteApp\RestauranteApp\Dados\";

            switch (entity)
            {
                case Entidade.Comanda:
                    path+= "ComandaDados.csv";
                    break;

                case Entidade.Mesa:
                    path+= "MesaDados.csv";
                    break;

                case Entidade.Pedido:
                    path+= "PedidoDados.csv";
                    break;

                case Entidade.Produto:
                    path+= "ProdutoDados.csv";
                    break;

                case Entidade.Status:
                    path+= "StatusDados.csv";
                    break;

                case Entidade.TipoProduto:
                    path+= "TipoProdutoDados.csv";
                    break;

                default:
                    path+= "LogDados.csv";
                    break;
            }

            return path;
        }

        /*
        public static int GetNextIdDaClasse<T>(Entidade entidade) where T : ParseToEntity<T>
        {
            string path = GetPathOfEntity(entidade);
            
            return T.ObterEntidadeId(File.ReadAllLines(path)[^1]);
        }
        */
        

        /*
        public T Get<T>(int id, Entidade entidade) where T : ParseToEntity<T>
        {

            string path = GetPathOfEntity(entidade);
            string? selectedLine = null;

            using (StreamReader sr = new StreamReader(path))
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
            
            return T.ConverterEmEntidade(selectedLine);
        }
        */
    }
}