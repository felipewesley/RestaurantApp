import { Component, OnInit } from '@angular/core';
import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { PedidoModel } from 'src/app/shared/models/pedido.model';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { routes } from 'src/app/consts/routes';


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
  styleUrls: ['./lista-pedidos.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ])
  ]
})
export class ListaPedidosComponent implements OnInit {

  dataSource = ELEMENT_DATA;
  columnsToDisplay = ['pedidoId', 'produto', 'quantidade', 'status'];
  expandedElement: PedidoModel | null;

  constructor (
    private router: Router,
    private authService: AuthService
  ) { }

  navigateToNovoPedido(): void {

    this.router.navigate([ this.authService.comandaAtiva, routes.NOVO_PEDIDO ]);
  }

  ngOnInit() { }

}