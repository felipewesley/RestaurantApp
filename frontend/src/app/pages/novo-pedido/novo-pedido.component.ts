import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { ProdutoModel } from 'src/app/shared/models/produto.model';
import { TipoProdutoModel } from './models/tipo-produto.model';
import { NovoPedidoService } from './novo-pedido.service';

@Component({
  selector: 'app-novo-pedido',
  templateUrl: './novo-pedido.component.html',
  styleUrls: ['./novo-pedido.component.scss']
})
export class NovoPedidoComponent implements OnInit, OnDestroy {

  categoriaAtual: TipoProdutoModel = {} as TipoProdutoModel;

  categorias: TipoProdutoModel[];
  produtos: ProdutoModel[];

  categoriasSubscription: Subscription;
  produtosSubscription: Subscription;

  constructor (private service: NovoPedidoService) { }

  ngOnInit() {

    // Busca inicial sempre será por bebidas
    const categoriaInicial: TipoProdutoModel = {
      tipoProdutoId: 1,
      descricao: 'Bebidas'
    };

    this.buscarProdutos(categoriaInicial);
    
    this.categoriaAtual = categoriaInicial;


    this.categoriasSubscription =  this.service.obterCategorias()
      .subscribe(categorias => {
        this.categorias = categorias;
      });
  }

  buscarProdutos(tipoProduto: TipoProdutoModel): void {

    // Se selecionada uma categoria diferente da atual
    if (this.categoriaAtual.tipoProdutoId !== tipoProduto.tipoProdutoId) {

      this.categoriaAtual = tipoProduto;
      
      this.produtosSubscription = this.service.buscarProdutos(tipoProduto.tipoProdutoId)
        .subscribe(produtos => this.produtos = produtos);
    }
  }

  ngOnDestroy() {

    this.categoriasSubscription.unsubscribe();
    this.produtosSubscription.unsubscribe();
  }

}
