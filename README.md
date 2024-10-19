# ACME API

## Descripción

Esta API permite registrar estudiantes adultos, registrar cursos, y matricular estudiantes en cursos. 

### Funcionalidades:
- Registrar estudiantes por nombre y edad.
- Registrar cursos con nombre, tarifa de inscripción, fechas de inicio y fin.
- Matricular estudiantes en cursos si cumplen con los requisitos.

## Consideraciones futuras:
- Integración con una base de datos.
- Publicación de la API como un servicio público.

## Cosas que te hubiera gustado hacer pero no hiciste:

- Mejoras en el sistema de autenticación y autorización para proteger los endpoints.
- Implementar paginación o filtrado en los métodos de obtención de datos.
- Añadir tests de integración para validar el flujo completo de la API.
- Uso de una base de datos real en lugar de la simulación actual con JSON.

## Cosas que hiciste pero podrían mejorar:

- La estructura de validación de la inscripción (podrías considerar optimizar la separación de responsabilidades).
- El manejo de errores y excepciones en toda la API (usando middleware personalizado para centralizar el manejo de errores).
- Mejorar la eficiencia en el manejo de los datos mock utilizando MemoryCache o una base de datos más eficiente.
- Refactorizar el código para hacerlo más modular y reutilizable, especialmente los servicios.

## Librerías de terceros utilizadas y por qué:

- AutoMapper: Para simplificar el mapeo entre DTOs y modelos de dominio, reduciendo código repetitivo.
- xUnit: Para realizar pruebas unitarias y asegurar el correcto funcionamiento de los servicios y controladores.
- Moq: Utilizado en las pruebas para simular comportamientos de dependencias como bases de datos o servicios externos.

## Tiempo invertido y cosas nuevas investigadas:

Entre 15 y 25 horas de trabajo, a razón de 3 horas invertidas diariamente. Esto incluye no solo la implementación, sino también la investigación y las pruebas.
Tuve que investigar: el uso de AutoMapper, el uso de xUnit y Moq en pruebas unitarias, y la forma de trabajar con una base de datos simulada en JSON.
Cualquier patrón de diseño o buena práctica que implementaste o consideraste relevante durante el desarrollo.

Durante el desarrollo del proyecto, consulté documentación en línea y recursos como Stack Overflow para aclarar detalles específicos de la implementación y buenas prácticas.
Además utilicé herramientas colaborativas como chatGPT para realizar consultas y validaciones específicas.