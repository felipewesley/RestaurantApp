import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { HomePageComponent } from './home-page/home-page.component';
import { ListaPedidosComponent } from '../lista-pedidos/lista-pedidos.component';
import { NovoPedidoComponent } from '../novo-pedido/novo-pedido.component';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: '/auth'
    },
    {
        path: ':comandaId',
        children: [
            {
                path: '',
                component: HomePageComponent,
            },{
                path: 'pedidos',
                component: ListaPedidosComponent
            }, {
                path: 'novo',
                component: NovoPedidoComponent
            }
        ]
    }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
