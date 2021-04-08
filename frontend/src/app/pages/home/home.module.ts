import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { HomeRoutingModule } from './home-routing.module';
import { TemplateModule } from 'src/app/template/template.module';
import { HomePageComponent } from './home-page/home-page.component';
import { PedidosPendentesListaComponent } from './pedidos-pendentes-lista/pedidos-pendentes-lista.component';
import { CardInfoComponent } from './card-info/card-info.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CardMesaComponent } from './home-page/card-mesa/card-mesa.component';
import { CardComandaComponent } from './home-page/card-comanda/card-comanda.component';
import { CancelarPedidoComponent } from './dialogs/cancelar-pedido/cancelar-pedido.component';

@NgModule({
  imports: [
    CommonModule,
    HomeRoutingModule,
    SharedModule,
    TemplateModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatTableModule,
    MatMenuModule,
    MatButtonModule,
    MatDialogModule
  ],
  declarations: [
    HomePageComponent,
    PedidosPendentesListaComponent,
    CardInfoComponent,
    CardMesaComponent,
    CardComandaComponent,
    CancelarPedidoComponent
  ],
  exports: [
    RouterModule
  ]
})
export class HomeModule { }
