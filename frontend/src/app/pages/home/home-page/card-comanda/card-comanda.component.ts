import { Component, Input, OnInit } from '@angular/core';
import { ComandaCompletaModel } from '../../models/comanda-completa.model';

@Component({
  selector: 'app-card-comanda',
  templateUrl: './card-comanda.component.html',
  styleUrls: ['./card-comanda.component.scss']
})
export class CardComandaComponent implements OnInit {

  @Input() comanda: ComandaCompletaModel;

  constructor() { }

  ngOnInit() { }

}
