# Ticketer API

Web API that acts as backend of a ticket management web project.

This app uses an AWS MSSQL database to be accesible out of localhost and the API is hosted in
`http://ticketerapi.eastus.azurecontainer.io/`

The project in this repo has being set up with a local MSSQL database. If you want to run it, go to `TicketerAPI/appsettings.json` and modify `localConnection` value with your own MSSQL connection string.

The API is hosted as a Docker Container in Azure; if you want to run it as a Docker Container, inside `TicketerAPI`folder, there's a Dockerfile that you can build and run with Docker and execute the app containerized.

### Commands to run with Docker
Being on the root folder, where **TicketerAPI.sln** is located:
    
    # cd TicketerAPI
    # docker build -t YOUR_APP_NAME .
    # docker run -p 8080:80 YOUR_APP_NAME 

Your Docker Container Image is now running on http://localhost:8080/.

## Commands to run with dotnet console

Being on the project folder(TicketerAPI):

After changing the connection string in the project with your own connection string in the file `appsettings.json`, to update the database run:
    
    # dotnet ef database update

To execute the API, run:

    # dotnet run


## ROLES

### Get all

`GET /api/roles`

Return a list of all the roles. Returns `200 Ok` if successfull.

#### Response
    [
        {
            "id": 1,
            "nombre": "Admin"
        },
        {
            "id": 2,
            "nombre": "Cliente"
        },
        {
            "id": 3,
            "nombre": "Mantenimiento"
        }
    ]

### Get one
`GET /api/roles/4`

Return one role by its ID. Returns `200 Ok` if successfull.
#### Response
    {
        "id": 4,
        "nombre": "Testtestest"
    }

### Create one
`POST /api/roles`

Create a new role. Returns `201 Created` if successfull.
#### Request
    {
        "nombre": "Testing"
    }
#### Response
    {
        "id": 4,
        "nombre": "Testing"
    }

### Update one
`PUT /api/roles/1`

Updates an existing role. Returns `204 No Content` if successfull.
#### Request
    {
        "nombre": "New value"
    }

### Partial update one
`PATCH /api/roles/1`

Updates fields on an existing role. Returns `204 No Content` if successfull. Receives a JsonPatchDocument.
#### Request
    [
        {
            "op": "replace",
            "path": "/nombre",
            "value": "nuevo nombre"
        }
    ]

### Delete one
`DELETE /api/roles/1`

Deletes an existing role. Returns `204 No Content` if successfull.

    
## PRIORIDAD/STATUS

### Get all

`GET /api/prioridad`

`GET /api/status`

Return a list of all prios/status. Returns `200 Ok` if successfull.

#### Response (Prioridad)
    [
        {
            "id": 1,
            "nombre": "Baja",
            "color": "#90ee90"
        },
        {
            "id": 2,
            "nombre": "Media",
            "color": "#ffed83"
        },
        {
            "id": 3,
            "nombre": "Alta",
            "color": "#ff726f"
        }
    ]

### Get one
`GET /api/prioridad/1`

`GET /api/status/1`

Return one prio/status by its ID. Returns `200 Ok` if successfull.
#### Response (Prioridad)
    {
        "id": 1,
        "nombre": "Baja",
        "color": "#90ee90"
    }

### Create one
`POST /api/prioridad`

`POST /api/status`

Create a new prio/status. Returns `201 Created` if successfull.
#### Request 
    {
        "nombre": "test",
        "color": "#ff720f"
    }
#### Response
    {
        "id": 4,
        "nombre": "test",
        "color": "#ff720f"
    }

### Update one
`PUT /api/prioridad/1`

`PUT /api/status/1`

Updates an existing prio/status. Returns `204 No Content` if successfull.
#### Request
    {
        "nombre": "test",
        "color": "#00072f"
    }


### Delete one
`DELETE /api/prioridad/1`

`DELETE /api/status/1`

Deletes an existing prio/status. Returns `204 No Content` if successfull.

    
## USUARIOS

### Get all

`GET /api/usuarios`

Return a list of all the users. Returns `200 Ok` if successfull.

#### Response
    [
        {
            "id": 3,
            "nombre": "Batman",
            "apellido": "Stark",
            "rolId": 2,
            "rol": {
                "id": 2,
                "nombre": "Cliente"
            }
        },
        {
            "id": 4,
            "nombre": "Robin",
            "apellido": "García",
            "rolId": 2,
            "rol": {
                "id": 2,
                "nombre": "Cliente"
            }
        },
        {
            "id": 5,
            "nombre": "Alberto",
            "apellido": "Peña",
            "rolId": 1,
            "rol": {
                "id": 1,
                "nombre": "Admin"
            }
        }
    ]

### Get one
`GET /api/usuarios/3`

