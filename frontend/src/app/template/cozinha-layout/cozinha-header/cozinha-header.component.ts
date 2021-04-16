import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cozinha-header',
  templateUrl: './cozinha-header.component.html',
  styleUrls: ['./cozinha-header.component.scss']
})
export class CozinhaHeaderComponent implements OnInit {

  username: string = 'user';
  toolbarAppName = 'Sutekina Ranchi';
  moduleName = 'Cozinha';

  constructor() { }

  ngOnInit() { }

}
