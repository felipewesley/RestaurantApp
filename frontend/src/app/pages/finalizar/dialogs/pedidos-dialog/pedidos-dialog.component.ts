import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';

@Component({
  selector: 'app-pedidos-dialog',
  templateUrl: './pedidos-dialog.component.html',
  styleUrls: ['./pedidos-dialog.component.scss']
})
export class PedidosDialogComponent implements OnInit {

  displayedColumns: string[] = ['pedidoId', 'produto', 'valor', 'quant', 'status'];
  dataSource: PedidoListaModel[];

  constructor (
    private dialogRef: MatDialogRef<PedidosDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PedidoListaModel[]
  ) {
    
    this.dataSource = data;
  }

  ngOnInit() { }

  onCancel() {
    this.dialogRef.close();
  }

}
