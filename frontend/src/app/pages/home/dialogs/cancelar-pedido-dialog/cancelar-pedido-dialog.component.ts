import { Component, Inject, OnInit } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';

@Component({
  selector: 'app-cancelar-pedido-dialog',
  templateUrl: './cancelar-pedido-dialog.component.html',
  styleUrls: ['./cancelar-pedido-dialog.component.scss']
})
export class CancelarPedidoDialogComponent implements OnInit {

  pedido: PedidoListaModel = {} as PedidoListaModel;

  constructor(
    private dialogRef: MatDialogRef<CancelarPedidoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PedidoListaModel
  ) {
    
    this.pedido = data;
  }

  ngOnInit() { }

  confirmarCancelamento(): void {

    this.dialogRef.close(true);
  }

  onCancel(): void {

    this.dialogRef.close(false);
  }

}
