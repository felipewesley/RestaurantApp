import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BadgeComponent } from './ui-elements/badge/badge.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    BadgeComponent
  ],
  imports: [
    CommonModule,
    MatSnackBarModule
  ], 
  exports: [
    BadgeComponent
  ]
})
export class SharedModule { }
