import { Component, Input, OnInit } from '@angular/core';
import { ProdutoModel } from 'src/app/shared/models/produto.model';

@Component({
  selector: 'app-produto-container',
  templateUrl: './produto-container.component.html',
  styleUrls: ['./produto-container.component.scss']
})
export class ProdutoContainerComponent implements OnInit {

  /*
  @Input() produto: ProdutoModel = {

    produtoId: 123,
    nome: 'Sashimi',
    tipoProduto: 'Carne/Peixe',
    quantidadePermitida: 0,
    valor: 7.9
  };
  */
  @Input() produto: ProdutoModel = {
    
    produtoId: 123,
    nome: 'Sashimi',
    tipoProduto: 'Carne/Peixe',
    quantidadePermitida: 0,
    valor: 7.9
  };

  constructor() { }

  ngOnInit() {
  }

}
