import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-retomar-comanda-form',
  templateUrl: './retomar-comanda-form.component.html',
  styleUrls: ['./retomar-comanda-form.component.scss']
})
export class RetomarComandaFormComponent implements OnInit {

  retomarForm: FormGroup;
  value: string = '';

  constructor() { }

  ngOnInit() {

    this.retomarForm = new FormGroup({
      "comandaId": new FormControl(null, [
        Validators.required,
        Validators.min(1),
        Validators.max(16)
      ])
    });
  }

  onSubmit(): void {

    console.warn('Comanda retomada!');
    console.log(this.retomarForm);
  }
}