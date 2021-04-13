import { Injectable } from '@angular/core';

import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';

@Injectable()
export class StdSnackbarService {

  private horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  private verticalPosition: MatSnackBarVerticalPosition = 'top';
  private duration = 1750;

  private closeIcon = 'x';

  constructor(private _snackBar: MatSnackBar) { }

  open(content: string, aditionalDuration?: number) {
    
    this._snackBar.open(content, this.closeIcon, {
      duration: aditionalDuration ? this.duration + aditionalDuration : this.duration,
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
    });
  }

}
