import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-card-mesa',
  templateUrl: './card-mesa.component.html',
  styleUrls: ['./card-mesa.component.scss']
})
export class CardMesaComponent implements OnInit {

  @Input() mesaId: number;
  valorRodizio: number = 45.0;

  constructor() { }

  ngOnInit() { }

}
