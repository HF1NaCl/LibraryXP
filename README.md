# LibraryXP

Descripción:
Este es un software demostrativo en el que se ha diseñado para el uso de equipos antiguos compatibles con .NET 3.5 (Siendo su principal foco, usable para Windows XP) para almacenar datos en un archivo .json.
Este software cuenta con la gestión de una Biblioteca en el que se almacenan los Autores, Libros, Usuarios y Préstamos para mantener un orden de los dichos Préstamos.

---

## 📌 Descripción general

-Gestiona las clases importantes de una Biblioteca, así como un seguimiento del puntaje o historial del usuario.
-Fue creado para utilizarlo en dispositivo con baja potencia, así como la necesidad de hacerlo en una Terminal (CLI), siendo este su mayor sentido de utilización.

---

## ⚙️ Tecnologías utilizadas

- Lenguaje: C#
- Framework: .NET Framework 3.5
- Entorno: Visual Studio 2026
- Newtonsoft.Json

---

## 🧱 Arquitectura del proyecto

Su lógica se basa en estos puntos:
-**Program.cs**
 -Es el punto de entrada en el que se comunican con inputs los números para acceder a las gestiones de cada clase.
-**Controllers**
 -Controladores de las clases para realizar aquellas operaciones necesarias como lo son los CRUD, y también adicionales en caso de ser necsario como contar y verificar datos.
 -**Data**
  -Datos iniciales para ejemplificar el uso del .json y la conexión principal entre las clases y el archivo para gestionar los datos.

---

### Notas finales

Este proyecto es un plano para realizar la gestión de datos en .json, por lo que si es necesario hacer otro proyecto para hacer lo mismo, considera estos planos como una ayuda en cómo se construye un software de este estilo con C# y con Newtonsonft.Json
