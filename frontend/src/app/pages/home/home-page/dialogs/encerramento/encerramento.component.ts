import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

/**
 * @title Dialog elements
 */
@Component({
  selector: 'app-encerramento',
  templateUrl: './encerramento.component.html',
  styleUrls: ['./encerramento.component.scss']
})
export class EncerramentoComponent {

  @Input() title: string;
  @Input() content: string|null;

  constructor(public dialog: MatDialog) { }

  encerrarDialog() {

    this.dialog.open(EncerramentoComponent);
  }
}
