# Sistema Comercial - roteiro da reapresentação

## Fluxo recomendado para demonstrar

1. Abra o **Caixa** e informe um valor inicial.
2. Abra **Clientes**, cadastre um cliente e mostre a edição na própria tela de listagem.
3. Abra **Fornecedores**, cadastre um fornecedor e mostre a edição.
4. Cadastre um produto com uma pequena quantidade inicial.
5. Em **Compras**, selecione o fornecedor, adicione o produto e finalize como pendente.
6. Abra **Estoque** e mostre que a compra aumentou a quantidade e criou uma movimentação de entrada.
7. Abra **Contas a Pagar** e mostre a conta criada automaticamente. Use **Pagar**.
8. Volte ao **Caixa** e mostre a saída financeira do pagamento.
9. Faça uma venda. Para demonstrar o financeiro, escolha **A Prazo**.
10. Abra **Contas a Receber**, mostre a conta automática e use **Receber**.
11. Volte ao **Estoque** para mostrar a saída da venda e ao **Caixa** para mostrar a entrada.
12. Feche o caixa e finalize criando um **Backup**.

## Como explicar a integração

- A compra é gravada dentro de uma transação no banco.
- Na mesma operação, o estoque aumenta, o histórico recebe uma entrada e o contas a pagar é criado.
- A venda reduz o estoque, registra uma saída no histórico e cria uma conta a receber.
- Pagamentos e recebimentos somente movimentam o caixa quando ele está aberto.
- O caixa mostra apenas movimentos do mês atual, mas os dados antigos continuam no banco.
- Contas pagas permanecem no histórico e são bloqueadas contra alterações financeiras retroativas.
- O backup usa o recurso de cópia do próprio SQLite para gerar um arquivo consistente.

## Conceitos técnicos aplicados

- CRUD para clientes, fornecedores, produtos e contas.
- Chaves primárias e estrangeiras para relacionar compras, produtos e fornecedores.
- Transações para impedir registros incompletos.
- Consultas parametrizadas para reduzir risco de SQL Injection.
- Separação entre Forms, Models, Repositories e Services.
- Persistência local com SQLite.
