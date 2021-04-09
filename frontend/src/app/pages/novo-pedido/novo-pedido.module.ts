import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule } from '@angular/material/form-field';

import { SharedModule } from 'src/app/shared/shared.module';
import { NovoPedidoComponent } from './novo-pedido.component';
import { ProdutoContainerComponent } from './produto-container/produto-container.component';
import { NovoPedidoDialogComponent } from './dialogs/novo-pedido-dialog/novo-pedido-dialog.component';
import { TipoProdutoContainerComponent } from './tipo-produto-container/tipo-produto-container.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatGridListModule,
    MatChipsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule
  ],
  declarations: [
    NovoPedidoComponent,
    TipoProdutoContainerComponent,
    ProdutoContainerComponent,
    NovoPedidoDialogComponent
  ]
})
export class NovoPedidoModule { }
