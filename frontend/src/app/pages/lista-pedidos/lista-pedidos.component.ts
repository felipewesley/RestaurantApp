import { Component, OnInit } from '@angular/core';
import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { PedidoModel } from 'src/app/shared/models/pedido.model';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { routes } from 'src/app/consts/routes';
import { HomeService } from '../home/home.service';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';

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

  dataSource;
  columnsToDisplay = ['pedidoId', 'produto', 'quantidade', 'status'];
  expandedElement: PedidoModel | null;

  private pedidos: PedidoListaModel[];

  constructor (
    private router: Router,
    private authService: AuthService,
    private homeService: HomeService
  ) { }

  navigateToNovoPedido(): void {

    this.router.navigate([ this.authService.comandaId, routes.NOVO_PEDIDO ]);
  }

  ngOnInit() {

    this.homeService.obterPedidosPendentes(this.authService.comandaId)
    .subscribe(pedidoList => {

      console.warn('Pedidos obtidos!');
      console.log('Listagem pedidos:', pedidoList);

      this.pedidos = pedidoList;
      this.dataSource = pedidoList;
    });
    
  }

}