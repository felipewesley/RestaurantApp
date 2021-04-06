import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'auth'
  }, {
    path: 'auth',
    loadChildren: () => import('./pages/auth/auth.module').then(m => m.AuthModule)
  }, {
    path: 'home',
    loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule)
  }, {
    path: 'pedidos',
    loadChildren: () => import('./pages/lista-pedidos/lista-pedidos.module').then(m => m.ListaPedidosModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      useHash: false,
      preloadingStrategy: PreloadAllModules
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
