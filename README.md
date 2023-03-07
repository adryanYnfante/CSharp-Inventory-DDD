# CSharp-Inventory-DDD

La App fue desarrollada en **.NET6**. La App de inventario permite crear productos, 
crear facturas de ventas de dichos productos y generar reportes del historial de facturas por ID de la factura 
o por el ID del cliente. El proyecto cuenta con 3 capas, en el dominio se concentra toda la lógica de negocio, 
una capa de infraestructura que tiene la conexión a la base de datos, se están utilizando 2 ORM para las consultas 
Entity Framework y Dapper, 
y una última capa de API que contiene los controllers y toda la configuración de la aplicación.

```sh
"SqlConnection": "Data Source=[SERVERNAME];Initial Catalog=InventoriesyDb;user id=[LOGIN];password=[PASSWORD];Encrypt=True;TrustServerCertificate=True;User Instance=False"
```

