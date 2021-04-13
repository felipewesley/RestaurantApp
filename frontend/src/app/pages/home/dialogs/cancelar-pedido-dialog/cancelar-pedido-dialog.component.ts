import { Component, Inject, OnInit } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { PedidoService } from 'src/app/pages/novo-pedido/pedido.service';
import { StdSnackbarService } from 'src/app/shared/ui-elements/std-snackbar/std-snackbar.service';

@Component({
  selector: 'app-cancelar-pedido-dialog',
  templateUrl: './cancelar-pedido-dialog.component.html',
  styleUrls: ['./cancelar-pedido-dialog.component.scss']
})
export class CancelarPedidoDialogComponent implements OnInit {

  pedido: PedidoListaModel = {} as PedidoListaModel;

  constructor(
    private snackBar: StdSnackbarService,
    private pedidoService: PedidoService,
    private dialogRef: MatDialogRef<CancelarPedidoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PedidoListaModel
  ) {
    
    this.pedido = data;
  }

  ngOnInit() { }

  confirmarCancelamento(): void {

    const pedidoId = this.pedido.pedidoId;

    this.pedidoService.cancelarPedido(pedidoId)
    .subscribe(pedidoModel => {

      this.snackBar.open('Seu pedido foi cancelado!');
      this.pedidoService.atualizarPedidos(pedidoModel);
      this.onCancel();
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

}
