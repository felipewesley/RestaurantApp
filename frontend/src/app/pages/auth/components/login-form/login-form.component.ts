import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { routes } from 'src/app/consts';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {

  comandaForm: FormGroup;

  constructor(private router: Router) { }

  ngOnInit() {

    this.comandaForm = new FormGroup({
      mesaId: new FormControl(null),
      qtdeClientes: new FormControl(null)
    });
  }

  criarComanda(): void {

    console.warn('Comanda criada!');
    console.log(this.comandaForm);

    setTimeout(() => {

      // http://localhost:4200/#/dashboard
      this.router.navigate([routes.DASHBOARD]);

    }, 3000)
  }
}
