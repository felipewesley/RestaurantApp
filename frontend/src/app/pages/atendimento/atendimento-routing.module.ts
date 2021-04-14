import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { AtendimentoPageComponent } from './atendimento-page/atendimento-page.component';

const routes: Routes = [
  {
    path: '',
    component: AtendimentoPageComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})

export class AtendimentoRoutingModule { }
