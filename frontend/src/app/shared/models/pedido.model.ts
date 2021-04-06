export interface PedidoModel {
    pedidoId: number;
    produto: string;
    quantidade: number;
    valor: number;
    dataHora?: Date;
    status: number;
    actions?: boolean;
}