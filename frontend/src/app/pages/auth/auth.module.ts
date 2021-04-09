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
import { AuthRoutingModule } from './auth-routing.module';
import { AuthPageComponent } from './auth-page/auth-page.component';
import { NovaComandaFormComponent } from './auth-page/nova-comanda-form/nova-comanda-form.component';
import { RetomarComandaFormComponent } from './auth-page/retomar-comanda-form/retomar-comanda-form.component';

@NgModule({
  declarations: [
    AuthPageComponent,
    NovaComandaFormComponent,
    RetomarComandaFormComponent,
  ],
  imports: [
    AuthRoutingModule,
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
    AuthPageComponent
  ]
})
export class AuthModule { }
