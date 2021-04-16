import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { PedidoAlterarModel } from 'src/app/pages/novo-pedido/models/pedido-alterar.model';
import { PedidoService } from 'src/app/pages/novo-pedido/pedido.service';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { StdSnackbarService } from 'src/app/shared/ui-elements/std-snackbar/std-snackbar.service';

@Component({
  selector: 'app-editar-pedido-dialog',
  templateUrl: './editar-pedido-dialog.component.html',
  styleUrls: ['./editar-pedido-dialog.component.scss']
})
export class EditarPedidoDialogComponent implements OnInit {

  pedido: PedidoListaModel = {} as PedidoListaModel;

  editarPedidoForm: FormGroup;

  constructor (
    private dialogRef: MatDialogRef<EditarPedidoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PedidoListaModel
  ) {
    
    this.pedido = data;

    this.editarPedidoForm = new FormGroup({
      novaQuantidade: new FormControl(null, [
        Validators.required,
        Validators.min(1),
        this.pedido.produto.quantidadePermitida > 0 ?
          Validators.max(this.pedido.produto.quantidadePermitida) :
          Validators.max(999)
      ])
    });
  }

  ngOnInit() { }

  onSubmit(): void {

    const model: PedidoAlterarModel = {
      novaQuantidade: this.editarPedidoForm.get('novaQuantidade').value
    };

    this.dialogRef.close({
      status: true,
      model: model
    });
  }

  onCancel(): void {
    this.dialogRef.close({status: false});
  }

}
