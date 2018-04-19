Solução do painel de score.

Tech stack:
1. Web API  para serviços REStful  (foi utlizado aplicação MVC web API tradicional pra usar o MSmQ, .Net Core poderia ser utlizado se outro serviço de mensageria fosse ussado ex.Rabbit)
2. Serviço de Mensageria MSMQ para armazenamento de mensagem (receber pontuações do jogadore) com alta escalabilidade gerenciaod pelo SO
3. Dapper como um Micro-ORM para desempenho e simplicidade em banco.
4. banco foi utilizado a instancia local do sql express claro que um banco SQL / oracle / portgree mysql são mais indicados.
5. um listener applicação console (não é melhor forma mas, escolhido pra ser simples, rapido e facil pra demosntrar, ideal um windows serviçe responsabel por processar as mensagens da mensageria, buscando a pontuação do jogador agregando os novos pontos e atualizando os pontos na base com intrução atomica), este serviço de leitura da mensageria pode ser escalavel tanto horizontalmente quando verticalmente.

Modelagem:
1. serviço web API recebe os dados no endpoint e inclui no serviço de mensageria as pontuações. (operação simples de inserção na fila, é rapida e escalavel pelo SO)
2. um listener (CLI) é encarregado de ler as mensagem e atualizar a pontuação no banco. (configurado para rodar de tempo em tempo com timer (20 segundos pra poder ver na lista do windows) (timer simples pra deixar acumular, ideal nao ter este tempo consumir direto o q tem na fila)
3. a consulta do painel é apenas top 100 decrecente utilizando dapper micro-ORM para desempenho. isto torna o armazenamento do banco bem otimizado, banco pequeno com os resultados ja calculados deixa a consulta do painel bem leve.
4. o indice da ordenação decrcente pode ser criado p deixar consulta mais rapida.
5. Injeção de dependencia (Unity nas controllers) utilizado para desacoplar e poderem ser feitos teste unitários (ficou incompleto por tempo mesmo)


Modo de uso:
1. rode o projeto ScoreBoard e os endpoints ja estão prontos, a pagina padrão do .net mvc vai abrir por default.
2. os endpoint estão pronto a serem consumidos (consultar o painel: url: localhost/api/leaderboard, trará o o JSON do painel atual
3. podem ser incluidas novas pontuações no endpoint: localhost/api/gameResult/ com o post do JSON:  ex. { PlayerId : 1,GameId : 1,Win : 1,TimeStamp : "2017-11-01"} (otulizado individual pra escalar pelo SO mesmo)
4. observação: assim que chamada o endpoint de inclusão de pontuação ela ainda nao aparece no painel, precisamos que o listener de processamento esteja rodando e ja processado a mensagem para visualizarmos.
5. podemos rodar o projeto com inicialização do console Listener e ele fará a totalização das novas pontuações com painel.
6. apos rodar o listener o painel ja esta atualizado.
7. o listener pode ser chamado direto do executavel na pasta de compilação assim tudo roda junto e poder ver todo ambiente rodando (cuidar p o listener nao bloquear as dlls e impedir que o site funcione ao rodar simultaneos) em um plano real cada um tera seu ambiente.

obs:
- não foi preocupado com armazenar historico dos pontos.
- os SQLs das consultas estão em codigo bem improtante q estivesse em arquivos SQL para manutenção (foi tempo mesmo)
- os unitarios nao sairam por tempo tambem.

- tem o script no projeto q cria o banco
- a connection string precisa ser ajustando p o local do banco, ela esta no contrutor da classe ScoreRepository, nao pe melhor caso, mas para um ambiente de DSV local ajuda na setup do projeto ideal vir do configs do proj.


