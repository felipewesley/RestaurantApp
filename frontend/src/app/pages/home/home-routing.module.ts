import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { HomePageComponent } from './home-page/home-page.component';
import { ListaPedidosComponent } from '../lista-pedidos/lista-pedidos.component';
import { NovoPedidoComponent } from '../novo-pedido/novo-pedido.component';
import { FinalizarComponent } from '../finalizar/finalizar.component';

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
            }, {
                path: 'finalizar',
                component: FinalizarComponent
            }, {
                path: 'pedidos',
                children: [
                    {
                        path: '',
                        component: ListaPedidosComponent,
                    }, {
                        path: 'novo',
                        component: NovoPedidoComponent
                    }
                ]
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
