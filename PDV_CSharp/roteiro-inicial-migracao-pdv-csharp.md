# Roteiro Inicial de Migracao do Core de Frente de Caixa (C#)

## 1. Objetivo
Este documento define uma trilha reduzida e executavel para iniciar a migracao do core de Frente de Caixa para C#, com baixo risco operacional e evolucao por etapas.

Foco desta fase:
- estabilizar a base tecnica do novo PDV;
- validar arquitetura e fluxo de venda local;
- preparar integracao progressiva com APIs existentes;
- garantir operacao local confiavel mesmo com indisponibilidade temporaria de servicos externos.

## 2. Escopo Inicial (Fase 1)
Implementar apenas o nucleo operacional:
1. contexto de loja/tenant (`id_cadastro`, `codloja`);
2. cadastro local de produtos e clientes com estrutura minima;
3. abertura e fechamento de caixa em fluxo simplificado;
4. criacao de venda local com itens, total, desconto, pagamento e troco;
5. persistencia transacional da venda;
6. trilha de logs, auditoria basica e eventos operacionais;
7. estrutura pronta para sincronizacao futura sem refatoracao estrutural.

Fora de escopo desta fase:
- cobertura fiscal completa;
- substituicao de sistema em producao;
- todos os modulos do ERP;
- automacao completa de retaguarda;
- sincronizacao bidirecional complexa.

## 3. Arquitetura Recomendada
Adotar separacao por camadas desde o inicio, respeitando o estilo do ecossistema .NET desktop:

1. `PDV.WinForms`
- interface do operador;
- formulários, navegacao e binding;
- sem regra critica embutida no formulario;
- preferir composicao com presenters, view models ou handlers de tela leves.

2. `PDV.Application`
- casos de uso;
- orquestracao do fluxo;
- servicos de aplicacao;
- DTOs de entrada e saida;
- contratos para repositorios, mensageria, logging e integracoes.

3. `PDV.Domain`
- entidades e regras de negocio puras;
- agregados;
- value objects;
- enums e politicas de negocio;
- invariantes de venda e caixa.

4. `PDV.Infrastructure`
- banco local;
- estrategia de persistencia (`Entity Framework Core`, `Dapper` ou modelo hibrido);
- implementacao de repositorios;
- integracao com APIs;
- logging;
- fila local persistida para sincronizacao futura.

Diretriz:
- o fluxo principal deve seguir `WinForms -> Application -> Domain -> Infrastructure`.
- em contexto desktop, pensar em `Controller` como coordenacao de tela e casos de uso, nao como controller HTTP.

## 4. Estrutura de Solucao (Sugestao)
```text
pdv-solution/
  src/
    PDV.WinForms/
    PDV.Application/
      UseCases/
      Services/
      Contracts/
      DTOs/
    PDV.Domain/
      Entities/
      Aggregates/
      ValueObjects/
      Enums/
    PDV.Infrastructure/
      Persistence/
      Repositories/
      Integrations/
      Logging/
      Queue/
  tests/
    PDV.Domain.Tests/
    PDV.Application.Tests/
    PDV.Infrastructure.Tests/
  docs/
    arquitetura.md
    contratos.md
    fluxo-venda.md
    checklist-execucao.md
```

Observacao:
- criar um projeto separado como `PDV.CrossCutting` apenas se surgir necessidade real;
- no inicio, manter a solucao enxuta ajuda mais do que superfragmentar.

## 5. Dominio Inicial e Agregados
Modelar explicitamente os principais conceitos de negocio:
1. `TenantContext`
2. `Produto`
3. `Cliente`
4. `Caixa`
5. `Venda`
6. `VendaItem`
7. `PagamentoVenda`

Diretriz de modelagem:
- tratar `Venda` como agregado principal;
- `VendaItem` e `PagamentoVenda` devem ser alterados por comportamento da propria venda;
- calculo de subtotal, desconto, total, valor recebido e troco deve ficar no dominio;
- evitar espalhar regras de fechamento de venda em tela, service e repositorio ao mesmo tempo.

## 6. Banco Local (Fase Inicial)
Criar modelo local enxuto:
1. `tenant_context`
2. `produtos_local`
3. `clientes_local`
4. `caixa_movimento`
5. `venda_cabecalho`
6. `venda_item`
7. `venda_pagamento`
8. `logs_operacionais`
9. `fila_integracao`
10. `outbox_eventos`

Diretrizes obrigatorias:
- toda tabela de negocio deve armazenar `id_cadastro` e `codloja`;
- toda venda deve possuir identificador local unico e `external_id` preparado para sincronizacao;
- registrar `schema_version` nos contratos e mensagens relevantes;
- prever campos de auditoria minima como criacao, atualizacao e status operacional.

Escolha de persistencia:
- se a equipe quer produtividade e mapeamento forte de entidades, iniciar com `Entity Framework Core`;
- se a equipe quer consultas mais controladas e SQL explicito, iniciar com `Dapper`;
- um modelo hibrido tambem faz sentido: EF Core para escrita transacional e Dapper para consultas e relatorios locais.

## 7. Fluxos Prioritarios
1. selecionar tenant e loja;
2. abrir caixa;
3. localizar produto;
4. adicionar e remover item;
5. recalcular total da venda;
6. receber pagamento;
7. validar fechamento;
8. finalizar venda local em transacao;
9. registrar evento operacional;
10. enfileirar sincronizacao futura;
11. emitir comprovante interno sem fiscal avancado nesta etapa.

