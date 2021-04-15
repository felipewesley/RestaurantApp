import { Component, OnInit, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CozinhaLoginModel } from '../../models/cozinha-login.model';

@Component({
  selector: 'app-cozinha-login',
  templateUrl: './cozinha-login.component.html',
  styleUrls: ['./cozinha-login.component.scss']
})
export class CozinhaLoginComponent implements OnInit {

  loginForm: FormGroup;

  defaultUser: CozinhaLoginModel = {
    username: 'felipe.basso',
    senha: '123abc'
  }

  constructor() { }

  ngOnInit() {
    
    this.loginForm = new FormGroup({
      username: new FormControl(this.defaultUser.username, [
        Validators.required
      ]),
      senha: new FormControl(this.defaultUser.senha, [
        Validators.required
      ])
    });
  }

  onSubmit(): void {


  }

}
