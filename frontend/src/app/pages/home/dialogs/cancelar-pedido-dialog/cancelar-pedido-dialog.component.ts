import { Component, Inject, OnInit } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { PedidoService } from 'src/app/pages/novo-pedido/pedido.service';

@Component({
  selector: 'app-cancelar-pedido-dialog',
  templateUrl: './cancelar-pedido-dialog.component.html',
  styleUrls: ['./cancelar-pedido-dialog.component.scss']
})
export class CancelarPedidoDialogComponent implements OnInit {

  pedido: PedidoListaModel = {} as PedidoListaModel;

  constructor(
    private pedidoService: PedidoService,
    private dialogRef: MatDialogRef<CancelarPedidoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PedidoListaModel
  ) {
    
    this.pedido = data;
  }

  ngOnInit() { }

  confirmarCancelamento(): void {

    this.pedidoService.cancelarPedido(this.pedido.pedidoId);
    this.onCancel();
  }

  onCancel(): void {
    this.dialogRef.close();
  }

}
