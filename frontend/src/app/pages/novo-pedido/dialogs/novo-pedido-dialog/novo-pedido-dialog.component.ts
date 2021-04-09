import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { PedidoService } from '../../pedido.service';
import { ProdutoModel } from 'src/app/shared/models/produto.model';
import { PedidoFormularioModel } from '../../models/pedido-formulario.model';

@Component({
  selector: 'app-novo-pedido-dialog',
  templateUrl: './novo-pedido-dialog.component.html',
  styleUrls: ['./novo-pedido-dialog.component.scss']
})
export class NovoPedidoDialogComponent implements OnInit {

  novoPedidoForm: FormGroup;

  produto: ProdutoModel = {} as ProdutoModel;

  constructor (
    private pedidoService: PedidoService,
    private dialogRef: MatDialogRef<NovoPedidoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { produtoModel: ProdutoModel}
  ) {
    
    this.produto = this.data.produtoModel;
  }

  ngOnInit() {
    
    this.novoPedidoForm = new FormGroup({
      quantidade: new FormControl(1, [
        Validators.required,
        Validators.min(1),
        this.produto.quantidadePermitida == 0 ? Validators.max(999):
        Validators.max(this.produto.quantidadePermitida)
      ])
    });
  }
  
  onSubmit(): void {
    
    const model: PedidoFormularioModel = {
      produtoId: this.produto.produtoId,
      quantidade: this.novoPedidoForm.get('quantidade').value
    };
    
    this.dialogRef.afterClosed().subscribe(() => {
  
      this.pedidoService.novoPedido(model);
    });

    this.onCancel();
  }

  onCancel() {
    this.dialogRef.close();
  }
}