## 8. Organizacao de Codigo
Padrao recomendado:
1. `UseCase` ou `Application Service` para regra de processo;
2. `Repository` para persistencia e consulta;
3. `DTO` para fronteiras de entrada e saida;
4. mapeamento explicito, sem expor entidades diretamente em integracao;
5. contratos (`interfaces`) definidos em `Application` para desacoplar implementacoes de infraestrutura.

Traducao pratica da estrutura que voce usa no Laravel:
- `Controller` vira camada de tela, presenter ou coordenacao do formulario;
- `Service` continua fazendo muito sentido em `Application`;
- `Repository Contract` vira interface como `IVendaRepository`;
- `Repository Eloquent` vira implementacao concreta em `Infrastructure`.

## 9. Integracao Progressiva com API (Fase 2)
Apos estabilizar a Fase 1, iniciar integracao externa:
1. mapear endpoints ja existentes no ecossistema atual;
2. criar cliente HTTP dedicado por contexto funcional;
3. sincronizar catalogos como produtos, clientes e precos;
4. enviar vendas finalizadas via fila local persistida e outbox;
5. aplicar idempotencia com `external_id` por venda;
6. implementar retry com backoff, rastreabilidade e status de reprocessamento.

Diretriz operacional:
- o PDV nao deve depender de disponibilidade imediata da API para concluir a venda local;
- a sincronizacao deve ser desacoplada do fechamento da venda.

## 10. Concorrencia, Consistencia e Recuperacao
Desde o inicio, considerar regras operacionais que costumam quebrar PDV quando ficam implicitas:
1. impedir mais de um caixa aberto por contexto quando essa for a regra do negocio;
2. garantir numeracao local consistente para venda e movimento;
3. tratar retomada segura apos queda de energia ou fechamento abrupto;
4. permitir reprocessamento idempotente de integracoes pendentes;
5. registrar estados intermediarios de venda e sincronizacao.

## 11. Logs, Eventos e Observabilidade
Separar claramente:
1. logs tecnicos de erro e diagnostico;
2. eventos operacionais de negocio;
3. auditoria minima de alteracoes sensiveis.

Eventos importantes desde a fase inicial:
1. abertura de caixa;
2. fechamento de caixa;
3. item adicionado;
4. item removido;
5. desconto aplicado;
6. venda finalizada;
7. tentativa de sincronizacao;
8. falha de sincronizacao;
9. reenvio concluido.

## 12. Backup e Recuperacao
Mesmo em fase inicial, incluir plano basico para o banco local:
1. definir rotina simples de backup;
2. validar restauracao em ambiente controlado;
3. documentar local de armazenamento;
4. prever estrategia para corrupcao ou perda parcial do banco local.

## 13. Sequencia de Execucao (8 Semanas)

### Semanas 1-2
1. montar solucao e projetos;
2. definir estrategia de persistencia (`EF Core`, `Dapper` ou hibrido);
3. configurar banco local;
4. implementar entidades, agregados e casos de uso base.

### Semanas 3-4
1. implementar fluxo completo de venda local;
2. implementar abertura e fechamento de caixa;
3. criar logs, eventos operacionais e tratamento de erro.

### Semanas 5-6
1. criar DTOs e contratos de integracao;
2. adicionar cliente API;
3. sincronizar dados de catalogo;
4. estruturar fila local persistida.

### Semanas 7-8
1. implementar envio de vendas por outbox;
2. adicionar retry, idempotencia e reprocessamento;
3. validar retomada, consistencia e fluxo ponta a ponta em homologacao.

## 14. Boas Praticas Obrigatorias
1. transacao no fechamento de venda;
2. versionamento de contrato com `schema_version`;
3. timeout e retry com backoff para API;
4. logs estruturados com identificador da venda;
5. testes unitarios de calculo e regras principais;
6. separacao estrita entre UI e regra de negocio;
7. fila persistida para integracoes assincronas;
8. tratamento explicito de idempotencia;
9. nao depender de API online para concluir a venda local.

## 15. Riscos e Mitigacoes
1. **Acoplamento da regra a UI**
- Mitigacao: centralizar regra em `Application` e `Domain`.

2. **Duplicidade em integracao futura**
- Mitigacao: idempotencia com `external_id` e controle de reenvio.

3. **Perda de contexto multi-tenant**
- Mitigacao: `id_cadastro` e `codloja` obrigatorios em todos os fluxos e tabelas.

4. **Escopo excessivo no inicio**
- Mitigacao: limitar entrega ao core local definido na Fase 1.

5. **Dependencia operacional de servico externo**
- Mitigacao: desacoplar sincronizacao da finalizacao da venda.

6. **Falha local sem recuperacao simples**
- Mitigacao: prever backup, retomada e fila persistida desde o desenho inicial.

## 16. Definicao de Pronto da Fase Inicial
A fase inicial estara pronta quando:
1. venda local fechar com consistencia;
2. caixa abrir e fechar corretamente;
3. dados persistirem com contexto tenant;
4. erros criticos e eventos operacionais ficarem rastreaveis;
5. a arquitetura estiver preparada para integrar API sem refatoracao estrutural;
6. houver caminho claro para reprocessar sincronizacoes pendentes;
7. a operacao local continuar funcional mesmo com indisponibilidade temporaria da API.

---

## Observacao
Este roteiro foi desenhado para permitir evolucao tecnica controlada, com baixo risco e base solida para as proximas fases de migracao. A prioridade desta etapa e provar consistencia operacional local, nao reproduzir toda a complexidade do ecossistema legado de uma vez.
