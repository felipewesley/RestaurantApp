import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';

import { SharedModule } from 'src/app/shared/shared.module';
import { NovoPedidoComponent } from './novo-pedido.component';
import { TipoProdutoContainerComponent } from './tipo-produto-container/tipo-produto-container.component';
import { ProdutoContainerComponent } from './produto-container/produto-container.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatGridListModule,
    MatChipsModule,
    MatButtonModule
  ],
  declarations: [
    NovoPedidoComponent,
    TipoProdutoContainerComponent,
    ProdutoContainerComponent
  ]
})
export class NovoPedidoModule { }
