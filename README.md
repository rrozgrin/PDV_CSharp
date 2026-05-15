# Roteiro Inicial de Migracao do PDV para C# (MVP Evolutivo)

## 1. Objetivo
Este documento define um plano pratico para construir um MVP de PDV em C# com arquitetura profissional, pronto para evoluir sem retrabalho pesado.

Meta principal:
- colocar um fluxo real de venda para rodar;
- manter separacao de camadas desde o inicio;
- evoluir por fases curtas e entregaveis;
- preparar base para migracao gradual do sistema legado.

## 2. Estrategia: MVP Primeiro, Evolucao Continua Depois
Decisao de produto e tecnica:
1. iniciar com escopo reduzido e funcional;
2. validar operacao local ponta a ponta;
3. publicar versoes pequenas no Git;
4. ampliar modulos de forma incremental.

Principio:
- nao copiar toda a complexidade do legado no inicio;
- extrair apenas o "coracao" da operacao e expandir com seguranca.

## 3. Escopo do MVP Atual
### Dentro do escopo
1. cadastro local de produto, variacao e grade;
2. abertura de venda;
3. adicao e remocao de itens;
4. recalculo de subtotal, desconto e total;
5. persistencia local em SQLite;
6. organizacao por camadas (`WinForms`, `Application`, `Domain`, `Infrastructure`).

### Fora do escopo por enquanto
1. fiscal completo;
2. todos os modulos do ERP;
3. sincronizacao completa com API;
4. automacoes avancadas de retaguarda;
5. regras complexas de caixa e conciliacao.

## 4. Arquitetura e Responsabilidades
1. `PDV.WinForms`
- interface do operador;
- eventos de tela e exibicao de dados;
- sem regra critica de negocio.

2. `PDV.Application`
- casos de uso (`UseCases`);
- orquestracao do fluxo;
- contratos de repositorio (`Interfaces`).

3. `PDV.Domain`
- entidades e regras de negocio puras;
- calculos e validacoes principais.

4. `PDV.Infrastructure`
- SQLite e acesso a dados;
- implementacao de repositorios;
- integracoes externas futuras.

Fluxo padrao:
`WinForms -> Application -> Domain -> Infrastructure`

## 5. Modelo Atual de Dominio (MVP)
### Produtos
1. `Produto` (pai)
2. `ProdutoVariacao` (SKU vendavel: codigo de barras, preco, estoque)
3. `VariacaoGrade` (atributos da variacao: peso/cor/tamanho)

### Vendas
1. `Venda`
2. `VendaItem`

Regra importante ja definida:
- baixa de estoque acontece no fechamento da venda (`FINALIZADA`), nao na adicao de item.

## 6. Banco Local Atual (SQLite)
Tabelas ja modeladas:
1. `produto`
2. `produto_variacao`
3. `variacao_grade`
4. `venda`
5. `venda_item`

Tabela futura:
1. `venda_pagamento` (fase de `FecharVenda`)

## 7. Estado Atual do Projeto
Ja concluido:
1. solucao em camadas criada;
2. entidades principais de produto e venda modeladas;
3. repositorios iniciais implementados;
4. caso de uso `AdicionarItemVendaUseCase` criado;
5. base SQLite criada com dados de exemplo.

Em andamento:
1. ligar fluxo completo no `FrmFrenteCaixa`;
2. validar ciclo de venda em execucao real.

## 8. Proximos Passos Prioritarios
1. finalizar `FrmFrenteCaixa` minimo (leitura de codigo, grid, total);
2. criar fluxo `FecharVenda` com transacao;
3. incluir `venda_pagamento`;
4. baixar estoque apenas ao finalizar venda;
5. registrar logs operacionais essenciais;
6. preparar README tecnico para portfolio.

## 9. Roadmap de Evolucao
### Fase 1 - MVP Operacional
- venda local funcional com itens e totalizacao.

### Fase 2 - Fechamento de Venda
- pagamento, finalizacao, baixa de estoque, consistencia transacional.

### Fase 3 - Caixa
- abertura/fechamento de caixa e movimentos.

### Fase 4 - Integracao
- sincronizacao com API e tratamento de reenvio.

### Fase 5 - Ampliacao
- estoque completo, clientes, financeiro e demais frentes.

## 10. Definicao de Pronto do MVP
O MVP sera considerado pronto quando:
1. for possivel abrir venda, adicionar itens e persistir corretamente;
2. totais forem calculados no dominio com consistencia;
3. arquitetura permanecer limpa e desacoplada;
4. houver versao executavel demonstravel;
5. codigo estiver versionado com historico de evolucao no Git.

---

## Observacao Final
Este projeto nasce como MVP por estrategia, nao por improviso.
A intencao e construir uma base enxuta, profissional e evolutiva para migrar gradualmente o sistema legado com menos risco e melhor qualidade tecnica.
