import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { animate, state, style, transition, trigger } from '@angular/animations';

import { appRoutes } from 'src/app/consts/app-routes';
import { PedidoModel } from 'src/app/shared/models/pedido.model';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { AuthService } from '../auth/auth.service';
import { HomeService } from '../home/home.service';

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

  dataSource: PedidoListaModel[] = [];
  columnsToDisplay = ['pedidoId', 'produto', 'quantidade', 'status'];
  expandedElement: PedidoModel | null;

  currentDate = new Date();

  constructor (
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private homeService: HomeService
  ) { }

  navigateToNovoPedido(): void {

    this.router.navigate([ appRoutes.NOVO_PEDIDO ], { relativeTo: this.route.parent });
  }

  ngOnInit() {

    this.homeService.obterPedidosPendentes(this.authService.comandaId)
      .subscribe(pedidoList => this.dataSource = pedidoList);
  }
}