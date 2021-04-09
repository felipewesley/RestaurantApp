import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { AuthService } from '../../auth.service';
import { ComandaFormularioModel } from '../../models/comanda-formulario.model';
import { MesaModel } from '../../models/mesa.model';

@Component({
  selector: 'app-nova-comanda-form',
  templateUrl: './nova-comanda-form.component.html',
  styleUrls: ['./nova-comanda-form.component.scss']
})
export class NovaComandaFormComponent implements OnInit {

  mesasDisponiveis: MesaModel[] = [];

  comandaForm: FormGroup;

  constructor (private service: AuthService) { }

  ngOnInit() {

    // Estrutura ReactiveForms
    this.comandaForm = new FormGroup({
      mesaId: new FormControl(null, [
        Validators.required, 
        Validators.min(1),
        Validators.max(16)
      ]),
      qtdeClientes: new FormControl(null, [
        Validators.required,
        Validators.min(1)
      ])
    });

    // Obtendo mesas disponiveis
    this.service.getMesas()
      .subscribe(mesas => this.mesasDisponiveis = mesas)
  }

  onSubmit(): void {

    const model: ComandaFormularioModel = {

      mesaId: this.comandaForm.get('mesaId').value,
      quantidadeCliente: this.comandaForm.get('qtdeClientes').value
    };

    this.service.criarComanda(model);
  }

  setFieldMesa(mesa: MesaModel): void {

    this.comandaForm.get('mesaId').setValue(mesa.mesaId);

    // Definindo maximo de clientes da mesa
    this.comandaForm.get('qtdeClientes').setValidators(Validators.max(mesa.capacidade));
  }

}
