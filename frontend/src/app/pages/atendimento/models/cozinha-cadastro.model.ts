export interface CozinhaCadastroModel {

    primeiroNome: string;
    sobrenome: string;
    email: string;
    dataNascimento: Date;
    senha: string;
    perguntaSeguranca?: string;
}