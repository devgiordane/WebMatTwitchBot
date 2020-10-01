# WebMatTwitchBot
Um simples bot para automatizar o chat do seu canal na twitch.

## Considerações:
  A finalidade deste bot é trazer uma maior proximidade entre o público da twitch e a linguagem de programação C#
  O bot, por padrão, é o mesmo usuário do canal que se deseja o bot.
  Existem dois modelos de Text to Speech (TTS) (texto para fala):
    A - Comando !Speak chama o power shell e utiliza o idioma padrão do seu windows para reproduzir a fala.
    B - Comando !SpeakPortuga chama o azure serviços cognitivos para reproduzir a fala (necessita configuração)

## Primeiro run
  Por segurança, o arquivo Parameters.cs vem com seus campos em branco. É necessário fazer o preenchimento antes de rodar o bot.
  
  1 - Para receber o valor da propriedade oauth, acesse https://twitchapps.com/tmi/ e conecte-se utilizando a conta que deseja ter o botchat.<br/>
  2 - Copie o codigo mostrado no item anterior e cole no campo oauth, no campo user preencha com o mesmo usuario utilizado anteriormente.
  B - Caso você deseja utilizar o !speakportuga (azure serviços cognitivos) é necessário ir em : www.portal.azure.com, cadastrar-se, e instalar Serviço Cognitivo.
      Depois de Instalado, pegar a key do serviço cognitivo e configurar o Parameters.cs;
  
## Customização
  Você pode customizar os comandos e respostas do bot indo até Commands.cs adicionando um novo item no campo List. (Se você sentir alguma dificuldade consulte itens já inseridos)
  Você pode customizar os registros em cache indo até Cache.cs. (Vide exemplo entre Cache.cs e Commands.cs)
  Por padrão o Text-to-Speech vem desabilitado, para habilita-lo digite no console !setspeaker true
  
## Considerações finais
  O processo de criação do bot foi todo produzido em live no canal www.twitch.tv/webmat1. Qualquer dúvida, sugestão e/ou reclamação, pode ser encaminhado pela twitch também.
  Agradaço a todos que participaram do processo de criação e tiveram compreensão da não complexidade deste bot, pois a finalidade maior é torná-lo um caminho acessivel ao C#.
  
  
  
Just a bot to respond your twitch chat.

## Firstly
  The main idea is turning people on twitch next to programmming language c#.
  By default, our bot uses the same userbot and channel target.
  There are two ways to use Text-to-Speech (TTS):
    A - Command !Speak, this uses the power shell to call all librarys. So its works fine on windows
    B - Command !SpeakPortuga, this uses azure cognitive services to do a machine speak

## First run
  For security, Parameters.cs file has your fields empty. Its necessary fill it before first run.

  1 - To get the value to fill oauth field, access https://twitchapps.com/tmi/ and click on Connect button.<br/>
  2 - Copy the code on before step and fill oauth field. user field must be filled with the same user used before.
  B - If you are going to use !SpeakPortuga (azure cognitive services) you need get key and location from www.portal.azure.com; Maybe you should sign-in and install the cognitive service.

## Customization
  You can change all commands in Commands.cs, List field. (You can see examples there)
  You can change all cached items in Chache.cs. (Its good to insert a command in Commands.cs as well)
  By default TTS is disabled, to enable you must type "!setspeaker true" on console.

## Final Considerations
  This bot was built on stream in www.twitch.tv/webmat1. Feel free to send your suggestion there.
  Thank's to all people no stream who gave me feedback, suggestions and fixes.
