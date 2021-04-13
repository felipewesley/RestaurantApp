import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { filter, map, switchMap } from 'rxjs/operators';

import { appRoutes } from 'src/app/consts/app-routes.enum';
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
  dataSource: PedidoListaModel[] = [];

  @Input() comandaId: number;

  pedidos: PedidoListaModel[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private pedidoService: PedidoService,
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

    this.dialog.open(EditarPedidoDialogComponent, { data: pedido });
  }

  cancelarPedido(pedido: PedidoListaModel): void {

    this.dialog.open(CancelarPedidoDialogComponent, { data: pedido });
  }

  navigateToPedidos(): void {

    this.router.navigate([appRoutes.PEDIDOS], { relativeTo: this.route });
  }

  navigateToNovoPedido(): void {

    this.router.navigate([appRoutes.NOVO_PEDIDO], { relativeTo: this.route });
  }
}