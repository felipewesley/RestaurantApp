import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { HomePageComponent } from './home-page/home-page.component';
import { LayoutComponent } from 'src/app/template/layout/layout.component';
import { ListaPedidosComponent } from '../lista-pedidos/lista-pedidos.component';
import { NovoPedidoComponent } from '../novo-pedido/novo-pedido.component';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: '/auth'
    }, {
        path: ':id',
        component: LayoutComponent,
        children: [
            {
                path: '',
                pathMatch: 'full',
                redirectTo: 'home'
            }, {
                path: 'home',
                component: HomePageComponent
            }, {
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
