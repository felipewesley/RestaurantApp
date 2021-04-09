import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { routes } from 'src/app/consts/routes';
import { environment } from 'src/environments/environment';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { ComandaCompletaModel } from './models/comanda-completa.model';
import { EncerrarComanda } from './models/encerrar-comanda.model';

@Injectable()
export class HomeService {

  private api_url = environment.API_URL + '/comanda';
  private api_url_pedido = environment.API_URL + '/pedido';

  comandaAtiva: ComandaCompletaModel = {} as ComandaCompletaModel;

  constructor (
    private http: HttpClient,
    private router: Router
  ) { }
  
  setGlobalComanda(comandaId: number): Observable<ComandaCompletaModel> {
    
    return this.http.get<ComandaCompletaModel>(this.api_url + '/' + comandaId)
    .pipe(
      take(1)
    );
  }

  obterPedidosPendentes(comandaId: number): Observable<PedidoListaModel[]> {

    return this.http.get<PedidoListaModel[]>(this.api_url_pedido + '/' + comandaId + '/comanda');
  }

  encerrarAtendimento(porcentagemGarcom: boolean = false): void {

    const model: EncerrarComanda = {
      porcentagemGarcom: porcentagemGarcom
    };

    if (confirm('Deseja realmente encerrar a comanda?')) {

      this.http.put(this.api_url + '/' + this.comandaAtiva.comandaId + '/encerrar', model)
      .subscribe(
        () => {
  
          this.router.navigate([routes.AUTH]);
        }
      );
    }
  }

}