Return one user by its ID. Returns `200 Ok` if successfull.
#### Response
    {
        "id": 3,
        "nombre": "Batman",
        "apellido": "Stark",
        "rolId": 2,
        "rol": {
            "id": 2,
            "nombre": "Cliente"
        }
    }

### Check user
`POST /api/usuarios/check`

Checks if an user exists by its username and password. Returns `200 Ok`.
#### Request
    {
        "nombreUsuario": "testerUser_4",
        "contrasena": "123456"
    }
#### Response if data is correct
    {
        "id": 4,
        "nombre": "Tester",
        "apellido": "User",
        "nombreUsuario": "testerUser_4",
        "contrasena": "123456",
        "rolId": 2,
        "rol": null
    }

### Create one
`POST /api/usuarios`

Create a new user. Returns `201 Created` if successfull.
#### Request
    {
        "nombre": "Test",
        "apellido": "User",
        "rolId": "2"
    }
#### Response
    {
        "id": 13,
        "nombre": "Test",
        "apellido": "User",
        "rolId": 2,
        "rol": null
    }

### Update one
`PUT /api/usuarios/1`

Updates an existing user. Returns `204 No Content` if successfull.
#### Request
    {
        "nombre": "Rodrigo",
        "apellido": "Cabrerita",
        "rolId": "2"
    }

### Partial update one
`PATCH /api/roles/1`

Updates fields on an existing user. Returns `204 No Content` if successfull. Receives a JsonPatchDocument.
#### Request
    [
        {
            "op": "replace",
            "path": "/nombre",
            "value": "nuevo nombre"
        }
    ]

### Delete one
`DELETE /api/usuarios/1`

Deletes an existing user. Returns `204 No Content` if successfull.

## SERVICIOS

### Get all

`GET /api/servicios`

Return a list of all the services. Returns `200 Ok` if successfull.

#### Response
    [
        {
            "id": 1,
            "nombre": "Servicio de reportes",
            "descripcion": "Reporte aquí sus averías",
            "puntuacion": 0.0
        },
        {
            "id": 4,
            "nombre": "Instalación de abanicos",
            "descripcion": "Instale su abanico de techo",
            "puntuacion": 0.0
        },
        {
            "id": 5,
            "nombre": "Limpieza de Piscinas",
            "descripcion": "Limpie su piscina aquí",
            "puntuacion": 0.0
        },
        {
            "id": 6,
            "nombre": "Test",
            "descripcion": "Service Modified 2",
            "puntuacion": 10.0
        }
    ]

### Get one
`GET /api/servicios/1`

Return one service by its ID. Returns `200 Ok` if successfull.
#### Response
    {
        "id": 1,
        "nombre": "Servicio de reportes",
        "descripcion": "Reporte aquí sus averías",
        "puntuacion": 0.0
    }

### Create one
`POST /api/servicios`

Create a new service. Returns `201 Created` if successfull.
#### Request
    {
        "nombre": "Test",
        "descripcion": "Service"
    }
#### Response
    {
        "id": 6,
        "nombre": "Test",
        "descripcion": "Service",
        "puntuacion": 0.0
    }

### Update one
`PUT /api/servicios/1`

Updates an existing service. Returns `204 No Content` if successfull.
#### Request
    {
        "nombre": "Test",
        "descripcion": "Service Modified 2",
        "puntuacion": "5"
    }

### Partial update one
`PATCH /api/servicios/1`

Updates fields on an existing service. Returns `204 No Content` if successfull. Receives a JsonPatchDocument.
#### Request
    [	
        {
            "opt": "replace",
            "path": "/puntuacion",
            "value": "10"
        }
    ]

### Delete one
`DELETE /api/servicios/1`

Deletes an existing service. Returns `204 No Content` if successfull.

## TICKETS

### Get all

`GET /api/tickets`


Return a list of all the tickets. Returns `200 Ok` if successfull.

`GET /api/tickets?filter=usuario&filterId=2`

You can filter by:  
> - `usuario`: Filter by users
> - `servicio`: Filter by services
>> `filterId`: represents the ID of the object to filter by.
    
#### Response
    [
        {
            "id": 7,
            "fechaCreacion": "2020-10-16T00:00:00",
            "usuarioId": 2,
            "usuario": {
                "id": 2,
                "nombre": "Rayni",
                "apellido": "Núñez",
                "rolId": 2,
                "rol": null
            },
            "servicioId": 5,
            "servicio": {
                "id": 5,
                "nombre": "Limpieza de Piscinas",
                "descripcion": "Limpie su piscina aquí",
                "puntuacion": 0.0
            },
            "ticketStatusId": 1,
            "ticketStatus": {
                "id": 1,
                "nombre": "Activo",
                "color": "#fdsadd"
            },
            "prioridadId": 1,
            "prioridad": {
                "id": 1,
                "nombre": "Baja",
                "color": "#90ee90"
            }
        }
    ]

