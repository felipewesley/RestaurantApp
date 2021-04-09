import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MesaModel } from './models/mesa.model';
import { filter, map, take, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ComandaFormularioModel } from './models/comanda-formulario.model';
import { routes } from 'src/app/consts/routes';
import { ComandaCompletaModel } from '../home/models/comanda-completa.model';

@Injectable()
export class AuthService {

  private api_url_mesa = environment.API_URL + '/mesa';
  private api_url_comanda = environment.API_URL + '/comanda'

  comandaId: number;

  constructor (
    private http: HttpClient,
    private router: Router
  ) { }

  getMesas(): Observable<MesaModel[]> {

    return this.http.get<MesaModel[]>(this.api_url_mesa);
  }

  criarComanda(model: ComandaFormularioModel): Observable<number> {

    return this.http.post<number>(this.api_url_comanda, model)
    .pipe(
      tap(x => this.comandaId = x),
      take(1)
    );
  }

  retomarComanda(mesaId: number): void {

    this.http.get<ComandaCompletaModel>(this.api_url_comanda + '/' + mesaId + '/mesa')
    .pipe(
      take(1)
    )
    .subscribe(model => {

      this.comandaId = model.comandaId;
      this.router.navigate([ routes.HOME, model.comandaId ]);
    }, error => {

      console.error(error);
    });
  }

}