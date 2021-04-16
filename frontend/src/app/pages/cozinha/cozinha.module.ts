import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';

import { CozinhaComponent } from './cozinha.component';
import { CozinhaRoutingModule } from './cozinha-routing.module';
import { PedidoContainerComponent } from './pedido-container/pedido-container.component';
import { MatButtonModule } from '@angular/material/button';
import { SharedModule } from 'src/app/shared/shared.module';
import { EntregarPedidoDialogComponent } from './dialogs/entregar-pedido-dialog/entregar-pedido-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    CozinhaRoutingModule,
    MatGridListModule,
    MatCardModule,
    MatButtonModule,
    SharedModule,
  ],
  declarations: [
    CozinhaComponent,
    PedidoContainerComponent,
    EntregarPedidoDialogComponent
  ],
  exports: [
    RouterModule
  ]
})
export class CozinhaModule { }
