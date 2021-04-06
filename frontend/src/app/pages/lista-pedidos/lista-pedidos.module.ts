import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatTableModule } from '@angular/material/table';

import { ListaPedidosComponent } from './lista-pedidos.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { MatCardModule } from '@angular/material/card';

@NgModule({
  imports: [
    CommonModule,
    MatTableModule,
    BrowserAnimationsModule,
    SharedModule,
    MatCardModule
  ],
  declarations: [ListaPedidosComponent]
})
export class ListaPedidosModule { }