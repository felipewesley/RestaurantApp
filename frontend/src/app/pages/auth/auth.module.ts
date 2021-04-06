import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';

import { AuthPageComponent } from './auth-page/auth-page.component';
import { NovaComandaFormComponent } from './auth-page/nova-comanda-form/nova-comanda-form.component';
import { RetomarComandaFormComponent } from './auth-page/retomar-comanda-form/retomar-comanda-form.component';
import { AuthRoutingModule } from './auth-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    AuthPageComponent,
    NovaComandaFormComponent,
    RetomarComandaFormComponent,
  ],
  imports: [
    AuthRoutingModule,
    CommonModule,
    ReactiveFormsModule,
    MatTabsModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    MatGridListModule,
    FormsModule,
    SharedModule
  ], 
  exports: [
    AuthPageComponent
  ]
})
export class AuthModule { }
