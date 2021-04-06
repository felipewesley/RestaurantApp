import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';

import { HomeRoutingModule } from './home-routing.module';
import { TemplateModule } from 'src/app/template/template.module';
import { HomePageComponent } from './home-page/home-page.component';
import { PedidosPendentesListaComponent } from './pedidos-pendentes-lista/pedidos-pendentes-lista.component';
import { CardInfoComponent } from './card-info/card-info.component';
import { SharedModule } from 'src/app/shared/shared.module';

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
    MatButtonModule
  ],
  declarations: [
    HomePageComponent,
    PedidosPendentesListaComponent,
    CardInfoComponent
  ],
  exports: [
    RouterModule
  ]
})
export class HomeModule { }
