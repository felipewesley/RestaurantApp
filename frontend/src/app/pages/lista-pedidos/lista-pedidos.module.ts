import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { MatTableModule } from '@angular/material/table';

import { ListaPedidosComponent } from './lista-pedidos.component';

const routes: Routes = [
  {
    path: '',
    component: ListaPedidosComponent
  }, {
    path: ':pedidoId',
    component: ListaPedidosComponent
  }
];

@NgModule({
  declarations: [
    ListaPedidosComponent,
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MatTableModule,
  ],
  exports: [RouterModule],
  providers: [PreloadAllModules]
})
export class ListaPedidosModule { }
