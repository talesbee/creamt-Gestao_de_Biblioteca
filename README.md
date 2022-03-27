# Gestão de Biblioteca
Projeto completo em C#, SQL, com interface, banco e API de um sistema para Gerenciamento de uma Biblioteca.

## Objetivo
Esse presente projeto tem como objetivo criar um sistema para gerenciamento de uma 
biblioteca. A proposta incial é desenvolver um sistema que contenha as seguintes 
caracteristicas:
- Interface Gráfica para computador (.exe), com gerenciamento de Login;
- Banco de Dados gerenciado por uma API;
- Pagina Web para monitoramento do Statos de um emprestimo, com gerenciamento de Login;
- Aplicativo mobile para monitoramento de um emprestimo, com gerenciamento de Login;

Para tanto, foi utilizado as seguintes ferramentas:
- MVC 5: Para a interface Gráfica (Computador);
- Projeto Banco de dados: DBDesigner;
- SQL Server: Banco de dados;
- Entity Framework 6: Para criar o Banco;
- DDD: Para a criação dos microserviços (API);
- React: Criação da Pagina Web;
- React Native: Criação do Aplicativo Mobile;

## Metodologia
O primeiro passo é projetar o Banco de Dados. Para isso, utilizando uma ferramenta 
DBDesigner, foi modelado o banco: https://dbdesigner.page.link/w6Hs9zZtjvk3p1Dj7

## Execução

Como um dos requisitos era usar SQL Server, em vez de criar um “localbd” temporário, decidi criar um banco. Assim, ao executar o projeto, se for em modo Debug, usará um banco temporário, mas se for Release o código tentará acessar o banco de produção.
Dessa forma, em modo Release, se faz necessário configurar o arquivo “appsettings.json”, mudando os dados: “Server”, “User Id” e “password”. Em seguida, deve-se, caso ainda não possua o banco criado, rodar o seguinte comando no console do Gerenciador de Pacotes:
Add-Migration InitialCreate
Update-Database
Para facilitar o uso, o banco já é populado com algumas informações, como por exemplo as situações dos Livros, tipos de pessoas, situação do empréstimo e um usuário inicial (Usuário: admin, Senha: 123).

