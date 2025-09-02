
# Shopping Online

- Aplicação que visa representar um sistema de e-commerce (comércio online), voltada para o cliente que deseja adquirir um produto e para a parte gerencial, com o controle de pedidos, produtos e cupons de desconto;

- Uma aplicação com frontend (3 projetos) e backend (9 projetos), desenvolvida inteiramente com solução Microsoft, em .NET Core 8; 

- O detalhamento mais técnico de cada um dos projetos, tanto frontend quanto backend, bem como as tecnologias empregadas em cada um deles, serão descritos nas seções a seguir.



## Backend
O backend foi desenvolvido em .NET Core 8. Ele é composto por:

### 1- Microserviços:
- Autenticação, usando Microsoft Identity;
- Email, usando serviço do google para gerenciamento emails;
- Produto, usando AWS S3 para armazenamento de arquivos de imagens;
- Cupons de desconto;
- Carrinho de compras;
- Recompensas;
- Pedido;
#### O banco de dados utilizados nos microserviços foi o Sql Server e como ORM foi empregado o Entity Framework Core (com uso de Migrations). 

### 2- Serviço de integração (MessageBus):
Para essa solução, foram utilizados o AWS SQS e o Kafka;

### 3- Gateway: 
Para essa solução, utilizou-se o Ocelot Gateway;

### 4 - Worker Service: 
Voltado para consumo das mensagens produzidas nos tópicos e disparo de email, depois da conclusão de pedido;

Nas Api's, procurou-se seguir os princípios do clean code e do solid com uma arquitetura em camadas;
## Frontend
O frontend consiste de três sistemas:

- Shopping Virtual (e-commerce), voltado para aquisição de produtos. Desenvolvido em Asp Net Core MVC, com Razor Pages;

- Gerenciador de produtos e cupons, voltado para a área administrativa, responsável pelo controle de produtos e cupons a serem utilizados na loja, bem como o gerenciamento de estoque. Desenvolvido em Asp Net Core MVC, com Razor Pages e usando a bibliotaca Refit para a comunicação com o Gateway;

- Gerenciador de pedidos, voltado para a área administrativa, responsável pelo controle e acompanhamento dos pedidos, do momento que é feito até a entrega final, com atulalizações em tempo real de alterações de pedidos.Desenvolvido em Asp Net Core MVC, com Razor Pages e usando a bibliotaca Refit para a comunicação com o Gateway;