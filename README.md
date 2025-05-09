
# Teste Técnico – Vequis

Aplicação console desenvolvida em .NET 8 para leitura de arquivos XML de notas fiscais, extração de dados, persistência em banco local (SQLite) e exibição de consultas no terminal.

---

## Tecnologias

- .NET 8
- Entity Framework Core
- SQLite (substituindo H2, com autorização do avaliador)
- System.Xml.Linq (para leitura de XML)

---

##  Como executar

1. Clone o repositório e acesse a pasta do projeto:
   ```bash
   git clone <URL_DO_REPOSITORIO>
   cd Teste_Vequis/LeitorNotasFiscais
   ```

2. Certifique-se de que os arquivos XML estão dentro da pasta `xml/` na raiz do projeto.  
Três arquivos de exemplo já estão disponíveis:

	-   `nota_fiscal_001.xml`
	    
	-   `nota_fiscal_002.xml`
	    
	-   `nota_fiscal_003.xml`

3.  Rode o projeto:
    
    `dotnet run` 
    

> Todas as etapas são executadas automaticamente:
> 
> -   Criação do banco e das tabelas
>     
> -   Inserção de registros iniciais
>     
> -   Leitura dos arquivos XML da pasta `xml/`
>     
> -   Inserção no banco (sem duplicar produtos)
>     
> -   Exibição dos dados e consultas no terminal
>     

----------

## Observações

-   A aplicação segue a estrutura em camadas (Controller, Service, Repository, Models).
    
-   IDs dos clientes iniciam em 100 e dos produtos em 231, conforme exigido.
    
-   Foi usada uma entrada “dummy” para forçar o autoincremento inicial no SQLite.
    
-   O banco H2 foi substituído por SQLite.
    

----------

## Funcionalidades principais

-   Leitura de XMLs com dados de cliente e produtos
    
-   Exibição dos dados extraídos e valor total da compra
    
-   Persistência no banco com controle de duplicação
    
-   Consultas:
    
    -   Cliente + Produto + Valor
        
    -   Total de produtos registrados
        
    -   Aplicação de impostos simulados
        
    -   Valor total por tipo de produto
        
