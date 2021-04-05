import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { routes } from 'src/app/consts';
import { StatusPedido } from '../../consts';

import { PedidoPendente } from '../../models/pedido-pendente.model';

const ELEMENT_DATA: PedidoPendente[] = [
  {
    codigo: 12345,
    produto: 'Sashimi',
    quantidade: 3,
    status: StatusPedido.EmAndamento,
    valor: 0.0,
    actions: null
  }, {
    codigo: 34567,
    produto: 'Coca-Cola',
    quantidade: 2,
    status: StatusPedido.Entregue,
    valor: 9.98,
    actions: true
  }, {
    codigo: 12345,
    produto: 'Sashimi',
    quantidade: 3,
    status: StatusPedido.Cancelado,
    valor: 0.0,
    actions: null
  }, {
    codigo: 34567,
    produto: 'Coca-Cola',
    quantidade: 2,
    status: null,
    valor: 9.98,
    actions: true
  }
];

@Component({
  selector: 'app-pedidos-pendentes-list',
  templateUrl: './pedidos-pendentes-list.component.html',
  styleUrls: ['./pedidos-pendentes-list.component.scss']
})
export class PedidosPendentesListComponent implements OnInit {

  statusPedido: StatusPedido;

  displayedColumns: string[] = ['codigo', 'produto', 'quantidade', 'valor', 'status', 'actions'];
  dataSource = ELEMENT_DATA;

  constructor(private router: Router) { }

  navigateToPedidos(): void {
    
    this.router.navigate([routes.PEDIDOS])
  }

  ngOnInit(): void {
  }

}
