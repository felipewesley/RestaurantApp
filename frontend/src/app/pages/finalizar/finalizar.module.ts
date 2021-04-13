import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCheckboxModule } from '@angular/material/checkbox';

import { FinalizarComponent } from './finalizar.component';
import { PedidosDialogComponent } from './dialogs/pedidos-dialog/pedidos-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  imports: [
    RouterModule,
    CommonModule,
    SharedModule,
    MatCardModule,
    MatTableModule,
    MatDialogModule,
    MatButtonModule,
    MatCheckboxModule,
    ReactiveFormsModule,
  ],
  declarations: [
    FinalizarComponent,
    PedidosDialogComponent
  ]
})
export class FinalizarModule { }
