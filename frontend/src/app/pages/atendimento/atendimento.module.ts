import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule } from '@angular/material/form-field';

import { SharedModule } from 'src/app/shared/shared.module';
import { AtendimentoRoutingModule } from './atendimento-routing.module';
import { AtendimentoPageComponent } from './atendimento-page/atendimento-page.component';
import { NovaComandaFormComponent } from './atendimento-page/nova-comanda-form/nova-comanda-form.component';
import { RetomarComandaFormComponent } from './atendimento-page/retomar-comanda-form/retomar-comanda-form.component';
import { CozinhaLoginComponent } from './atendimento-page/cozinha-login/cozinha-login.component';

@NgModule({
  declarations: [
    AtendimentoPageComponent,
    NovaComandaFormComponent,
    RetomarComandaFormComponent,
    CozinhaLoginComponent
  ],
  imports: [
    AtendimentoRoutingModule,
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    SharedModule,
    MatTabsModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    MatGridListModule,
    MatChipsModule
  ], 
  exports: [
    AtendimentoPageComponent
  ]
})
export class AtendimentoModule { }
