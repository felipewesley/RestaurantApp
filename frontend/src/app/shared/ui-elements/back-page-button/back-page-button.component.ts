import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-back-page-button',
  templateUrl: './back-page-button.component.html',
  styleUrls: ['./back-page-button.component.scss']
})
export class BackPageButtonComponent implements OnInit {

  constructor (private location: Location) { }

  ngOnInit() { }

  onBackPage(): void {
    this.location.back();
  }

}
