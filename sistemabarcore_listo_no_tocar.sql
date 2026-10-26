CREATE DATABASE SistemaBarCore;
GO

USE SistemaBarCore;
GO

-- =========================================================
-- 1. TABLAS DEL SISTEMA
-- =========================================================

-- 1.1 ZONA Y MESA
CREATE TABLE zona(
    id_zona INT IDENTITY(1,1) PRIMARY KEY,
    nombre_zona VARCHAR(50),
    activo bit default 1
);

CREATE TABLE mesa(
    id_mesa INT IDENTITY(1,1) PRIMARY KEY,
    id_zona INT REFERENCES zona(id_zona),
    numero_mesa INT,
    capacidad_persona INT,
    estado VARCHAR(20),
    activo bit default 1
);

-- 1.2 USUARIO
CREATE TABLE usuario(
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    cedula VARCHAR(20),
    rol VARCHAR(30),
    contraseña VARCHAR(256),
    activo bit default 1 
);

-- 1.3 DIRECCION Y CLIENTE
CREATE TABLE direccion(
    id_direccion INT IDENTITY(1,1) PRIMARY KEY,
    departamento VARCHAR(50),
    municipio VARCHAR(50),
    detalle_direccion VARCHAR(250),
    activo bit default 1
);

CREATE TABLE cliente(
    id_cliente INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    cedula VARCHAR(20),
    telefono VARCHAR(20),
    activo bit default 1,
    id_direccion INT REFERENCES direccion(id_direccion)
);

-- 1.4 CATEGORIA, PRODUCTOS Y KARDEX
CREATE TABLE categoria(
    id_categoria INT IDENTITY(1,1) PRIMARY KEY,
    nombrecat VARCHAR(50),
    activo bit default 1
);

CREATE TABLE productos(
    id_producto INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100),
    precio_venta DECIMAL(10,2),
    precio_costo DECIMAL(10,2),
    stock_actual INT,
    stock_minimo INT,
    activo bit default 1,
    id_categoria INT REFERENCES categoria(id_categoria)
);

CREATE TABLE kardex(
    id_kardex INT IDENTITY(1,1) PRIMARY KEY,
    tipo_movimiento VARCHAR(30),
    cantidad INT,
    fecha DATETIME,
    id_producto INT REFERENCES productos(id_producto)
);

-- 1.5 COMANDA Y DETALLE
CREATE TABLE comanda(
    id_comanda INT IDENTITY(1,1) PRIMARY KEY,
    id_mesa INT REFERENCES mesa(id_mesa),
    id_usuario INT REFERENCES usuario(id_usuario),
    fecha_hora_apertura DATETIME,
    estado VARCHAR(20)
);

CREATE TABLE detalle_comanda(
    id_detalle_comanda INT IDENTITY(1,1) PRIMARY KEY,
    cantidad_pedida INT,
    precio_venta DECIMAL(10,2),
    nota_especial VARCHAR(150),
    id_comanda INT REFERENCES comanda(id_comanda),
    id_producto INT REFERENCES productos(id_producto)
);

-- 1.6 FACTURACION
CREATE TABLE factura(
    id_factura INT IDENTITY(1,1) PRIMARY KEY,
    numero_factura VARCHAR(20),
    fecha_hora_cierre DATETIME,
    subtotal DECIMAL(10,2),
    iva DECIMAL(10,2),
    total DECIMAL(10,2),
    estado_factura VARCHAR(20),
    id_comanda INT REFERENCES comanda(id_comanda),
    id_cliente INT NULL REFERENCES cliente(id_cliente)
);

-- 1.7 CONTABILIDAD
CREATE TABLE asiento_contable(
    id_asiento_contable INT IDENTITY(1,1) PRIMARY KEY,
    fecha_asiento DATETIME,
    glosa VARCHAR(250),
    id_factura INT null REFERENCES factura(id_factura)
);

CREATE TABLE tipo_cuenta(
    id_tipo_cuenta INT IDENTITY(1,1) PRIMARY KEY,
    nombre_tipo_cuenta VARCHAR(50),
    activo bit default 1
);

CREATE TABLE cuenta_contable(
    id_cuenta_contable INT IDENTITY(1,1) PRIMARY KEY,
    cod_cuenta VARCHAR(20),
    nombre_cuenta VARCHAR(100),
    activo bit default 1,
    id_tipo_cuenta INT REFERENCES tipo_cuenta(id_tipo_cuenta)
);

