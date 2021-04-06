import { Component } from '@angular/core';
import { AuthService } from '../../services';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.scss']
})
export class AuthPageComponent {

  public todayDate: Date = new Date();

  constructor(
    // private service: AuthService
    ) { }

  criarComanda() {
    // this.service
  }

}
