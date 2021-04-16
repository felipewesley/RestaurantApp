import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';
import { filter, switchMap } from 'rxjs/operators';

import { StdSnackbarService } from 'src/app/shared/ui-elements/std-snackbar/std-snackbar.service';
import { CozinhaService } from '../cozinha.service';
import { EntregarPedidoDialogComponent } from '../dialogs/entregar-pedido-dialog/entregar-pedido-dialog.component';
import { ListagemCozinhaModel } from '../models/listagem-cozinha.model';

@Component({
  selector: 'app-pedido-container',
  templateUrl: './pedido-container.component.html',
  styleUrls: ['./pedido-container.component.scss']
})
export class PedidoContainerComponent implements OnInit, OnDestroy {

  @Input() pedido: ListagemCozinhaModel;

  entregarPedidoSubscription: Subscription;

  constructor (
    private dialog: MatDialog,
    private snackBar: StdSnackbarService,
    private service: CozinhaService
  ) { }

  ngOnInit() { }

  entregar(): void {

    let dialog = this.dialog.open(EntregarPedidoDialogComponent, { 
      width: '400px',
      data: {
        pedido: this.pedido
      }
    });

    this.entregarPedidoSubscription = dialog.afterClosed()
    .pipe(
      filter(r => r), 
      switchMap(r => {

        return this.service.entregarPedido(this.pedido.pedidoId);
      })
    )
    .subscribe(mesaId => {

      this.snackBar.open(`O pedido foi entregue a mesa ${mesaId}`, 500);
    }, error => {

      this.snackBar.open('Este pedido nao pode mais ser entregue');
    });
  }

  ngOnDestroy() {

    if (this.entregarPedidoSubscription)
      this.entregarPedidoSubscription.unsubscribe();
  }
}
