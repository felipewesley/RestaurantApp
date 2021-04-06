import { Component, Input, OnInit } from '@angular/core';
import { BadgeColor } from './badgeColor.enum';

@Component({
  selector: 'app-badge',
  templateUrl: './badge.component.html',
  styleUrls: ['./badge.component.scss']
})
export class BadgeComponent implements OnInit {

  @Input('color') theme: typeof BadgeColor;

  constructor() { }

  ngOnInit() { }

}
