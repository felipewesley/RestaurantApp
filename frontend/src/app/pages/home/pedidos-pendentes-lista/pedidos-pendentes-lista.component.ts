import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { filter, map, switchMap, take } from 'rxjs/operators';

import { appRoutes } from 'src/app/consts/app-routes.enum';
import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { CancelarPedidoDialogComponent } from '../dialogs/cancelar-pedido-dialog/cancelar-pedido-dialog.component';
import { EditarPedidoDialogComponent } from '../dialogs/editar-pedido-dialog/editar-pedido-dialog.component';

import { PedidoService } from '../../novo-pedido/pedido.service';
import { StdSnackbarService } from 'src/app/shared/ui-elements/std-snackbar/std-snackbar.service';

@Component({
  selector: 'app-pedidos-pendentes-lista',
  templateUrl: './pedidos-pendentes-lista.component.html',
  styleUrls: ['./pedidos-pendentes-lista.component.scss']
})
export class PedidosPendentesListaComponent implements OnInit {

  displayedColumns: string[] = ['actions', 'pedidoId', 'produto', 'quantidade', 'valor', 'status'];
  dataSource: PedidoListaModel[] = [];

  @Input() comandaId: number;

  pedidos: PedidoListaModel[] = [];

  constructor(
    private route: ActivatedRoute,
    private pedidoService: PedidoService,
    private snackBar: StdSnackbarService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {

    this.route.params
      .pipe(
        switchMap((params: Params) => {
          this.comandaId = +params['comandaId'];
          return this.pedidoService.obterPedidos(this.comandaId);
        })
      )
      .subscribe(pedidos => {

        this.dataSource = pedidos.filter(p => p.statusEnum == StatusPedido.EmAndamento);

      });
  }

  editarPedido(pedido: PedidoListaModel): void {

    let dialog = this.dialog.open(EditarPedidoDialogComponent, { data: pedido });

    dialog.afterClosed()
    .pipe(
      take(1),
      filter(r => r.status),
      switchMap(r => {
        return this.pedidoService.editarPedido(pedido.pedidoId, r.model);
      })
    )
    .subscribe(pedidoEditado => {

      this.pedidoService.atualizarPedidos(pedidoEditado);
      this.snackBar.open(`O pedido foi editado. Nova quantidade: ${pedidoEditado.quantidade} un.`, 1000);

    }, error => {

      this.snackBar.open(`Nao foi possivel editar o pedido`, 500);
    });
  }

  cancelarPedido(pedido: PedidoListaModel): void {

    let dialog = this.dialog.open(CancelarPedidoDialogComponent, { data: pedido });

    dialog.afterClosed()
    .pipe(
      take(1),
      filter(r => r),
      switchMap(r => {
        return this.pedidoService.cancelarPedido(pedido.pedidoId);
      })
    )
    .subscribe(pedidoCancelado => {

      this.pedidoService.atualizarPedidos(pedidoCancelado);
      this.snackBar.open(`O pedido foi cancelado`, 500);

    }, error => {

      this.snackBar.open(`Nao foi possivel cancelar o pedido`, 500);
    })
  }

}