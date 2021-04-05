import { Component, OnInit, Input } from '@angular/core';

import { CardInfo, VisitsChartData } from '../../models';
import { colors } from '../../../../consts';

@Component({
  selector: 'app-card-info',
  templateUrl: './card-info.component.html',
  styleUrls: ['./card-info.component.css']
})
export class CardInfoComponent implements OnInit {

  @Input('cardElement') element: CardInfo;

  title: string;
  icon?: string;
  content: {label: string, value: any}[];

  public colors: typeof colors = colors;

  constructor() { }

  ngOnInit(): void {
    
    this.title = this.element.title;
    this.icon = this.element.icon;
    this.content = this.element.content;
  }

}
