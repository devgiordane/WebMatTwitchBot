# WebMatTwitchBot
Um simples bot para automatizar o chat do seu canal na twitch.

Considerações:
  A finalidade deste bot é trazer uma maior proximidade entre o público da twitch e a linguagem de programação C#
  O bot, por padrão, é o mesmo usuário do canal que se deseja o bot.

Primeiro run
  Por segurança, o arquivo Parameters.cs vem com seus campos em branco. É necessário fazer o preenchimento antes de rodar o bot.
  
  1 - Para receber o valor da propriedade oauth, acesse https://twitchapps.com/tmi/ e conecte-se utilizando a conta que deseja ter o botchat.
  2 - Copie o codigo mostrado no item anterior e cole no campo oauth, no campo user preencha com o mesmo usuario utilizado anteriormente.
  
Customização
  Você pode customizar os comandos e respostas do bot indo até Commands.cs adicionando um novo item no campo List. (Se você sentir alguma dificuldade consulte itens já inseridos)
  Você pode customizar os registros em cache indo até Cache.cs. (Vide exemplo entre Cache.cs e Commands.cs)
  
Considerações finais
  O processo de criação do bot foi todo produzido em live no canal www.twitch.tv/webmat1. Qualquer dúvida, sugestão e/ou reclamação, pode ser encaminhado pela twitch também.
  Agradaço a todos que participaram do processo de criação e tiveram compreensão da não complexidade deste bot, pois a finalidade maior é torná-lo um caminho acessivel ao C#.
  
  
Just a bot to respond your twitch chat.

Firstly
  The main idea is turning people on twitch next to programmming language c#.
  By default, our bot uses the same userbot and channel target.

First run
  For security, Parameters.cs file has your fields empty. Its necessary fill it before first run.

  1 - To get the value to fill oauth field, access https://twitchapps.com/tmi/ and click on Connect button.
  2 - Copy the code on before step and fill oauth field. user field must be filled with the same user used before.

Customization
  You can change all commands in Commands.cs, List field. (You can see examples there)
  You can change all cached items in Chache.cs. (Its good to insert a command in Commands.cs as well)

Final Considerations
  This bot was built on stream in www.twitch.tv/webmat1. Feel free to send your suggestion there.
  Thank's to all people no stream who gave me feedback, suggestions and fixes.
