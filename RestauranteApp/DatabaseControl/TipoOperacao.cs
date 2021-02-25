using System;
using System.Collections.Generic;
using System.Text;

namespace RestauranteApp.DatabaseControl
{
    enum TipoOperacao
    {
        insert = 1,
        update = 2,
        delete = 3,
        select = 4,
        leituraMuitosRegistros = 100
    }
}
