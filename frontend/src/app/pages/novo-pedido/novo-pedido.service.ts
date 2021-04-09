import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { TipoProdutoModel } from './models/tipo-produto.model';
import { ProdutoModel } from 'src/app/shared/models/produto.model';

@Injectable({
  providedIn: 'root'
})
export class NovoPedidoService {

  private api_url_categorias = environment.API_URL + '/tipoProduto';
  private api_url_produtos = environment.API_URL + '/produto';

  constructor(private http: HttpClient) { }

  obterCategorias(): Observable<TipoProdutoModel[]> {

    return this.http.get<TipoProdutoModel[]>(this.api_url_categorias);
  }

  buscarProdutos(categoriaId: number): Observable<ProdutoModel[]> {

    return this.http.get<ProdutoModel[]>(this.api_url_produtos + '/tipo/' + categoriaId);
  }

}
