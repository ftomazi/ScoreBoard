Solu��o do painel de score.

Tech stack:
1. Web API  para servi�os REStful  (foi utlizado aplica��o MVC web API tradicional pra usar o MSmQ, .Net Core poderia ser utlizado se outro servi�o de mensageria fosse ussado ex.Rabbit)
2. Servi�o de Mensageria MSMQ para armazenamento de mensagem (receber pontua��es do jogadore) com alta escalabilidade gerenciaod pelo SO
3. Dapper como um Micro-ORM para desempenho e simplicidade em banco.
4. banco foi utilizado a instancia local do sql express claro que um banco SQL / oracle / portgree mysql s�o mais indicados.
5. um listener applica��o console (n�o � melhor forma mas, escolhido pra ser simples, rapido e facil pra demosntrar, ideal um windows servi�e responsabel por processar as mensagens da mensageria, buscando a pontua��o do jogador agregando os novos pontos e atualizando os pontos na base com intru��o atomica), este servi�o de leitura da mensageria pode ser escalavel tanto horizontalmente quando verticalmente.

Modelagem:
1. servi�o web API recebe os dados no endpoint e inclui no servi�o de mensageria as pontua��es. (opera��o simples de inser��o na fila, � rapida e escalavel pelo SO)
2. um listener (CLI) � encarregado de ler as mensagem e atualizar a pontua��o no banco. (configurado para rodar de tempo em tempo com timer (20 segundos pra poder ver na lista do windows) (timer simples pra deixar acumular, ideal nao ter este tempo consumir direto o q tem na fila)
3. a consulta do painel � apenas top 100 decrecente utilizando dapper micro-ORM para desempenho. isto torna o armazenamento do banco bem otimizado, banco pequeno com os resultados ja calculados deixa a consulta do painel bem leve.
4. o indice da ordena��o decrcente pode ser criado p deixar consulta mais rapida.
5. Inje��o de dependencia (Unity nas controllers) utilizado para desacoplar e poderem ser feitos teste unit�rios (ficou incompleto por tempo mesmo)


Modo de uso:
1. rode o projeto ScoreBoard e os endpoints ja est�o prontos, a pagina padr�o do .net mvc vai abrir por default.
2. os endpoint est�o pronto a serem consumidos (consultar o painel: url: localhost/api/leaderboard, trar� o o JSON do painel atual
3. podem ser incluidas novas pontua��es no endpoint: localhost/api/gameResult/ com o post do JSON:  ex. { PlayerId : 1,GameId : 1,Win : 1,TimeStamp : "2017-11-01"} (otulizado individual pra escalar pelo SO mesmo)
4. observa��o: assim que chamada o endpoint de inclus�o de pontua��o ela ainda nao aparece no painel, precisamos que o listener de processamento esteja rodando e ja processado a mensagem para visualizarmos.
5. podemos rodar o projeto com inicializa��o do console Listener e ele far� a totaliza��o das novas pontua��es com painel.
6. apos rodar o listener o painel ja esta atualizado.
7. o listener pode ser chamado direto do executavel na pasta de compila��o assim tudo roda junto e poder ver todo ambiente rodando (cuidar p o listener nao bloquear as dlls e impedir que o site funcione ao rodar simultaneos) em um plano real cada um tera seu ambiente.

obs:
- n�o foi preocupado com armazenar historico dos pontos.
- os SQLs das consultas est�o em codigo bem improtante q estivesse em arquivos SQL para manuten��o (foi tempo mesmo)
- os unitarios nao sairam por tempo tambem.

- tem o script no projeto q cria o banco
- a connection string precisa ser ajustando p o local do banco, ela esta no contrutor da classe ScoreRepository, nao pe melhor caso, mas para um ambiente de DSV local ajuda na setup do projeto ideal vir do configs do proj.


