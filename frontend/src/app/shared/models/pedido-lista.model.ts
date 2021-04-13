import { StatusPedido } from "src/app/consts/status-pedido.enum";
import { ProdutoModel } from "./produto.model";

export interface PedidoListaModel {

    pedidoId: number;
    comandaId: number;
    produto: ProdutoModel;
    quantidade: number;
    statusEnum: StatusPedido;
    novoValorComanda?: number;
}