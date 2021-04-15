import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';

import { CozinhaComponent } from './cozinha.component';
import { CozinhaRoutingModule } from './cozinha-routing.module';

@NgModule({
  imports: [
    CommonModule,
    CozinhaRoutingModule,
    MatGridListModule,
    MatCardModule
  ],
  declarations: [
    CozinhaComponent
  ],
  exports: [
    RouterModule
  ]
})
export class CozinhaModule { }