CREATE TABLE detalle_asiento_contable(
    id_detalle_asiento_contable INT IDENTITY(1,1) PRIMARY KEY,
    monto_debe DECIMAL(10,2) not null default 0.00 ,
    monto_haber DECIMAL(10,2)not null default 0.00,
    id_asiento_contable INT not null REFERENCES asiento_contable(id_asiento_contable),
    id_cuenta_contable INT not null REFERENCES cuenta_contable(id_cuenta_contable)
);
GO

-- 2. PROCEDIMIENTOS ALMACENADOS 

-- MODULO: ZONA
------------------------------------------------------------
--nuevo--
CREATE OR ALTER PROC nuevo_zona
(
    @nombre_zona VARCHAR(50)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO zona(nombre_zona) VALUES(@nombre_zona)
        COMMIT
    END TRY
  BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO
--listar--
CREATE OR ALTER PROC listar_zona
AS
BEGIN
 SET NOCOUNT ON;
    SELECT * FROM zona ;
END
GO
--obtener id--
CREATE OR ALTER PROC obtener_zona_por_id 
(@id_zona INT) 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM zona WHERE id_zona = @id_zona ;
END;
GO
--buscar --
CREATE OR ALTER PROC buscar_zona
(
    @criterio VARCHAR(50)
)
AS
BEGIN
  SET NOCOUNT ON;
    SELECT * FROM zona WHERE nombre_zona LIKE '%' + @criterio + '%'
    and activo = 1;
END
GO
--editar--
CREATE OR ALTER PROC editar_zona
(
    @id_zona INT,
    @nombre_zona VARCHAR(50),
    @activo bit
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE zona 
         SET nombre_zona = @nombre_zona,
                activo = @activo 
            WHERE id_zona = @id_zona;
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO
--eliminar
CREATE OR ALTER PROC eliminar_zona
(
    @id_zona INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
       UPDATE zona SET activo = 0 where  id_zona=@id_zona;
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
 END CATCH
END
GO

------------------------------------------------------------
-- MODULO: MESA
------------------------------------------------------------

--nuevo--
CREATE OR ALTER PROC nuevo_mesa
(
    @id_zona INT,
    @numero_mesa INT,
    @capacidad_persona INT,
    @estado VARCHAR(20)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO mesa(id_zona, numero_mesa, capacidad_persona, estado)
        VALUES(@id_zona, @numero_mesa, @capacidad_persona, @estado)
        COMMIT
    END TRY
   BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO
--listar--
CREATE OR ALTER PROC listar_mesa
AS
BEGIN
 SET NOCOUNT ON;
    SELECT * FROM mesa ;
END
GO

--obtener por id --
CREATE OR ALTER PROC obtener_mesa_por_id 
(
    @id_mesa INT
) 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM mesa WHERE id_mesa = @id_mesa ;
END;
GO

--buscar--
CREATE OR ALTER PROC buscar_mesa
(
    @id_zona INT
)
AS
BEGIN
 SET NOCOUNT ON;
    SELECT m.*, z.nombre_zona 
    FROM mesa m
    INNER JOIN zona z ON m.id_zona = z.id_zona
    WHERE m.id_zona = @id_zona;
END
GO

CREATE OR ALTER PROC editar_mesa
(
    @id_mesa INT,
    @id_zona INT,
    @numero_mesa INT,
    @capacidad_persona INT,
    @estado VARCHAR(20),
    @activo bit
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE mesa
        SET id_zona=@id_zona, numero_mesa=@numero_mesa, capacidad_persona=@capacidad_persona, estado=@estado, activo= @activo
        WHERE id_mesa=@id_mesa
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_mesa
(
    @id_mesa INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE mesa SET activo = 0 WHERE id_mesa=@id_mesa;
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

------------------------------------------------------------
-- MODULO: USUARIO
------------------------------------------------------------
CREATE OR ALTER PROC nuevo_usuario
(
    @nombre VARCHAR(100),
    @cedula VARCHAR(20),
    @rol VARCHAR(30),
    @contraseña VARCHAR(256)
    
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO usuario(nombre, cedula, rol, contraseña)
        VALUES(@nombre, @cedula, @rol, @contraseña)
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_usuario
AS
BEGIN
SET NOCOUNT ON;
    SELECT id_usuario,nombre,cedula,rol, activo FROM usuario;
END
GO
--obtener id--
CREATE OR ALTER PROC obtener_usuario_por_id (@id_usuario INT) AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_usuario, nombre, cedula, rol, activo FROM usuario WHERE id_usuario = @id_usuario ;
END;
GO
--buscar
CREATE OR ALTER PROC buscar_usuario
(
    @criterio VARCHAR(100)
)
AS
BEGIN
SET NOCOUNT ON;
    SELECT id_usuario,nombre,cedula,rol  FROM usuario
    WHERE (nombre LIKE '%' + @criterio + '%' OR cedula LIKE '%' + @criterio + '%') AND activo=1 ;
END
GO

CREATE OR ALTER PROC editar_usuario
(
    @id_usuario INT,
    @nombre VARCHAR(100),
    @cedula VARCHAR(20),
    @rol VARCHAR(30),
    @contraseña VARCHAR(256) = NULL,
    @activo BIT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        BEGIN TRAN;

        UPDATE usuario
        SET 
            nombre = @nombre,
            cedula = @cedula,
            rol = @rol,
            contraseña = CASE 
                            WHEN NULLIF(@contraseña, '') IS NULL 
                            THEN contraseña
                            ELSE @contraseña
                         END,
            activo = @activo
        WHERE id_usuario = @id_usuario;

        COMMIT;

    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK;

        THROW;

    END CATCH
END
GO

CREATE OR ALTER PROC eliminar_usuario
(
    @id_usuario INT
)
AS
BEGIN
   SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE usuario SET activo = 0 WHERE id_usuario=@id_usuario;
        COMMIT
    END TRY
   BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

------------------------------------------------------------
-- MODULO: DIRECCION Y CLIENTE
------------------------------------------------------------
CREATE OR ALTER PROC nuevo_direccion
(
    @departamento VARCHAR(50),
    @municipio VARCHAR(50),
    @detalle_direccion VARCHAR(250)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO direccion(departamento, municipio, detalle_direccion)
        VALUES(@departamento, @municipio, @detalle_direccion)
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_direccion
AS
BEGIN
  SET NOCOUNT ON;
    SELECT * FROM direccion WHERE activo=1;
END
GO
--obtener id--
CREATE OR ALTER PROC obtener_direccion_por_id 
(@id_direccion INT) 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM direccion WHERE id_direccion = @id_direccion AND activo = 1;
END;
GO

--editar--
CREATE OR ALTER PROC editar_direccion
(
    @id_direccion INT,
    @departamento VARCHAR(50),
    @municipio VARCHAR(50),
    @detalle_direccion VARCHAR(250),
    @activo bit
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE direccion
        SET departamento=@departamento, municipio=@municipio, detalle_direccion=@detalle_direccion, activo=@activo
        WHERE id_direccion=@id_direccion
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_direccion
(
    @id_direccion INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE direccion SET activo= 0 WHERE id_direccion=@id_direccion
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO
--nuevo cliente--
CREATE OR ALTER PROC nuevo_cliente(
    @nombre VARCHAR(100),
    @cedula VARCHAR(20),
    @telefono VARCHAR(20),
    @departamento VARCHAR(50),
    @municipio VARCHAR(50),
    @detalle_direccion VARCHAR(250)
)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;

            -- 1. Insertamos la nueva dirección
            INSERT INTO direccion (departamento, municipio, detalle_direccion)
            VALUES (@departamento, @municipio, @detalle_direccion);

            -- 2. Capturamos el ID recién generado de la dirección
            DECLARE @nuevo_id_dir INT = SCOPE_IDENTITY();

            -- 3. Insertamos el cliente utilizando ese ID
            INSERT INTO cliente (id_direccion, nombre, cedula, telefono)
            VALUES (@nuevo_id_dir, @nombre, @cedula, @telefono);

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO
--listar
CREATE OR ALTER PROC listar_cliente
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.id_cliente, 
        c.nombre, 
        c.cedula, 
        c.telefono, 
        c.activo, 
        c.id_direccion,
        d.departamento, 
        d.municipio, 
        d.detalle_direccion
    FROM cliente c
    INNER JOIN direccion d ON c.id_direccion = d.id_direccion;
END
GO
--obtener por id
CREATE OR ALTER PROC obtener_cliente_por_id(
    @id_cliente INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.id_cliente, 
        c.nombre, 
        c.cedula, 
        c.telefono, 
        c.activo, 
        c.id_direccion,
        d.departamento, 
        d.municipio, 
        d.detalle_direccion
    FROM cliente c
    INNER JOIN direccion d ON c.id_direccion = d.id_direccion
    WHERE c.id_cliente = @id_cliente;
END
GO

CREATE OR ALTER PROC buscar_cliente(
    @criterio VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.id_cliente, 
        c.nombre, 
        c.cedula, 
        c.telefono, 
        c.activo, 
        c.id_direccion,
        d.departamento, 
        d.municipio, 
        d.detalle_direccion
    FROM cliente c
    INNER JOIN direccion d ON c.id_direccion = d.id_direccion
    WHERE (
        c.nombre LIKE '%' + @criterio + '%' 
        OR c.cedula LIKE '%' + @criterio + '%'
        OR c.telefono LIKE '%' + @criterio + '%'
        OR d.municipio LIKE '%' + @criterio + '%'
    )
    AND c.activo = 1; 
END
GO
--editar
CREATE OR ALTER PROC editar_cliente(
    @id_cliente INT,
    @nombre VARCHAR(100),
    @cedula VARCHAR(20),
    @telefono VARCHAR(20),
    @activo BIT,
    @id_direccion INT,
    @departamento VARCHAR(50),
    @municipio VARCHAR(50),
    @detalle_direccion VARCHAR(250)
)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;

            -- 1. Actualizamos cliente (incluyendo su estado activo/inactivo)
            UPDATE cliente
            SET nombre = @nombre,
                cedula = @cedula,
                telefono = @telefono,
                activo = @activo
            WHERE id_cliente = @id_cliente;

            -- 2. Actualizamos la dirección (re-activándola también)
            UPDATE direccion
            SET departamento = @departamento,
                municipio = @municipio,
                detalle_direccion = @detalle_direccion,
                activo = @activo
            WHERE id_direccion = @id_direccion;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

GO
CREATE OR ALTER PROC eliminar_cliente(
    @id_cliente INT
)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN;

            -- 1. Desactivamos la dirección asociada al cliente
            UPDATE d
            SET d.activo = 0
            FROM direccion d
            INNER JOIN cliente c ON d.id_direccion = c.id_direccion
            WHERE c.id_cliente = @id_cliente;

            -- 2. Desactivamos al cliente (Baja Lógica)
            UPDATE cliente 
            SET activo = 0 
            WHERE id_cliente = @id_cliente;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END;
GO

------------------------------------------------------------
-- MODULO: CATEGORIA, PRODUCTO Y KARDEX
------------------------------------------------------------
CREATE OR ALTER PROC nuevo_categoria
(
    @nombrecat VARCHAR(50)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO categoria(nombrecat) VALUES(@nombrecat)
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_categoria
AS
BEGIN
  SET NOCOUNT ON;
    SELECT * FROM categoria ;
END
GO
--obtener por id--
CREATE OR ALTER PROC obtener_categoria_por_id (@id_categoria INT) AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM categoria WHERE id_categoria = @id_categoria ;
END;
GO
--buscar 
CREATE OR ALTER PROC buscar_categoria
(
    @criterio VARCHAR(50)
)
AS
BEGIN
  SET NOCOUNT ON;
    SELECT * FROM categoria WHERE nombrecat LIKE '%' + @criterio + '%' AND activo=1;
END
GO

CREATE OR ALTER PROC editar_categoria
(
    @id_categoria INT,
    @nombrecat VARCHAR(50),
    @activo bit
)
AS
BEGIN
  SET NOCOUNT ON
    BEGIN TRY
        BEGIN TRAN
        UPDATE categoria SET nombrecat=@nombrecat , activo=@activo WHERE id_categoria=@id_categoria
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_categoria
(
    @id_categoria INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE categoria SET activo = 0 WHERE id_categoria=@id_categoria
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO
--nuevo--
CREATE OR ALTER PROC nuevo_productos
(
    @id_categoria INT,
    @nombre VARCHAR(100),
    @precio_venta DECIMAL(10,2),
    @precio_costo DECIMAL(10,2),
    @stock_actual INT,
    @stock_minimo INT
)
AS
BEGIN
 SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO productos(id_categoria, nombre, precio_venta, precio_costo, stock_actual, stock_minimo)
        VALUES(@id_categoria, @nombre, @precio_venta, @precio_costo, @stock_actual, @stock_minimo)
        COMMIT
        PRINT 'Se inserto correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_productos
AS
BEGIN
  SET NOCOUNT ON;
    SELECT * FROM productos;
END
GO
--obtener por id--
CREATE OR ALTER PROC obtener_productos_por_id 
(
    @id_producto INT
) 
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT * 
    FROM productos 
    WHERE id_producto = @id_producto;
END;
GO
--buscar--
CREATE OR ALTER PROC buscar_productos
(
    @criterio VARCHAR(100)
)
AS
BEGIN
   SET NOCOUNT ON;
    SELECT p.*, c.nombrecat 
    FROM productos p
    INNER JOIN categoria c ON p.id_categoria = c.id_categoria
    WHERE p.nombre LIKE '%' + @criterio + '%';
END
GO

CREATE OR ALTER PROC editar_productos
(
    @id_producto INT,
    @id_categoria INT,
    @nombre VARCHAR(100),
    @precio_venta DECIMAL(10,2),
    @precio_costo DECIMAL(10,2),
    @stock_actual INT,
    @stock_minimo INT,
    @activo bit
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE productos
        SET id_categoria=@id_categoria, nombre=@nombre, precio_venta=@precio_venta,
            precio_costo=@precio_costo, stock_actual=@stock_actual, stock_minimo=@stock_minimo , activo= @activo
        WHERE id_producto=@id_producto
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_productos
(
    @id_producto INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
    UPDATE productos SET activo = 0 WHERE id_producto = @id_producto;
     COMMIT
        PRINT 'Se elimino correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO
--nuevo 
CREATE OR ALTER PROC nuevo_kardex
(
    @id_producto INT,
    @tipo_movimiento VARCHAR(30),
    @cantidad INT,
    @fecha DATETIME
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO kardex(id_producto, tipo_movimiento, cantidad, fecha)
        VALUES(@id_producto, @tipo_movimiento, @cantidad, @fecha)
        COMMIT
        PRINT 'Se inserto correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_kardex
AS
BEGIN
  SET NOCOUNT ON;
    SELECT * FROM kardex;
END
GO
--obtener por id 
CREATE OR ALTER PROC obtener_kardex_por_id 
(
    @id_kardex INT
) 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM kardex WHERE id_kardex = @id_kardex;
END;
GO
--editar
CREATE OR ALTER PROC editar_kardex
(
    @id_kardex INT,
    @id_producto INT,
    @tipo_movimiento VARCHAR(30),
    @cantidad INT,
    @fecha DATETIME
    
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE kardex
        SET id_producto=@id_producto, tipo_movimiento=@tipo_movimiento, cantidad=@cantidad, fecha=@fecha
        WHERE id_kardex=@id_kardex
        COMMIT
        PRINT 'Se edito correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_kardex
(
    @id_kardex INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        DELETE FROM kardex WHERE id_kardex=@id_kardex
        COMMIT
        PRINT 'Se elimino correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

-- TRIGGER: Sincroniza el Stock del Producto con cada Movimiento del Kardex
CREATE OR ALTER TRIGGER trg_actualizar_stock
ON kardex
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- =========================================
    -- 1. REVERTIR EL MOVIMIENTO ANTERIOR
    --    Aplica en UPDATE y DELETE
    -- =========================================

    UPDATE p
    SET p.stock_actual =
        p.stock_actual
        - CASE
            WHEN d.tipo_movimiento = 'ENTRADA' THEN d.cantidad
            WHEN d.tipo_movimiento = 'SALIDA' THEN -d.cantidad
            ELSE 0
          END
    FROM productos p
    INNER JOIN deleted d
        ON p.id_producto = d.id_producto;


    -- =========================================
    -- 2. APLICAR EL NUEVO MOVIMIENTO
    --    Aplica en INSERT y UPDATE
    -- =========================================

    UPDATE p
    SET p.stock_actual =
        p.stock_actual
        + CASE
            WHEN i.tipo_movimiento = 'ENTRADA' THEN i.cantidad
            WHEN i.tipo_movimiento = 'SALIDA' THEN -i.cantidad
            ELSE 0
          END
    FROM productos p
    INNER JOIN inserted i
        ON p.id_producto = i.id_producto;
END;
GO

------------------------------------------------------------
-- MODULO: COMANDA Y DETALLE
------------------------------------------------------------
CREATE OR ALTER PROC nuevo_comanda
(
    @id_mesa INT,
    @id_usuario INT,
    @fecha_hora_apertura DATETIME,
    @estado VARCHAR(20)
)
AS
BEGIN
 SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO comanda(id_mesa, id_usuario, fecha_hora_apertura, estado)
        VALUES(@id_mesa, @id_usuario, @fecha_hora_apertura, @estado)
        COMMIT
        PRINT 'Se inserto correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_comanda
AS
BEGIN
  SET NOCOUNT ON;
    SELECT * FROM comanda ;
END
GO
--obtener por id
CREATE OR ALTER PROC obtener_comanda_por_id (@id_comanda INT) AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM comanda WHERE id_comanda = @id_comanda;
END;
GO

--editar
CREATE OR ALTER PROC editar_comanda
(
    @id_comanda INT,
    @id_mesa INT,
    @id_usuario INT,
    @fecha_hora_apertura DATETIME,
    @estado VARCHAR(20)
)
AS
BEGIN
   SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE comanda
        SET id_mesa=@id_mesa, id_usuario=@id_usuario, fecha_hora_apertura=@fecha_hora_apertura, estado=@estado
        WHERE id_comanda=@id_comanda
        COMMIT
        PRINT 'Se edito correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_comanda
(
    @id_comanda INT
)
AS
BEGIN
 SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
     UPDATE comanda SET estado = 'CANCELADA' WHERE id_comanda = @id_comanda        COMMIT
        PRINT 'Se elimino correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO
--detalle comanda 
CREATE OR ALTER PROC nuevo_detalle_comanda
(
    @id_comanda INT,
    @id_producto INT,
    @cantidad_pedida INT,
    @precio_venta DECIMAL(10,2),
    @nota_especial VARCHAR(150)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO detalle_comanda(id_comanda, id_producto, cantidad_pedida, precio_venta, nota_especial)
        VALUES(@id_comanda, @id_producto, @cantidad_pedida, @precio_venta, @nota_especial)
        COMMIT
        PRINT 'Se inserto correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_detalle_comanda
AS
BEGIN
  SET NOCOUNT ON;
    SELECT * FROM detalle_comanda;
END
GO
--obtener id 
CREATE OR ALTER PROC obtener_detalle_comanda_por_id 
(
    @id_detalle_comanda INT
) 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * 
    FROM detalle_comanda 
    WHERE id_detalle_comanda = @id_detalle_comanda;
END;
GO
--editar 
CREATE OR ALTER PROC editar_detalle_comanda
(
    @id_detalle_comanda INT,
    @id_comanda INT,
    @id_producto INT,
    @cantidad_pedida INT,
    @precio_venta DECIMAL(10,2),
    @nota_especial VARCHAR(150)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE detalle_comanda
        SET id_comanda=@id_comanda, id_producto=@id_producto, cantidad_pedida=@cantidad_pedida,
            precio_venta=@precio_venta, nota_especial=@nota_especial
        WHERE id_detalle_comanda=@id_detalle_comanda
        COMMIT
        PRINT 'Se edito correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_detalle_comanda
(
    @id_detalle_comanda INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        DELETE FROM detalle_comanda WHERE id_detalle_comanda=@id_detalle_comanda
        COMMIT
        PRINT 'Se elimino correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO


-- Detalle Comanda (Todos los ítems de una sola comanda)
CREATE OR ALTER PROC obtener_detalle_comanda_por_comanda 
(
    @id_comanda INT
) 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * 
    FROM detalle_comanda 
    WHERE id_comanda = @id_comanda;
END;
GO

------------------------------------------------------------
-- MODULO: FACTURACION
------------------------------------------------------------
CREATE OR ALTER PROC nuevo_factura
(
    @id_comanda INT,
    @id_cliente INT,
    @numero_factura VARCHAR(20),
    @fecha_hora_cierre DATETIME,
    @subtotal DECIMAL(10,2),
    @iva DECIMAL(10,2),
    @total DECIMAL(10,2),
    @estado_factura VARCHAR(20)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO factura(id_comanda, id_cliente, numero_factura, fecha_hora_cierre, subtotal, iva, total, estado_factura)
        VALUES(@id_comanda, @id_cliente, @numero_factura, @fecha_hora_cierre, @subtotal, @iva, @total, @estado_factura)
        COMMIT
        PRINT 'Se inserto correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_factura
AS
BEGIN
  SET NOCOUNT ON;
    SELECT * FROM factura;
END
GO
--obtener por id
CREATE OR ALTER PROC obtener_factura_por_id 
(
    @id_factura INT
) 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        f.id_factura,
        f.numero_factura,
        f.fecha_hora_cierre,
        f.subtotal,
        f.iva,
        f.total,
        f.estado_factura,
        f.id_comanda,
        f.id_cliente,
        -- Si no hay cliente asignado, devuelve 'Cliente General' en vez de NULL
        ISNULL(c.nombre, 'Cliente General') AS nombre_cliente 
    FROM factura f
    LEFT JOIN cliente c ON f.id_cliente = c.id_cliente
    WHERE f.id_factura = @id_factura;
END;
GO
--buscar
CREATE OR ALTER PROC buscar_factura
(
    @criterio VARCHAR(20)
)
AS
BEGIN
   SET NOCOUNT ON;
    SELECT f.*, c.nombre AS nombre_cliente
    FROM factura f
    LEFT JOIN cliente c ON f.id_cliente = c.id_cliente
    WHERE f.numero_factura LIKE '%' + @criterio + '%';
END
GO

CREATE OR ALTER PROC editar_factura
(
    @id_factura INT,
    @id_comanda INT,
    @id_cliente INT,
    @numero_factura VARCHAR(20),
    @fecha_hora_cierre DATETIME,
    @subtotal DECIMAL(10,2),
    @iva DECIMAL(10,2),
    @total DECIMAL(10,2),
    @estado_factura VARCHAR(20)
)
AS
BEGIN
   SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE factura
        SET id_comanda=@id_comanda, id_cliente=@id_cliente, numero_factura=@numero_factura,
            fecha_hora_cierre=@fecha_hora_cierre, subtotal=@subtotal, iva=@iva,
            total=@total, estado_factura=@estado_factura
        WHERE id_factura=@id_factura
        COMMIT
        PRINT 'Se edito correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_factura
(
    @id_factura INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        -- En lugar de DELETE, para anular:
          UPDATE factura SET estado_factura = 'ANULADA' WHERE id_factura = @id_factura
        COMMIT
        PRINT 'Se elimino correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

------------------------------------------------------------
-- MODULO: CONTABILIDAD
------------------------------------------------------------
CREATE OR ALTER PROC nuevo_asiento_contable
(
    @id_factura INT,
    @fecha_asiento DATETIME,
    @glosa VARCHAR(250)
)
AS
BEGIN
 SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO asiento_contable(id_factura, fecha_asiento, glosa)
        VALUES(@id_factura, @fecha_asiento, @glosa)
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_asiento_contable
AS
BEGIN
  SET NOCOUNT ON;
    SELECT id_asiento_contable, fecha_asiento, glosa, id_factura 
    FROM asiento_contable;
END
GO
--obtener por  id
CREATE OR ALTER PROC obtener_asiento_contable_por_id (@id_asiento_contable INT) AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_asiento_contable, fecha_asiento, glosa, id_factura 
    FROM asiento_contable 
    WHERE id_asiento_contable = @id_asiento_contable;;
END;
GO
--editar
CREATE OR ALTER PROC editar_asiento_contable
(
    @id_asiento_contable INT,
    @id_factura INT=null,
    @fecha_asiento DATETIME,
    @glosa VARCHAR(250)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE asiento_contable
        SET id_factura=@id_factura, fecha_asiento=@fecha_asiento, glosa=@glosa
        WHERE id_asiento_contable=@id_asiento_contable
        COMMIT
    END TRY
   BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_asiento_contable
(
    @id_asiento_contable INT
)
AS
BEGIN
 SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        DELETE FROM asiento_contable WHERE id_asiento_contable=@id_asiento_contable
        COMMIT
        PRINT 'Se elimino correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO
--tipo cuenta
CREATE OR ALTER PROC nuevo_tipo_cuenta
(
    @nombre_tipo_cuenta VARCHAR(50)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO tipo_cuenta(nombre_tipo_cuenta,activo) VALUES(@nombre_tipo_cuenta,1)
        COMMIT
        PRINT 'Se inserto correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_tipo_cuenta
AS
BEGIN
  SET NOCOUNT ON;
    SELECT id_tipo_cuenta, nombre_tipo_cuenta, activo
    FROM tipo_cuenta;
END
GO
--obtener por id--
CREATE OR ALTER PROC obtener_tipo_cuenta_por_id 
(
    @id_tipo_cuenta INT
) 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_tipo_cuenta, nombre_tipo_cuenta, activo 
    FROM tipo_cuenta 
    WHERE id_tipo_cuenta = @id_tipo_cuenta;
END;

GO

GO
--editar
CREATE OR ALTER PROC editar_tipo_cuenta
(
    @id_tipo_cuenta INT,
    @nombre_tipo_cuenta VARCHAR(50),
    @activo bit
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE tipo_cuenta SET nombre_tipo_cuenta=@nombre_tipo_cuenta, activo = @activo WHERE id_tipo_cuenta=@id_tipo_cuenta
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_tipo_cuenta
(
    @id_tipo_cuenta INT
)
AS
BEGIN
 SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE tipo_cuenta 
        SET activo = 0 
        WHERE id_tipo_cuenta = @id_tipo_cuenta;
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC nuevo_cuenta_contable
(
    @id_tipo_cuenta INT,
    @cod_cuenta VARCHAR(20),
    @nombre_cuenta VARCHAR(100)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO cuenta_contable(id_tipo_cuenta, cod_cuenta, nombre_cuenta,activo)
        VALUES(@id_tipo_cuenta, @cod_cuenta, @nombre_cuenta,1)
        COMMIT
        PRINT 'Se inserto correctamente'
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_cuenta_contable
AS
BEGIN
  SET NOCOUNT ON;
   SELECT id_cuenta_contable, cod_cuenta, nombre_cuenta, activo, id_tipo_cuenta 
    FROM cuenta_contable;END
GO
--obtener por id
CREATE OR ALTER PROC obtener_cuenta_contable_por_id 
(
    @id_cuenta_contable INT
) 
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_cuenta_contable, cod_cuenta, nombre_cuenta, activo, id_tipo_cuenta 
    FROM cuenta_contable
    WHERE id_cuenta_contable = @id_cuenta_contable;
END;
GO
--editar
CREATE OR ALTER PROC editar_cuenta_contable
(
    @id_cuenta_contable INT,
    @id_tipo_cuenta INT,
    @cod_cuenta VARCHAR(20),
    @nombre_cuenta VARCHAR(100),
    @activo bit
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE cuenta_contable
        SET id_tipo_cuenta=@id_tipo_cuenta, cod_cuenta=@cod_cuenta, nombre_cuenta=@nombre_cuenta , activo =@activo
        WHERE id_cuenta_contable=@id_cuenta_contable
        COMMIT
    END TRY
    BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_cuenta_contable
(
    @id_cuenta_contable INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE cuenta_contable 
        SET activo = 0 
        WHERE id_cuenta_contable = @id_cuenta_contable;
        COMMIT
        PRINT 'Se elimino correctamente'
    END TRY
   BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
END
GO
--detalle asiento contable
CREATE OR ALTER PROC nuevo_detalle_asiento_contable
(
    @id_asiento_contable INT,
    @id_cuenta_contable INT,
    @monto_debe DECIMAL(10,2),
    @monto_haber DECIMAL(10,2)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        INSERT INTO detalle_asiento_contable(id_asiento_contable, id_cuenta_contable, monto_debe, monto_haber)
        VALUES(@id_asiento_contable, @id_cuenta_contable, @monto_debe, @monto_haber)
        COMMIT
    END TRY
    BEGIN CATCH
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC listar_detalle_asiento_contable
AS
BEGIN
  SET NOCOUNT ON;
    SELECT id_detalle_asiento_contable, monto_debe, monto_haber, id_asiento_contable, id_cuenta_contable 
    FROM detalle_asiento_contable;
END
GO
--obtener por id
CREATE OR ALTER PROC obtener_detalle_asiento_contable_por_id 
(
    @id_detalle_asiento_contable INT
)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id_detalle_asiento_contable, monto_debe, monto_haber, id_asiento_contable, id_cuenta_contable
    FROM detalle_asiento_contable
    WHERE id_detalle_asiento_contable = @id_detalle_asiento_contable;
END;
GO
--editar
CREATE OR ALTER PROC editar_detalle_asiento_contable
(
    @id_detalle_asiento_contable INT,
    @id_asiento_contable INT,
    @id_cuenta_contable INT,
    @monto_debe DECIMAL(10,2),
    @monto_haber DECIMAL(10,2)
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        UPDATE detalle_asiento_contable
        SET id_asiento_contable=@id_asiento_contable, id_cuenta_contable=@id_cuenta_contable,
            monto_debe=@monto_debe, monto_haber=@monto_haber
        WHERE id_detalle_asiento_contable=@id_detalle_asiento_contable
        COMMIT
    END TRY
    BEGIN CATCH
        ROLLBACK;
    THROW;
END CATCH
END
GO

CREATE OR ALTER PROC eliminar_detalle_asiento_contable
(
    @id_detalle_asiento_contable INT
)
AS
BEGIN
  SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRAN
        DELETE FROM detalle_asiento_contable WHERE id_detalle_asiento_contable=@id_detalle_asiento_contable
        COMMIT
    END TRY
    BEGIN CATCH
        ROLLBACK;
    THROW;
END CATCH
END
GO


-- Detalle Asiento Contable (Todos los registros de un mismo asiento)
CREATE OR ALTER PROC obtener_detalle_asiento_por_asiento (@id_asiento_contable INT) AS
BEGIN
    SET NOCOUNT ON;
    SELECT dac.*, cc.nombre_cuenta, cc.cod_cuenta
    FROM detalle_asiento_contable dac
    INNER JOIN cuenta_contable cc ON dac.id_cuenta_contable = cc.id_cuenta_contable
    WHERE dac.id_asiento_contable = @id_asiento_contable;
END;
GO
