import { Component, Input, OnInit } from '@angular/core';
import { CardInfo } from '../models/card-info.model';

@Component({
  selector: 'app-card-info',
  templateUrl: './card-info.component.html',
  styleUrls: ['./card-info.component.scss']
})
export class CardInfoComponent implements OnInit {

  @Input('cardElement') element: CardInfo;

  title: string;
  icon?: string;
  content: {label: string, value: any}[];

  constructor() { }

  ngOnInit() {

    this.title = this.element.title;
    this.icon = this.element.icon;
    this.content = this.element.content;
  }

}
