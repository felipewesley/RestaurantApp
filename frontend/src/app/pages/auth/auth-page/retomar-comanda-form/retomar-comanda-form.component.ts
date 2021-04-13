import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { appRoutes } from 'src/app/consts/app-routes.enum';
import { HomeService } from 'src/app/pages/home/home.service';

import { AuthService } from '../../auth.service';

@Component({
  selector: 'app-retomar-comanda-form',
  templateUrl: './retomar-comanda-form.component.html',
  styleUrls: ['./retomar-comanda-form.component.scss']
})
export class RetomarComandaFormComponent implements OnInit {

  retomarForm: FormGroup;

  constructor (
    private router: Router,
    private service: AuthService,
    private homeService: HomeService
  ) { }

  ngOnInit() {

    this.retomarForm = new FormGroup({
      mesaId: new FormControl(null, [
        Validators.required,
        Validators.min(1),
        Validators.max(16)
      ])
    });
  }

  onSubmit(): void {

    const mesaId = this.retomarForm.get('mesaId').value;
    
    this.service.retomarComanda(mesaId)
    .subscribe(comanda => {

      // this.homeService.iniciarAtendimento(comanda);
      this.router.navigate([ appRoutes.HOME, comanda.comandaId ]);
    });
  }
}