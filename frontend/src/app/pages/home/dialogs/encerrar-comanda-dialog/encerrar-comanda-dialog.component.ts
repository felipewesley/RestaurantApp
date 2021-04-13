import { Component, Inject, OnInit } from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { appRoutes } from 'src/app/consts/app-routes.enum';

@Component({
  selector: 'app-encerrar-comanda-dialog',
  templateUrl: './encerrar-comanda-dialog.component.html',
  styleUrls: ['./encerrar-comanda-dialog.component.scss']
})
export class EncerrarComandaDialogComponent implements OnInit {

  constructor(
    private dialogRef: MatDialogRef<EncerrarComandaDialogComponent>,
  ) { }

  ngOnInit() { }

  confirmarEncerramento(): void {

    this.dialogRef.close(true);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

}
