import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';

import { apiRoutes } from 'src/app/consts/api-routes.enum';
import { environment } from 'src/environments/environment';

import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { ComandaCompletaModel } from './models/comanda-completa.model';
import { EncerrarComanda } from './models/encerrar-comanda.model';
import { switchMap, take } from 'rxjs/operators';

const API_URL = `${environment.API_URL}/${apiRoutes.COMANDA}`;

@Injectable()
export class HomeService {

  comandaId: number;

  private _comanda = new BehaviorSubject<ComandaCompletaModel>(null);

  comanda$ = this._comanda.asObservable();

  // comandaAtiva: ComandaCompletaModel = {} as ComandaCompletaModel;

  constructor (private http: HttpClient) { }

  obterComanda(comandaId: number): Observable<ComandaCompletaModel> {

    return this.http.get<ComandaCompletaModel>(`${API_URL}/${comandaId}`)
    .pipe(
      take(1),
      switchMap(
        comanda => {
          this._comanda.next(comanda);
          return this.comanda$;
        }
      )
    );
  }
  
  // iniciarAtendimento(comanda: ComandaCompletaModel): void {

  //   this._comanda.next(comanda);
  // }

  encerrarAtendimento(porcentagemGarcom: boolean = false): Observable<number> {

    const comandaId = this.comandaId;

    const model: EncerrarComanda = {
      porcentagemGarcom: porcentagemGarcom
    };

    if (confirm('Deseja realmente encerrar a comanda?')) {

      return this.http.put<number>(`${API_URL}/${comandaId}/encerrar`, model);
    }
  }

  atualizarValorComanda(model: PedidoListaModel): void {

    let comanda = this._comanda.getValue();

    comanda.valor = model.novoValorComanda;
    this._comanda.next(comanda);
  }

}
