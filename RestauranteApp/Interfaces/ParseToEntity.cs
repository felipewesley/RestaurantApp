using System;

namespace RestauranteApp.Interfaces
{
    interface ParseToEntity<T>
    {
        T ConverterEmEntidade(string dados);

        int ObterEntidadeId(string dados);
    }
}
