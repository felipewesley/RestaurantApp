import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MesaModel } from './models/mesa.model';
import { filter, map, take } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ComandaModel } from './models/comanda.model';
import { routes } from 'src/app/consts/routes';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api_url_mesa = environment.API_URL + '/mesa';
  private api_url_comanda = environment.API_URL + '/comanda'

  comandaAtiva: number = 1234;

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor (
    private http: HttpClient,
    private router: Router
  ) { }

  getMesas(): Observable<MesaModel[]> {

    return this.http.get<MesaModel[]>(this.api_url_mesa);
  }

  criarComanda(model: ComandaModel): void {

    /*
    this.http.post<number>(this.api_url_comanda, model)
    .pipe(
      take(1)
    )
    .subscribe(
      comandaId => {

        this.comandaAtiva = comandaId;
        this.router.navigate([ comandaId, routes.HOME ]);
      }
    )
    */

    this.http.post(this.api_url_comanda, model)
    .subscribe(
      () => {

        this.comandaAtiva = 1234;
        this.router.navigate([ this.comandaAtiva, routes.HOME ]);
      }
    )

  }

}
