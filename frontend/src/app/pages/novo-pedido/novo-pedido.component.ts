import { Component, OnInit } from '@angular/core';
import { ProdutoModel } from 'src/app/shared/models/produto.model';
import { TipoProdutoModel } from './models/tipo-produto.model';
import { NovoPedidoService } from './novo-pedido.service';

@Component({
  selector: 'app-novo-pedido',
  templateUrl: './novo-pedido.component.html',
  styleUrls: ['./novo-pedido.component.scss']
})
export class NovoPedidoComponent implements OnInit {

  categoriaAtual: TipoProdutoModel = {} as TipoProdutoModel;

  categorias: TipoProdutoModel[];
  produtos: ProdutoModel[];

  constructor (
    private service: NovoPedidoService
  ) { }

  ngOnInit() {

    this.service.obterCategorias().subscribe(
      categorias => {

        this.categorias = categorias;
      }
    );

    const categoriaInicial: TipoProdutoModel = {
      tipoProdutoId: 1,
      descricao: 'Bebidas'
    }

    // Busca inicial sempre será por bebidas
    this.buscarProdutos({
      tipoProdutoId: 1,
      descricao: 'Bebidas'
    });

    this.categoriaAtual = categoriaInicial;
  }

  buscarProdutos(tipoProduto: TipoProdutoModel): void {

    if (this.categoriaAtual.tipoProdutoId !== tipoProduto.tipoProdutoId) {

      this.categoriaAtual = tipoProduto;

      this.service.buscarProdutos(tipoProduto.tipoProdutoId)
      .subscribe(
        produtos => {
          this.produtos = produtos;
        }
      );
    }
  }

}
