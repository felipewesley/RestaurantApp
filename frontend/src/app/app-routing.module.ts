import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { CozinhaLayoutComponent } from './template/cozinha-layout/cozinha-layout.component';
import { LayoutComponent } from './template/layout/layout.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'atendimento'
  }, {
    path: 'atendimento',
    loadChildren: () => import('./pages/atendimento/atendimento.module').then(m => m.AtendimentoModule)
  }, {
    path: 'home',
    component: LayoutComponent,
    loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule)
  }, {
    path: 'cozinha',
    component: CozinhaLayoutComponent,
    loadChildren: () => import('./pages/cozinha/cozinha.module').then(m => m.CozinhaModule)
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
