-- Creación de la tabla Grupo
CREATE TABLE Grupo (
    GrupoId INT PRIMARY KEY IDENTITY(1,1),
    NomOficina VARCHAR(100) NOT NULL,
    CodigoDep INT CHECK (CodigoDep BETWEEN 1 AND 23) NOT NULL
);

--Ingreso Datos de Prueba en la tabla de Grupo
Insert into Grupo ( NomOficina, CodigoDep) values ( 'Oficina1 ', 1), ('Oficina 2', 2);

-- Creación de la tabla Empleado con SupervisorId sin cascada
CREATE TABLE Empleado (
    EmpleadoId INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    PuestoTrabajo VARCHAR(50) NOT NULL,
    SalarioBase INT NULL,
    SupervisorId INT NULL,
	CodigoEmpleado INT CHECK (CodigoEmpleado BETWEEN 1 AND 10000) NOT NULL,
    GrupoId INT NOT NULL,

    -- Clave foránea para referenciar al supervisor (sin cascada)
    CONSTRAINT fk_Empleado_Supervisor FOREIGN KEY (SupervisorId) REFERENCES Empleado(EmpleadoId)
    ON DELETE NO ACTION ON UPDATE NO ACTION,

    -- Clave foránea para referenciar al grupo
    CONSTRAINT fk_Empleado_Grupo FOREIGN KEY (GrupoId) REFERENCES Grupo(GrupoId)
    ON DELETE CASCADE ON UPDATE CASCADE
);

--Ingreso Datos de Prueba en la tabla de Empleado
Insert into Empleado ( 
	Nombre, 
	PuestoTrabajo, 
	SalarioBase, 
	SupervisorId, 
	CodigoEmpleado,
	GrupoId
	) values ( 
	'Gonzalo Monroy ', 
	'Desarrollador', 
	150000, 
	null, 
	1,
	1
	), 
	('Julio Perez ', 
	'Diseñador', 
	150000, 
	null, 
	2,
	2);


	--Ingreso Datos de Prueba en la tabla de Empleado
Insert into Empleado ( 
	Nombre, 
	PuestoTrabajo, 
	SalarioBase, 
	SupervisorId, 
	CodigoEmpleado,
	GrupoId
	) values ( 
	'Gonzalo Monroy ', 
	'Desarrollador', 
	150000, 
	null, 
	1,
	1
	)
	;

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';


EXEC sp_help 'Empleado';

select * from Grupo;
select * from Empleado;


drop table Empleado;