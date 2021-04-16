import { StatusPedido } from "src/app/consts/status-pedido.enum";
import { ProdutoModel } from "src/app/shared/models/produto.model";

export interface ListagemCozinhaModel {

    comandaId: number;
    pedidoId: number;
    mesaId: number;
    produto: ProdutoModel;
    quantidade:number;
    dataHoraRealizacao: Date;
    dataHoraEntrega: Date;
    statusEnum: StatusPedido;
}