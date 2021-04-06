import { StatusPedido } from "../../dashboard/consts";

export interface PedidoCompleto {
    pedidoId: number;
    produto: string;
    quantidade: number;
    dataHora: Date;
    valor: number;
    status: StatusPedido;
}