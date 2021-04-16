import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ListagemCozinhaModel } from '../../models/listagem-cozinha.model';

@Component({
  selector: 'app-entregar-pedido-dialog',
  templateUrl: './entregar-pedido-dialog.component.html',
  styleUrls: ['./entregar-pedido-dialog.component.scss']
})
export class EntregarPedidoDialogComponent implements OnInit {

  pedido: ListagemCozinhaModel = {} as ListagemCozinhaModel;

  constructor (
    private dialogRef: MatDialogRef<EntregarPedidoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: { pedido: ListagemCozinhaModel }
  ) {
    
    this.pedido = data.pedido;
  }

  ngOnInit() { }

  confirmarEntrega(): void {

    this.dialogRef.close(true);
  }

  onCancel(): void {

    this.dialogRef.close(false);
  }

}
