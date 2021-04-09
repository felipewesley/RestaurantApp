import { Component, Input, OnInit } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';

import { ProdutoModel } from 'src/app/shared/models/produto.model';
import { NovoPedidoDialogComponent } from '../dialogs/novo-pedido-dialog/novo-pedido-dialog.component';

@Component({
  selector: 'app-produto-container',
  templateUrl: './produto-container.component.html',
  styleUrls: ['./produto-container.component.scss']
})
export class ProdutoContainerComponent implements OnInit {

  @Input() produto: ProdutoModel = {} as ProdutoModel;

  constructor(private dialog: MatDialog) { }

  ngOnInit() { }

  iniciarDialogPedido(): void {
    
    this.dialog.open(NovoPedidoDialogComponent, { data: {
        produtoModel: this.produto
      }
    });
  }

}
