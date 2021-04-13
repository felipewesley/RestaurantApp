import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { Subscription } from 'rxjs';
import { switchMap, take } from 'rxjs/operators';

import { appRoutes } from 'src/app/consts/app-routes.enum';
import { CardInfo } from '../models/card-info.model';
import { HomeService } from '../home.service';
import { ComandaCompletaModel } from '../models/comanda-completa.model';

import { MatDialog } from '@angular/material/dialog';
import { EncerrarComandaDialogComponent } from '../dialogs/encerrar-comanda-dialog/encerrar-comanda-dialog.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit, OnDestroy {

  comandaSubscription: Subscription;
  comanda: ComandaCompletaModel = {} as ComandaCompletaModel;
  comandaId: number;
  mesa: number;

  cards: CardInfo[];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private homeService: HomeService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {

    this.comandaSubscription = this.route.params
      .pipe(
        take(1),
        switchMap((params: Params) => {

          this.comandaId = +params['comandaId'];
          return this.homeService.obterComanda(this.comandaId);
        })
      )
      .subscribe(comanda => {

        console.log(comanda);
        this.comanda = comanda;
      });
  }

  encerrarAtendimento(): void {

    let dialog = this.dialog.open(EncerrarComandaDialogComponent);

    dialog.afterClosed()
    .pipe(
      take(1)
    )
    .subscribe(r => {
      if (r)
        this.router.navigate([ appRoutes.FINALIZAR ], { relativeTo: this.route });
    })
  }

  ngOnDestroy() {

    this.comandaSubscription.unsubscribe();
  }
}