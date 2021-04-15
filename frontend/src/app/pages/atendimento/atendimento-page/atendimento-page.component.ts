import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-atendimento-page',
  templateUrl: './atendimento-page.component.html',
  styleUrls: ['./atendimento-page.component.scss'],
  encapsulation: ViewEncapsulation.Emulated
})
export class AtendimentoPageComponent implements OnInit {

  tituloCozinhaTab: string = 'Ola!';

  constructor() { }

  ngOnInit() {
    let currentDate = new Date;

    if (currentDate.getHours() >= 6 && currentDate.getHours() < 12) {
      // Periodo da manha
      this.tituloCozinhaTab = 'Bom dia!';

    } else if (currentDate.getHours() >= 12 && currentDate.getHours() < 18) {
      // Periodo da tarde
      this.tituloCozinhaTab = 'Boa tarde!';

    } else {
      // Periodo da tarde
      this.tituloCozinhaTab = 'Boa noite!';
    }
  }
}
