import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { StdSnackbarService } from 'src/app/shared/ui-elements/std-snackbar/std-snackbar.service';
import { CozinhaService } from './cozinha.service';
import { ListagemCozinhaModel } from './models/listagem-cozinha.model';

@Component({
  selector: 'app-cozinha',
  templateUrl: './cozinha.component.html',
  styleUrls: ['./cozinha.component.scss']
})
export class CozinhaComponent implements OnInit, OnDestroy {

  pedidosSubscription: Subscription;
  pedidos: ListagemCozinhaModel[] = [];

  constructor (
    private cozinhaService: CozinhaService,
    private snackBar: StdSnackbarService
  ) { }

  ngOnInit() {

    this.pedidosSubscription = this.cozinhaService.obterPedidosPendentes()
    .subscribe(pedidos => {
      
      this.pedidos = pedidos.filter(p => p.statusEnum == StatusPedido.EmAndamento);
    })
  }

  ngOnDestroy() {

    this.pedidosSubscription.unsubscribe();
  }

   entregarPedido(pedidoId: number): void {

    this.cozinhaService.entregarPedido(pedidoId)
      .pipe(
        take(1)
      )
      .subscribe(mesaId => {

        this.snackBar.open(`O pedido foi entregue para a mesa ${mesaId}`);
      });
    
   }


}
