import { Component, OnInit } from '@angular/core';

import { MatDialogRef } from '@angular/material/dialog';

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
