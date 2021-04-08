import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-tipo-produto-container',
  templateUrl: './tipo-produto-container.component.html',
  styleUrls: ['./tipo-produto-container.component.scss']
})
export class TipoProdutoContainerComponent implements OnInit {

  @Input() categoriaId: number;

  constructor() { }

  ngOnInit() { }

}
