import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { routes } from 'src/app/consts/routes';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';

import { HomeService } from '../home.service';

@Component({
  selector: 'app-pedidos-pendentes-lista',
  templateUrl: './pedidos-pendentes-lista.component.html',
  styleUrls: ['./pedidos-pendentes-lista.component.scss']
})
export class PedidosPendentesListaComponent implements OnInit {

  displayedColumns: string[] = ['actions', 'pedidoId', 'produto', 'quantidade', 'valor', 'status'];
  dataSource;

  @Input() comandaId: number;

  pedidos: PedidoListaModel[] = [];

  constructor (
    private router: Router,
    private activeRoute: ActivatedRoute,
    private homeService: HomeService
  ) { }
  
  navigateToPedidos(): void {
    
    this.router.navigate([ routes.PEDIDOS ], { relativeTo: this.activeRoute });
  }

  navigateToNovoPedido(): void {

    this.router.navigate([ routes.NOVO_PEDIDO ], { relativeTo: this.activeRoute });
  }

  ngOnInit() {

    this.homeService.obterPedidosPendentes(this.comandaId)
    .subscribe(pedidoList => {

      console.warn('Pedidos obtidos!');
      console.log('Listagem pedidos:', pedidoList);

      this.pedidos = pedidoList;
      this.dataSource = pedidoList;
    });
    
  }

}