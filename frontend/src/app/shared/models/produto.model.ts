export interface ProdutoModel {

    produtoId: number;
    nome: string;
    valor: number;
    tipoProduto: string;
    imagem?: string;
    quantidadePermitida: number;
}