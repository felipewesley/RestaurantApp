import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { TipoProdutoModel } from './models/tipo-produto.model';
import { ProdutoModel } from 'src/app/shared/models/produto.model';

const API_URL_CATEGORIAS = `${environment.API_URL}/tipoProduto`;
const API_URL_PRODUTOS = `${environment.API_URL}/produto`;

@Injectable({
  providedIn: 'root'
})
export class NovoPedidoService {

  constructor(private http: HttpClient) { }

  obterCategorias(): Observable<TipoProdutoModel[]> {

    return this.http.get<TipoProdutoModel[]>(API_URL_CATEGORIAS);
  }

  buscarProdutos(categoriaId: number): Observable<ProdutoModel[]> {

    return this.http.get<ProdutoModel[]>(`${API_URL_PRODUTOS}/tipo/${categoriaId}`);
  }

}
