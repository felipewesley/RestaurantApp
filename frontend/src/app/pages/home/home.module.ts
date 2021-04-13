import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatChipsModule } from '@angular/material/chips';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';

import { HomeRoutingModule } from './home-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { TemplateModule } from 'src/app/template/template.module';
import { HomePageComponent } from './home-page/home-page.component';
import { CardMesaComponent } from './home-page/card-mesa/card-mesa.component';
import { CardComandaComponent } from './home-page/card-comanda/card-comanda.component';

import { EditarPedidoDialogComponent } from './dialogs/editar-pedido-dialog/editar-pedido-dialog.component';
import { PedidosPendentesListaComponent } from './pedidos-pendentes-lista/pedidos-pendentes-lista.component';
import { CancelarPedidoDialogComponent } from './dialogs/cancelar-pedido-dialog/cancelar-pedido-dialog.component';
import { EncerrarComandaDialogComponent } from './dialogs/encerrar-comanda-dialog/encerrar-comanda-dialog.component';

@NgModule({
  imports: [
    SharedModule,
    CommonModule,
    MatIconModule,
    MatCardModule,
    MatMenuModule,
    TemplateModule,
    MatTableModule,
    MatInputModule,
    MatChipsModule,
    MatButtonModule,
    MatDialogModule,
    MatToolbarModule,
    HomeRoutingModule,
    ReactiveFormsModule,
  ],
  declarations: [
    HomePageComponent,
    CardMesaComponent,
    CardComandaComponent,
    EditarPedidoDialogComponent,
    CancelarPedidoDialogComponent,
    EncerrarComandaDialogComponent,
    PedidosPendentesListaComponent,
  ],
  exports: [
    RouterModule
  ],
  providers: []
})
export class HomeModule { }
