import { PedidoListaModel } from "src/app/shared/models/pedido-lista.model";

export interface ComandaCompletaModel {

    comandaId: number;
    mesaId: number;
    quantidadeClientes: number;
    valor: number;
    dataHoraEntrada: number;
    paga: boolean;
    pedidos: PedidoListaModel[];
}