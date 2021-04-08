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

  categoriaAtual: string = "Bebidas";

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

    this.buscarProdutos(0);
  }

  buscarProdutos(categoriaId: number): void {

    this.service.buscarProdutos(categoriaId).subscribe(
      produtos => {
        this.produtos = produtos;
      }
    );
  }

}
