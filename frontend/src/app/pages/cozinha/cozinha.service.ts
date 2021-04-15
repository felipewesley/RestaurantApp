import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, pipe } from 'rxjs';
import { take } from 'rxjs/operators';
import { apiRoutes } from 'src/app/consts/api-routes.enum';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';
import { environment } from 'src/environments/environment';
import { CozinhaCadastroModel } from '../atendimento/models/cozinha-cadastro.model';
import { CozinhaLoginModel } from '../atendimento/models/cozinha-login.model';

const API_URL = `${environment.API_URL}/${apiRoutes.COZINHA}`;

@Injectable()
export class CozinhaService {

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

  obterPedidosPendentes(): Observable<PedidoListaModel[]> {

    return this.http.get<PedidoListaModel[]>(`${API_URL}/pedidos/pendentes`)
      .pipe(
        take(1)
      );
  }

  entregarPedido(pedidoId: number): void { }

}
