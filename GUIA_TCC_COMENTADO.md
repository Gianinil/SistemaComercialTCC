# Guia rapido para apresentar o Sistema Comercial

Este arquivo resume as partes principais do projeto para estudo e apresentacao.

## Fluxo geral

1. `Program.cs` inicia a aplicacao.
2. `Database.CriarTabela()` garante que o banco SQLite tenha as tabelas necessarias.
3. `FormSplash` mostra a tela de carregamento.
4. `FormLogin` valida o acesso demonstrativo.
5. `FormMenu` abre o painel principal com atalhos e indicadores.

## Camadas do projeto

- `Models`: classes simples que representam dados do sistema, como Produto, Cliente e Fornecedor.
- `Data`: conexao e criacao das tabelas do SQLite.
- `Data/Repositories`: classes responsaveis por salvar, buscar, editar e excluir dados.
- `Forms`: telas do Windows Forms, focadas na interacao com o usuario.
- `UI/AppTheme.cs`: identidade visual centralizada, com cores, botoes, tabelas e cabecalhos.

## Por que usar Repository

O Repository separa a tela do banco de dados. A tela pede "listar produtos", e o repositório decide qual SQL usar. Isso facilita manutencao, organizacao e explicacao tecnica.

## Psicologia das cores no layout

- Verde: acoes positivas, como salvar, entrar, vender e finalizar.
- Ambar: financeiro e atencao, como caixa, contas e pagar.
- Vermelho: acoes destrutivas ou de saida, como excluir, cancelar, remover e sair.
- Roxo: cadastros de clientes e fornecedores.
- Grafite: fundo neutro para reduzir cansaco visual e destacar informacoes importantes.

## Banco SQLite

O sistema usa SQLite porque e simples para um ERP local: nao exige servidor instalado e grava os dados em um arquivo `estoque.db` junto ao executavel.

## Venda

A tela de venda usa um carrinho temporario. Quando o usuario finaliza, o sistema executa uma transacao:

1. Baixa o estoque.
2. Registra a venda.
3. Registra a entrada no caixa.

Se algo falhar, a transacao desfaz tudo para evitar dados incompletos.
