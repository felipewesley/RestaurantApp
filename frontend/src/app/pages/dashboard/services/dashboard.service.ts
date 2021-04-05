import { Injectable } from '@angular/core';
import { StatusPedido } from '../consts';

import { PedidoPendente } from '../models/pedido-pendente.model';


@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  public loadPedidosPendentes(): PedidoPendente[] {
    
    return [
      {
        codigo: 12345,
        produto: 'Coca-Cola',
        quantidade: 3,
        valor: 5.99,
        status: StatusPedido.EmAndamento
      }, {
        codigo: 27396,
        produto: 'Sashimi',
        quantidade: 5,
        valor: 0,
        status: StatusPedido.EmAndamento
      }, {
        codigo: 83517,
        produto: 'Yakisoba',
        quantidade: 2,
        valor: 0,
        status: StatusPedido.EmAndamento
      }
    ];
  }
}