### Get one
`GET /api/tickets/6`

Return one ticket by its ID. Returns `200 Ok` if successfull.
#### Response
    {
        "id": 6,
        "fechaCreacion": "2020-10-16T00:00:00",
        "usuarioId": 1,
        "usuario": {
            "id": 1,
            "nombre": "Ariel",
            "apellido": "Fermin",
            "rolId": 1,
            "rol": null
        },
        "servicioId": 4,
        "servicio": {
            "id": 4,
            "nombre": "Instalación de abanicos",
            "descripcion": "Instale su abanico de techo",
            "puntuacion": 0.0
        },
        "ticketStatusId": 1,
        "ticketStatus": {
            "id": 1,
            "nombre": "Activo",
            "color": "#fdsadd"
        },
        "prioridadId": 1,
        "prioridad": {
            "id": 1,
            "nombre": "Baja",
            "color": "#90ee90"
        }
    }

### Create one
`POST /api/tickets`

Create a new ticket. Returns `201 Created` if successfull.
#### Request
    {
        "fechaCreacion": "2020-10-16",
        "usuarioId": 2,
        "servicioId": 5,
        "prioridadId": 1,
        "ticketStatusId": 1
    }
#### Response
    {
        "id": 7,
        "fechaCreacion": "2020-10-16T00:00:00",
        "usuarioId": 2,
        "usuario": null,
        "servicioId": 5,
        "servicio": null,
        "ticketStatusId": 1,
        "ticketStatus": null,
        "prioridadId": 1,
        "prioridad": null
    }

### Update one
`PUT /api/tickets/1`

Updates an existing ticket. Returns `204 No Content` if successfull.
#### Request
    {
        "fechaCreacion": "2019-11-16",
        "usuarioId": 1,
        "servicioId": 1,
        "prioridadId": 1,
        "ticketStatusId": 1
    }

### Partial update one
`PATCH /api/tickets/1`

Updates fields on an existing ticket. Returns `204 No Content` if successfull. Receives a JsonPatchDocument.
#### Request
    [
        {
            "op": "replace",
            "path": "/ticketStatusId",
            "value": 2
        }
    ]

### Delete one
`DELETE /api/tickets/1`

Deletes an existing ticket. Returns `204 No Content` if successfull.

## CLIENTES

### Get all

`GET /api/clientes`

Return a list of all the clients. Returns `200 Ok` if successfull.

`GET /api/clientes?nombre=comerciante`

You can filter by:  
> - `nombre`: Filter by name of the user. Name can be the incomplete name. Ex., if name is **Joseph**, you can look for **Jose**.


#### Response
    [
        {
            "id": 1,
            "nombre": "Cliente",
            "apellido": "1",
            "nacimiento": "2001-11-12T00:00:00"
        },
        {
            "id": 2,
            "nombre": "Cliente",
            "apellido": "2",
            "nacimiento": "2001-11-12T00:00:00"
        },
        {
            "id": 4,
            "nombre": "Comerciante",
            "apellido": "1",
            "nacimiento": "2001-11-12T00:00:00"
        },
        {
            "id": 5,
            "nombre": "Empresario",
            "apellido": "1",
            "nacimiento": "2001-11-12T00:00:00"
        }
    ]

### Get one
`GET /api/clientes/3`

Return one client by its ID. Returns `200 Ok` if successfull.
#### Response
    {
        "id": 3,
        "nombre": "Cliente",
        "apellido": "nuevo",
        "nacimiento": "2001-11-12T00:00:00"
    }

### Create one
`POST /api/clientes`

Create a new client. Returns `201 Created` if successfull.
#### Request
    {
        "nombre": "Comerciante",
        "Apellido": "2",
        "nacimiento": "2001-11-12"
    }
#### Response
    {
        "id": 5,
        "nombre": "Comerciante",
        "apellido": "2",
        "nacimiento": "2001-11-12T00:00:00"
    }

### Update one
`PUT /api/clientes/1`

Updates an existing client. Returns `204 No Content` if successfull.
#### Request
    {
        "nombre": "Empresario",
        "Apellido": "1",
        "nacimiento": "2001-11-12"
    }

### Partial update one
`PATCH /api/clientes/1`

Updates fields on an existing client. Returns `204 No Content` if successfull. Receives a JsonPatchDocument.
#### Request
    [
        {
            "op": "replace",
            "path": "/apellido",
            "value": "nuevo"
        }
    ]

### Delete one
`DELETE /api/clientes/1`

Deletes an existing client. Returns `204 No Content` if successfull.
