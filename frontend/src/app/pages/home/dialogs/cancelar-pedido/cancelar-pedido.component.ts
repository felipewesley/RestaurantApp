import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';

@Component({
  selector: 'app-cancelar-pedido',
  templateUrl: './cancelar-pedido.component.html',
  styleUrls: ['./cancelar-pedido.component.scss']
})
export class CancelarPedidoComponent implements OnInit {

  pedido: PedidoListaModel = {} as PedidoListaModel;

  constructor(
    private dialogRef: MatDialogRef<CancelarPedidoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PedidoListaModel
  ) {
    
    this.pedido = data;
  }

  ngOnInit() { }

  onCancel() {
    this.dialogRef.close();
  }

}
