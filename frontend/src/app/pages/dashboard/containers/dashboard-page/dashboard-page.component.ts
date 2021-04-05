import { Component, OnInit } from '@angular/core';

import { DashboardService } from '../../services';
import { CardInfo } from '../../models';

import { PedidoPendente } from '../../models/pedido-pendente.model';

import { Router } from '@angular/router';
import { routes } from 'src/app/consts';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {

  public cards: CardInfo[] = [];

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

  public pedidosData: PedidoPendente[];

  constructor(
    private service: DashboardService,
    private router: Router
    ) {

    this.pedidosData = this.service.loadPedidosPendentes();
  }

  backToLogin(): void {
    this.router.navigate([routes.LOGIN]);
  }
}
