import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { animate, state, style, transition, trigger } from '@angular/animations';

import { Subscription } from 'rxjs';

import { PedidoModel } from 'src/app/shared/models/pedido.model';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { PedidoService } from '../novo-pedido/pedido.service';
import { switchMap } from 'rxjs/operators';

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
export class ListaPedidosComponent implements OnInit, OnDestroy {

  pedidosSubscription: Subscription;

  dataSource: PedidoListaModel[] = [];
  columnsToDisplay = ['pedidoId', 'produto', 'quantidade', 'status'];
  // expandedElement: PedidoModel | null;
  expandedElement: PedidoListaModel | null;

  constructor (
    private route: ActivatedRoute,
    private pedidoService: PedidoService
  ) { }

  ngOnInit() {

    this.pedidosSubscription = this.route.params
    .pipe(
      switchMap((params: Params) => {

        const comandaId = +params['comandaId'];
        return this.pedidoService.obterPedidos(comandaId);
      })
    )
    .subscribe(pedidos => {

      this.dataSource = pedidos;
    });
  }

  ngOnDestroy() {

    this.pedidosSubscription.unsubscribe();
  }
}