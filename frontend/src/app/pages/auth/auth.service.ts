import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { switchMap, take, tap } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { apiRoutes } from 'src/app/consts/api-routes.enum';
import { MesaModel } from './models/mesa.model';
import { ComandaFormularioModel } from './models/comanda-formulario.model';
import { ComandaCompletaModel } from '../home/models/comanda-completa.model';

const API_URL_MESA = `${environment.API_URL}/${apiRoutes.MESA}`;
const API_URL_COMANDA = `${environment.API_URL}/${apiRoutes.COMANDA}`;

@Injectable()
export class AuthService {

  comandaId: number;

  constructor (
    private http: HttpClient
  ) { }

  getMesas(): Observable<MesaModel[]> {

    return this.http.get<MesaModel[]>(API_URL_MESA)
    .pipe(
      take(1)
    );
  }

  obterComanda(comandaId: number): Observable<ComandaCompletaModel> {

    return this.http.get<ComandaCompletaModel>(`${API_URL_COMANDA}/${comandaId}`)
    .pipe(
      take(1)
    );
  }

  criarComanda(model: ComandaFormularioModel): Observable<ComandaCompletaModel> {

    return this.http.post<number>(API_URL_COMANDA, model)
    .pipe(
      take(1),
      tap(comandaId => this.comandaId = comandaId),
      switchMap(comandaId => this.obterComanda(comandaId))
    );
  }

  retomarComanda(mesaId: number): Observable<ComandaCompletaModel> {

    return this.http.get<number>(`${API_URL_COMANDA}/${mesaId}/retomar`)
    .pipe(
      take(1),
      tap(comandaId => this.comandaId = comandaId),
      switchMap(comandaId => this.obterComanda(comandaId))
    );
  }

}