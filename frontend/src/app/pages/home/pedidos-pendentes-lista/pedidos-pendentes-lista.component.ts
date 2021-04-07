import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { routes } from 'src/app/consts/routes';

import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { PedidoModel } from '../../../shared/models/pedido.model';
import { AuthService } from '../../auth/auth.service';

const ELEMENT_DATA: PedidoModel[] = [
  {
    pedidoId: 12345,
    produto: 'Sashimi',
    quantidade: 3,
    status: StatusPedido.EmAndamento,
    valor: 0.0,
    actions: null
  }, {
    pedidoId: 34567,
    produto: 'Coca-Cola',
    quantidade: 2,
    status: StatusPedido.Entregue,
    valor: 9.98,
    actions: true
  }, {
    pedidoId: 12345,
    produto: 'Sashimi',
    quantidade: 3,
    status: StatusPedido.Cancelado,
    valor: 0.0,
    actions: null
  }, {
    pedidoId: 34567,
    produto: 'Coca-Cola',
    quantidade: 2,
    status: null,
    valor: 9.98,
    actions: true
  }
];

@Component({
  selector: 'app-pedidos-pendentes-lista',
  templateUrl: './pedidos-pendentes-lista.component.html',
  styleUrls: ['./pedidos-pendentes-lista.component.scss']
})
export class PedidosPendentesListaComponent implements OnInit {

  displayedColumns: string[] = ['pedidoId', 'produto', 'quantidade', 'valor', 'status', 'actions'];
  dataSource = ELEMENT_DATA;

  constructor (
    private router: Router,
    private authService: AuthService
  ) { }

  navigateToPedidos(): void {
    
    this.router.navigate([ this.authService.comandaAtiva, routes.PEDIDOS ]);
  }

  navigateToNovoPedido(): void {

    this.router.navigate([ this.authService.comandaAtiva, routes.NOVO_PEDIDO ]);
  }

  ngOnInit() { }

}