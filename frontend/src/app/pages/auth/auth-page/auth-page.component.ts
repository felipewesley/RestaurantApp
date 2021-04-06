import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AuthService } from '../auth.service';

import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.scss'],
  encapsulation: ViewEncapsulation.Emulated
})
export class AuthPageComponent implements OnInit {

  public todayDate: Date = new Date();

  constructor(
    private service: AuthService
  ) { }

  ngOnInit() {

    this.service.getMesas()

      .subscribe(mesas => {

        console.log(mesas);
      })
  }

}
