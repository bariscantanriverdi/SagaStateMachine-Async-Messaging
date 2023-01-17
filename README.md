# Saga State Machine & MassTransit Automatonymous & Request Response Pattern
This sample project was developed with .NET Core 6.0, where I used RabbitMQ as the message broker and MongoDB to persist the state machine data.
It includes MassTransit Automatonymous &amp; Request/Response Pattern &amp; Basic Order Management Flow implementations.

# Installation

This project needs MongoDB and RabbitMQ to run correctly. Hence, you can install these as Docker containers or directly to your local.

### Docker Installation

https://www.docker.com/products/docker-desktop

RabbitMQ: docker run -d -p 15672:15672 -p 5672:5672 --name rabbitmq rabbitmq:3-management

MongoDB: docker run -d -p 27017:27017 --name mongodb mongo:latest


### For Direct Installation

RabbitMQ: https://www.rabbitmq.com/download.html

MongoDB: https://www.mongodb.com/try/download/community



-----------

Happy Coding...

----------------------------------------------------------------------------------------------------------------------------------------


![.Net Core](https://www.gezginler.net/indir/resim-grafik/net-core-desktop-runtime-1603015250.png)
![RabbitMQ](https://sekolahlinux.com/wp-content/uploads/2018/02/d50b96ee-666d-43ce-8d91-d0cbb6f93ffb-rabbitmq.png)
![MongoDB](https://api.civo.com/k3s-marketplace/mongodb.png)

