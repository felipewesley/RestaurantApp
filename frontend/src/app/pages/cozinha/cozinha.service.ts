import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, pipe } from 'rxjs';
import { switchMap, take, tap } from 'rxjs/operators';
import { apiRoutes } from 'src/app/consts/api-routes.enum';
import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { environment } from 'src/environments/environment';
import { CozinhaCadastroModel } from '../atendimento/models/cozinha-cadastro.model';
import { CozinhaLoginModel } from '../atendimento/models/cozinha-login.model';
import { ListagemCozinhaModel } from './models/listagem-cozinha.model';

const API_URL = `${environment.API_URL}/${apiRoutes.COZINHA}`;

@Injectable()
export class CozinhaService {

  private _pedidos = new BehaviorSubject<ListagemCozinhaModel[]>([]);

  pedidos$ = this._pedidos.asObservable();

  constructor (
    private http: HttpClient
  ) { }

  private username: string = null;

  autenticarUsuario(model: CozinhaLoginModel): Observable<boolean> {

    return this.http.post<boolean>(`${API_URL}/autenticar`, model)
      .pipe(
        take(1)
      );
  }

  criarUsuario(model: CozinhaCadastroModel): Observable<string> {
    
    return this.http.post<string>(`${API_URL}/user`, model)
      .pipe(
        take(1)
      );
  }

  obterPedidosPendentes(): Observable<ListagemCozinhaModel[]> {

    return this.http.get<ListagemCozinhaModel[]>(`${API_URL}/pedido/pendentes`)
      .pipe(
        take(1),
        switchMap(pedidos => {
          this._pedidos.next(pedidos);
          return this.pedidos$;
        })
      );
  }

  entregarPedido(pedidoId: number): Observable<number> {
    
    // dar next no behaviorSubject neste metodo

    return this.http.put<number>(`${API_URL}/pedido/${pedidoId}/entregar`, {})
      .pipe(
        take(1),
        tap(() => {
          let pedidos = this._pedidos.getValue()
                      .map(p => p.pedidoId == pedidoId ? { ...p, statusEnum: StatusPedido.Entregue } : p);

          this._pedidos.next(pedidos);
        })
      );
  }

}
