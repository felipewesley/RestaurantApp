import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ComandaCompletaModel } from '../models/comanda-completa.model';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../auth/auth.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  api_url = environment.API_URL + '/comanda/obter/completa';

  comandaAtual = new BehaviorSubject<ComandaCompletaModel>(null);

  comandaAtual$ = this.comandaAtual.asObservable();

  private comandaId: number;
  comandaAtiva: ComandaCompletaModel;

  constructor (
    private http: HttpClient,
    private router: Router,
    private activeRoute: ActivatedRoute,
    private authService: AuthService
  ) { }

  setGlobalComanda(): void {
    
    this.comandaId = this.authService.comandaAtiva;
    
    this.http.get<ComandaCompletaModel>(this.api_url + '/' + this.comandaId)
    .subscribe(
      comandaModel => {

        this.comandaAtiva = comandaModel;
        console.log(comandaModel);
      }
    );
  }

}
