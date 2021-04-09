import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { routes } from 'src/app/consts/routes';
import { CardInfo } from '../models/card-info.model';
import { HomeService } from '../home.service';
import { ComandaCompletaModel } from '../models/comanda-completa.model';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  comanda: ComandaCompletaModel = {} as ComandaCompletaModel;

  cards: CardInfo[];

  constructor(
    private router: Router,
    private activeRoute: ActivatedRoute,
    private homeService: HomeService
  ) { }

  ngOnInit() {

    this.activeRoute.params
      .pipe(
        switchMap(
          (params: Params) => {
            const comandaId = +params['comandaId'];
            return this.homeService.setGlobalComanda(comandaId);
          })
      )
      .subscribe(model => {

        this.comanda = model;
        this.homeService.comandaAtiva = model;

      }, error => {

        console.error(error);
        this.router.navigate([ routes.AUTH ]);
      })
      ;
  }

  encerrarAtendimento(): void {
  
    // Chamar dialog de confirmacao de encerramento
  
    this.homeService.encerrarAtendimento();
  }
}




