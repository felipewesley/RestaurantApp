import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ComandaCriada } from '../models/comanda-criada.model';
import { NovaComandaModel } from '../models/nova-comanda.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  API_URL: "http://localhost:5270/";

  constructor (
    // private http: HttpClient
  ) { }

  /*
  criarComanda(model: NovaComandaModel): Observable<ComandaCriada> {

    return this.http.post<ComandaCriada>(this.API_URL, model);
  }

  retomarComanda(comandaId: number): Observable<number> {

    return this.http.get<number>(this.API_URL + comandaId);
  }
  */
}
