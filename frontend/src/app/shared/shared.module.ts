import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BadgeComponent } from './ui-elements/badge/badge.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { BackPageButtonComponent } from './ui-elements/back-page-button/back-page-button.component';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    BadgeComponent,
    BackPageButtonComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatSnackBarModule,
    MatIconModule
  ], 
  exports: [
    BadgeComponent,
    BackPageButtonComponent
  ]
})
export class SharedModule { }
