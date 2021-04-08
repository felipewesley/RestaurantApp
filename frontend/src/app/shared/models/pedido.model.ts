import { StatusPedido } from "src/app/consts/status-pedido.enum";
import { ProdutoModel } from "./produto.model";

export interface PedidoModel {

    pedidoId: number;
    produto: string;
    quantidade: number;
    valor?: number;
    dataHora?: Date;
    status: number;
    actions?: boolean;
}