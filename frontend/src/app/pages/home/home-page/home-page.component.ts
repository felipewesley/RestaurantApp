import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { routes } from 'src/app/consts/routes';
import { CardInfo } from '../models/card-info.model';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  cards: CardInfo[];

  constructor (
    private router: Router
  ) { }

  ngOnInit() {

    this.cards = [
      {
        title: 'Mesa',
        icon: 'dashboard',
        content: [
          { label: 'Número da mesa', value: 12 },
          { label: 'Rodízios', value: '3 pessoas' }
        ],
        disabled: false
      }, {
        title: 'Comanda',
        icon: 'fact_check',
        content: [
          { label: 'Código', value: '024512' },
          { label: 'Valor atual', value: 'R$ 94.65' }
        ],
      }
    ].filter(m => m.disabled !== true);
  }

  encerrarAtendimento(): void {

    this.router.navigate([routes.AUTH]);
  }
}
