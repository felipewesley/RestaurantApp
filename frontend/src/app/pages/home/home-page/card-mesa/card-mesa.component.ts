import { Component, Input, OnInit } from '@angular/core';
import { MesaModel } from 'src/app/pages/auth/models/mesa.model';

@Component({
  selector: 'app-card-mesa',
  templateUrl: './card-mesa.component.html',
  styleUrls: ['./card-mesa.component.scss']
})
export class CardMesaComponent implements OnInit {

  @Input() mesaId: number;

  constructor() { }

  ngOnInit() { }

}
