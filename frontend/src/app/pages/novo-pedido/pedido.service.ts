import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject, Observable } from 'rxjs';
import { switchMap, take } from 'rxjs/operators';

import { apiRoutes } from 'src/app/consts/api-routes.enum';
import { environment } from 'src/environments/environment';

import { PedidoAlterarModel } from './models/pedido-alterar.model';
import { PedidoFormularioModel } from './models/pedido-formulario.model';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { HomeService } from '../home/home.service';
import { ComandaCompletaModel } from '../home/models/comanda-completa.model';

const API_URL = `${environment.API_URL}/${apiRoutes.PEDIDO}`;

@Injectable()
export class PedidoService {

  private _pedidos = new BehaviorSubject<PedidoListaModel[]>([]);

  pedidos$ = this._pedidos.asObservable();

  constructor (
    private homeService: HomeService,
    private http: HttpClient
  ) { }

  obterPedidos(comandaId: number): Observable<PedidoListaModel[]> {

    return this.http.get<PedidoListaModel[]>(`${API_URL}/${comandaId}/comanda`)
    .pipe(
      take(1),
      switchMap(pedidos => {

        this._pedidos.next(pedidos);
        return this.pedidos$;
      })
    );
  }

  novoPedido(model: PedidoFormularioModel): Observable<PedidoListaModel> {

    return this.homeService.comanda$
    .pipe(
      take(1),
      switchMap((comanda: ComandaCompletaModel) => {

        model.comandaId = comanda.comandaId;
        return this.http.post<PedidoListaModel>(API_URL, model);
      })
    );
  }

  editarPedido(pedidoId: number, model: PedidoAlterarModel): Observable<PedidoListaModel> {
    
    return this.homeService.comanda$
    .pipe(
      take(1),
      switchMap((comanda: ComandaCompletaModel) => {

        model.comandaId = comanda.comandaId;
        return this.http.put<PedidoListaModel>(`${API_URL}/${pedidoId}`, model);
      })
    );
  }

  cancelarPedido(pedidoId: number): Observable<PedidoListaModel> {

    return this.homeService.comanda$
    .pipe(
      take(1),
      switchMap(() => {
        return this.http.put<PedidoListaModel>(`${API_URL}/${pedidoId}/cancelar`, {});
      })
    );
  }

  atualizarPedidos(model: PedidoListaModel, novo: boolean = false): void {

    let pedidos: PedidoListaModel[];

    if (novo) {

      pedidos = this._pedidos.getValue();
      pedidos.push(model);

    } else {

      pedidos = this._pedidos.getValue()
              .map(p => p.pedidoId === model.pedidoId ? model : p);
    }

    this._pedidos.next(pedidos);
    this.homeService.atualizarValorComanda(model);
  }

}