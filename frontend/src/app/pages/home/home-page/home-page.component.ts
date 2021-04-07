import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { routes } from 'src/app/consts/routes';
import { AuthService } from '../../auth/auth.service';
import { CardInfo } from '../models/card-info.model';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  private comandaId: number;

  cards: CardInfo[];

  constructor (
    private router: Router,
    private activeRoute: ActivatedRoute,
    private homeService: HomeService
  ) { }

  ngOnInit() {

    /*this.activeRoute.params
      .subscribe(
        (params: Params) => {
          console.warn('params:', params);
          this.comandaId = +params['id']
        }
      );
      */

    // Buscando comanda pelo id
    this.homeService.setGlobalComanda();

    const comanda = this.homeService.comandaAtiva;

    this.cards = [
      {
        title: 'Mesa',
        icon: 'dashboard',
        content: [
          { label: 'Número da mesa', value: comanda.mesaId },
          { label: 'Rodízios', value: comanda.quantidadeClientes + ' pessoa(s)' }
        ],
        disabled: false
      }, {
        title: 'Comanda',
        icon: 'fact_check',
        content: [
          { label: 'Código', value: comanda.comandaId },
          { label: 'Valor atual', value: 'R$' + comanda.valor }
        ],
      }
    ].filter(m => m.disabled !== true);
  }

  encerrarAtendimento(): void {

    this.router.navigate([routes.AUTH]);
  }
}
