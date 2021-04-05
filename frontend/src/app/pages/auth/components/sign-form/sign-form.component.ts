import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-form',
  templateUrl: './sign-form.component.html',
  styleUrls: ['./sign-form.component.scss']
})
export class SignFormComponent implements OnInit {

  retomarForm: FormGroup;

  ngOnInit() {

    this.retomarForm = new FormGroup({
      "comandaId": new FormControl(null)
    });
  }

  retomarComanda(): void {

    console.warn('Comanda retomada!');
    console.log(this.retomarForm);
  }
}
