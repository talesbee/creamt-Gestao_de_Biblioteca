# Gestão de Biblioteca
Projeto completo em C#, SQL, com interface, banco e API de um sistema para Gerenciamento de uma Biblioteca.
## Objetivo
Esse presente projeto tem como objetivo criar um sistema para gerenciamento de uma biblioteca. A proposta inicial é desenvolver um sistema que contenha as seguintes características:
Banco de Dados gerenciado por uma API;
Página Web para monitoramento do Status de um empréstimo, com gerenciamento de Login;
## Metodologia
O primeiro passo é projetar o Banco de Dados. Para isso, utilizando uma ferramenta DBDesigner, foi modelado o banco. Foi escolhida essa ferramenta por ela ser de licença livre e possibilitar a criação de toda documentação do banco de dados de forma automática.
Link para a documentação gerada pela ferramenta 
Projeto do banco: https://dbdesigner.page.link/w6Hs9zZtjvk3p1Dj7 
## Execução
Como um dos requisitos era usar SQL Server, em vez de criar um “localbd” temporário, decidi criar um banco. Assim, ao executar o projeto, se for em modo Debug, usará um banco temporário, mas se for Release o código tentará acessar o banco de produção.
Dessa forma, em modo Release, se faz necessário configurar o arquivo “appsettings.json”, mudando os dados: “Server”, “User Id” e “password”. Em seguida, deve-se, caso ainda não possua o banco criado, rodar o seguinte comando no console do Gerenciador de Pacotes:

Add-Migration InitialCreate
Update-Database

Para facilitar o uso, o banco já é populado com algumas informações, como por exemplo as situações dos Livros, tipos de pessoas, situação do empréstimo e um usuário inicial (Usuário: admin, Senha: 123).
## Uso
Depois de criar o banco e rodar o projeto, a primeira tela é a do Login. Com os dados já pré-preparados: Usuário: admin e Senha: 123, ao logar, vamos para a tela inicial. 

Temos mais duas telas principais: Movimentação e Cadastros. 

Na tela Movimentação, fica especificamente os registros de Movimentações/Empréstimos e todos os outros cadastros do banco ficam disponíveis pela tela Cadastros.
 
#### Primeiros cadastros
No centro de todo o banco está o cadastro de pessoas. Livros precisam dos autores e para os empréstimos precisamos da pessoa a pegar o livro e da pessoa a emprestar o livro.

Então para começar temos que cadastrar ao menos 2 pessoas, uma de qualquer tipo e outra como "Autor". 

As Tabelas de Tipo de Pessoa, Situação dos empréstimos ou do Livro, como possuíam valores pré-determinados
já foram populadas, contudo, ainda se é possível adicionar mais valores. 

Depois do cadastro de Pessoas, vem o cadastro de Categorias. 

Com o cadastro feito, próximo passo é cadastrar um Livro. 

Por fim, já é possível registrar uma movimentação.

#### Movimentando Livros
A usabilidade foi pensada da seguinte forma: 
- Se faz um cadastro de movimentação;
- A data máxima de devolução é definida pela quantidade de dias que aquele tipo de pessoa pode ficar com o livro.

Assim, a data de entrega é a princípio a data máxima. Contudo, caso ao editar uma movimentação, seja adicionada uma data de entrega, essa data de entrega fica registrada.

A situação da movimentação é definida automaticamente seguindo a seguinte lógica:
- Se não há registro na data de entrega -> Se a data de empréstimo é menor ou igual a data máxima de entrega -> livro "Emprestado";
- Se não há registro na data de entrega -> Se a data de empréstimo é maior do que a data máxima de entrega -> livro "Atrasado";
- Se há registro na data de entrega -> Livro "Devolvido";


## Dificuldades
Como meu primeiro projeto na vida, começado do zero e nesse nível de complexidade, me senti um aluno no fundamental ouvindo pela primeira vez algo chamada "tabuada” e logo em seguida sendo cobrada a tabuada do 3455. 

Apanhei de mais para conceitos simples, tive que ler muita documentação para entender o funcionamento de coisas que na verdade eram bem simples. Aprendi muita coisa nesses dias e consegui evoluir muito meu C# Web. 

No que levei algumas horas para fazer a parte do Banco, comunicação, Migração e Update, levei dias para conseguir trabalhar com as Telas. 

Quase Abandonei as telas do MVC e parti para o React, Com a API funcionando era simples fazer tudo que foi pedido. Mas me mantive firme!

## Conclusão
Essa foi a primeira vez que crio algo desse tipo em C#, toda minha experiencia até então foi dar manutenção em sistemas legado.

Passei mais da metade do tempo que precisei para criar o sistema lendo documentações, mas foi muito gratificante ver tudo funcionando. 

Queria ter implementado um sistema de autenticação com OAuth2, ter implementado uma API mais sólida. Não consegui também fazer uma divisão melhor das funções e serviços (separado o serviço Front-End do Back-End, para facilitar possíveis manutenções), para assim utilizar a metodologia DDD em toda sua glória.

Mas me sinto satisfeito com o que consegui fazer dentro do prazo definido. 

Em continuidade ao projeto, eu implementaria níveis de permissão aos usuários, onde um Gerente ou Administrador além do acesso as telas bases do sistema (Movimentação e Cadastro de itens), teria acesso a uma tela de cadastro de usuários, um Funcionário veria as telas base do sistema e um Usuário veria apenas a tela Movimentações, onde já existiria um filtro para mostrar só as movimentações que está relacionada a ele. 

Eu teria feito a API separada do Web se tivesse mais tempo, com certeza usaria o Swagger, senti muita falta dele no desenvolvimento, facilita muito ter a visão dos métodos e seus exemplos de uso e até mesmo testar rapidamente.
