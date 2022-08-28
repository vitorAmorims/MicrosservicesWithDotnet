# Play.Infra

Infra based for the Play Economy system.

## Prerequisites

- [Docker version 20.10.7](https://www.docker.com/get-started/)

## To build the docker-compose

Run this at the root of this repository:

```
docker-compose up -d
```
## Stop the application containers using docker-compose stop:

```
docker-compose stop
```

## Remove the application containers using docker-compose rm -f:

```
docker-compose rm -f
```

## Status of Docker Containers

```
docker-compose ps
```

## To start redis

```
docker run --name redis-server -it redis 
```


## Learn More

You can learn more in the [Docker Desktop overview](https://docs.docker.com/desktop/).

To learn Docker, check out the [Docker documentation](https://docs.docker.com/desktop/).
