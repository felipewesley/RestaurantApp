import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { AuthService } from '../auth/auth.service';
import { HomeService } from '../home/home.service';
import { environment } from 'src/environments/environment';
import { StatusPedido } from 'src/app/consts/status-pedido.enum';
import { PedidoAlterarModel } from './models/pedido-alterar.model';
import { PedidoFormularioModel } from './models/pedido-formulario.model';
import { PedidoListaModel } from 'src/app/shared/models/pedido-lista.model';

@Injectable()
export class PedidoService {

  private api_url = environment.API_URL + '/pedido';

  private _pedidos = new BehaviorSubject<PedidoListaModel[]>([]);

  pedidos: PedidoListaModel;
  pedidos$ = this._pedidos.asObservable();

  novoPedidoForm = new FormGroup({
    produtoId: new FormControl(null),
    quantidade: new FormControl(null)
  });

  constructor (
    private homeService: HomeService,
    private authService: AuthService,
    private http: HttpClient
  ) {
    
    const comandaId = this.authService.comandaId;

    http.get<PedidoListaModel[]>(this.api_url + '/' + comandaId + '/comanda')
    .subscribe(pedidos => this._pedidos.next(pedidos));
  }

  novoPedido(model: PedidoFormularioModel): void {

    model.comandaId = this.homeService.comandaAtiva.comandaId;
    
    this.http.post<PedidoListaModel>(this.api_url, model)
    .pipe(
      take(1)
    )
    .subscribe(p => {

      const pedidos = this._pedidos.getValue();
      pedidos.push(p);

      this._pedidos.next(pedidos.slice());
    }, error => {

      console.error(error);
    });
  }

  editarPedido(pedidoId: number, model: PedidoAlterarModel): void {
    
    const comandaId = this.authService.comandaId;

    model.comandaId = comandaId;

    this.http.put<PedidoListaModel>(this.api_url + '/' + pedidoId, model)
    .pipe(
      take(1)
    )
    .subscribe(pedido => {

      const pedidos = this._pedidos.getValue()
                    .map(p => p.pedidoId == pedidoId ? { ...p, quantidade: pedido.quantidade} : p);

      this._pedidos.next(pedidos);
    });
  }

  cancelarPedido(pedidoId: number): void {

    this.http.put<StatusPedido>(this.api_url + '/' + pedidoId + '/cancelar', {})
    .pipe(
      take(1)
    )
    .subscribe(novoStatus => {

      let pedidos = this._pedidos.getValue()
                  .map(p => p.pedidoId == pedidoId ? { ...p, statusEnum: novoStatus } : p);

      this,this._pedidos.next(pedidos);
    });
  }

}