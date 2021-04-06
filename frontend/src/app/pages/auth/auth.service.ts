import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MesaModel } from './models/mesa.model';
import { filter, map, take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api_url = environment.API_URL + '/mesa';

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getMesas(): Observable<MesaModel[]> {

    return this.http.get<MesaModel[]>(this.api_url);
  }

}
