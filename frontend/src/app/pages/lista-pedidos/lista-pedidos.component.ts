import { Component, OnInit } from '@angular/core';
import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { PedidoModel } from 'src/app/shared/models/pedido.model';

const ELEMENT_DATA: PedidoModel[] = [
  {
    pedidoId: 123,
    produto: 'Sashimi',
    quantidade: 3,
    dataHora: new Date(),
    valor: 0.0,
    status: StatusPedido.EmAndamento
  }, {
    pedidoId: 456,
    produto: 'Yakisoba',
    quantidade: 2,
    dataHora: new Date(),
    valor: 7.5,
    status: StatusPedido.Cancelado
  }, {
    pedidoId: 789,
    produto: 'Coca-Cola',
    quantidade: 3,
    dataHora: new Date(),
    valor: 14.95,
    status: StatusPedido.Entregue
  }
];

@Component({
  selector: 'app-lista-pedidos',
  templateUrl: './lista-pedidos.component.html',
  styleUrls: ['./lista-pedidos.component.scss']
})
export class ListaPedidosComponent implements OnInit {

  dataSource = ELEMENT_DATA;
  columnsToDisplay = ['pedidoId', 'produto', 'quantidade', 'status'];
  expandedElement: PedidoModel | null;

  constructor() { }

  ngOnInit() { }

}
