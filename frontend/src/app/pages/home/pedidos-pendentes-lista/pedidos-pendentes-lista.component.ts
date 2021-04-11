import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, map } from 'rxjs/operators';

import { appRoutes } from 'src/app/consts/app-routes';
import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { CancelarPedidoDialogComponent } from '../dialogs/cancelar-pedido-dialog/cancelar-pedido-dialog.component';
import { EditarPedidoDialogComponent } from '../dialogs/editar-pedido-dialog/editar-pedido-dialog.component';

import { PedidoService } from '../../novo-pedido/pedido.service';

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
    private pedidoService: PedidoService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {

    this.pedidoService.pedidos$
    .pipe(
      // map(p => p.statusEnum === StatusPedido.Cancelado)
    )
    .subscribe(pedidos => {

      this.pedidos = pedidos.filter(p => p.statusEnum == StatusPedido.EmAndamento);
      this.dataSource = this.pedidos;
    });
  }

  editarPedido(pedido: PedidoListaModel): void {

    this.dialog.open(EditarPedidoDialogComponent, { data: pedido });
  }

  cancelarPedido(pedido: PedidoListaModel): void {

    this.dialog.open(CancelarPedidoDialogComponent, { data: pedido });
  }

  navigateToPedidos(): void {
    
    this.router.navigate([ appRoutes.PEDIDOS ], { relativeTo: this.activeRoute });
  }

  navigateToNovoPedido(): void {

    this.router.navigate([ appRoutes.NOVO_PEDIDO ], { relativeTo: this.activeRoute });
  }
}