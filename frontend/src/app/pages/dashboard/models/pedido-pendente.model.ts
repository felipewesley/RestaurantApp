export interface PedidoPendente {
    codigo: number;
    produto: string;
    quantidade: number;
    valor: number;
    status: number;
    actions?: boolean;
}
