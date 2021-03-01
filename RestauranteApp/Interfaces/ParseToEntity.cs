using System;
using System.Collections.Generic;
using System.Text;

namespace RestauranteApp.Interfaces
{
    interface ParseToEntity<T>
    {
        T ConverterEmEntidade(string dados);

        int ObterEntidadeId(string dados);
    }
}
