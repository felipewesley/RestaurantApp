import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { PedidoService } from '../../pedido.service';
import { ProdutoModel } from 'src/app/shared/models/produto.model';
import { PedidoFormularioModel } from '../../models/pedido-formulario.model';
import { switchMap, take } from 'rxjs/operators';
import { StdSnackbarService } from 'src/app/shared/ui-elements/std-snackbar/std-snackbar.service';

@Component({
  selector: 'app-novo-pedido-dialog',
  templateUrl: './novo-pedido-dialog.component.html',
  styleUrls: ['./novo-pedido-dialog.component.scss']
})
export class NovoPedidoDialogComponent implements OnInit {

  novoPedidoForm: FormGroup;

  produto: ProdutoModel = {} as ProdutoModel;

  constructor (
    private snackBar: StdSnackbarService,
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
        this.produto.quantidadePermitida > 0 ?
        Validators.max(this.produto.quantidadePermitida) :
        Validators.max(999)
      ])
    });

    this.novoPedidoForm.get('quantidade').valueChanges
    .subscribe(novoValor => {
      if (novoValor > 2) return false;
    });
  }
  
  alterarQuantidade(ref: number): void {

    const novaQuantidade = parseInt(this.novoPedidoForm.get('quantidade').value) + ref;
    
    // if (novaQuantidade > this.novoPedidoForm.get('quantidade'))

    this.novoPedidoForm.get('quantidade').setValue(novaQuantidade);
    console.log(this.novoPedidoForm.get('quantidade').errors);
  }

  onSubmit(): void {
    
    const model: PedidoFormularioModel = {
      produtoId: this.produto.produtoId,
      quantidade: this.novoPedidoForm.get('quantidade').value
    };
    
    this.pedidoService.novoPedido(model)
    .pipe(
      take(1)
    )
    .subscribe(novoPedido => {

      this.snackBar.open('Seu pedido foi salvo com sucesso!');
      this.pedidoService.atualizarPedidos(novoPedido, true);
      this.onCancel();
    });
  }

  onCancel() {
    this.dialogRef.close();
  }
}
