import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, map } from 'rxjs/operators';

import { routes } from 'src/app/consts/routes';
import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { CancelarPedidoDialogComponent } from '../dialogs/cancelar-pedido-dialog/cancelar-pedido-dialog.component';
import { EditarPedidoDialogComponent } from '../dialogs/editar-pedido-dialog/editar-pedido-dialog.component';

import { AuthService } from '../../auth/auth.service';
import { HomeService } from '../home.service';
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
    private homeService: HomeService,
    private authService: AuthService,
    private pedidoService: PedidoService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {

    // Chamar service de pedidos onde existem os observables

    this.pedidoService.pedidos$
    .pipe(
      // map(p => p.statusEnum === StatusPedido.Cancelado)
    )
    .subscribe(pedidos => {

      console.warn('Pedidos obtidos by pedidos$.subscribe()');

      this.pedidos = pedidos.filter(p => p.statusEnum == StatusPedido.EmAndamento);
      this.dataSource = this.pedidos;
    });
  }

  editarPedido(pedido: PedidoListaModel): void {

    // Implementar chamada de dialog
    console.warn('Editar pedido called!');
    this.dialog.open(EditarPedidoDialogComponent, { data: pedido });
  }

  cancelarPedido(pedido: PedidoListaModel): void {

    console.warn('Cancelar pedido called!');
    this.dialog.open(CancelarPedidoDialogComponent, { data: pedido });
  }

  navigateToPedidos(): void {
    
    this.router.navigate([ routes.PEDIDOS ], { relativeTo: this.activeRoute });
  }

  navigateToNovoPedido(): void {

    this.router.navigate([ routes.NOVO_PEDIDO ], { relativeTo: this.activeRoute });
  }
}