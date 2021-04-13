import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';

import { Subscription } from 'rxjs';
import { switchMap, take } from 'rxjs/operators';

import { ComandaCompletaModel } from '../home/models/comanda-completa.model';
import { HomeService } from '../home/home.service';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { PedidosDialogComponent } from './dialogs/pedidos-dialog/pedidos-dialog.component';
import { StdSnackbarService } from 'src/app/shared/ui-elements/std-snackbar/std-snackbar.service';
import { appRoutes } from 'src/app/consts/app-routes.enum';

@Component({
  selector: 'app-finalizar',
  templateUrl: './finalizar.component.html',
  styleUrls: ['./finalizar.component.scss']
})
export class FinalizarComponent implements OnInit, OnDestroy {

  comandaSubscription: Subscription;
  encerramentoSubscription: Subscription;

  comanda: ComandaCompletaModel = {} as ComandaCompletaModel;
  finalizada: boolean = false;
  
  encerramentoComandaForm: FormGroup;
  labelEncerramento = 'Finalização do atendimento';
  dataHoraEncerramento: Date = new Date;

  constructor (
    private homeService: HomeService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: StdSnackbarService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    
    this.encerramentoComandaForm = new FormGroup({
      porcentagemGarcom: new FormControl(true)
    });

    this.comandaSubscription = this.route.params
      .pipe(
        take(1),
        switchMap((params: Params) => {

          const comandaId = +params['comandaId'];
          return this.homeService.obterComanda(comandaId);
        })
      )
      .subscribe(comanda => {

        this.comanda = comanda;
        this.finalizada = comanda.paga;
      }, error => {

        this.snackBar.open('A comanda solicitada já foi encerrada');
        this.router.navigate([ appRoutes.AUTH ]);
      });
  }

  onSubmit() {

    const porcentagemGarcom = this.encerramentoComandaForm.get('porcentagemGarcom').value;

    this.encerramentoSubscription = this.homeService.encerrarAtendimento(this.comanda.comandaId, porcentagemGarcom)
    .subscribe(comandaId => {

      this.finalizada = true;
      this.snackBar.open(`Comanda ${comandaId} encerrada com sucesso!`);
      this.labelEncerramento = 'Atendimento finalizado';
    });
  }

  verPedidos(): void {

    this.dialog.open(PedidosDialogComponent, { data: this.comanda.pedidos });
  }

  ngOnDestroy() {

    if (this.comandaSubscription)
      this.comandaSubscription.unsubscribe();

    if (this.encerramentoSubscription)
      this.encerramentoSubscription.unsubscribe();
  }

}
