# LibraryXP

Descripción:
Este es un software demostrativo en el que se ha diseñado para el uso de equipos antiguos compatibles con .NET 3.5 (Siendo su foco original para Windows XP, pero debido a compatibilidad, es usable en sistemas x64) para almacenar datos en un archivo .json como persistencia sin base de datos.
Este software cuenta con la gestión de una Biblioteca en el que se almacenan los Autores, Libros, Usuarios y Préstamos para mantener un orden de los dichos Préstamos.

El proyecto fue creado con fines académicos y de práctica en arquitectura básica y persistencia de datos sin base de datos.

---

## 📌 Descripción general

- Gestiona las clases importantes de una Biblioteca, así como un seguimiento del puntaje o historial del usuario.
- Fue creado para utilizarlo en dispositivo con baja potencia, así como la necesidad de hacerlo en una Terminal (CLI), siendo este su mayor sentido de utilización.
- Guardado local en archivo .json
- Validaciones de existencia de clases como los préstamos antes de hacer cambios.

---

## ⚙️ Tecnologías utilizadas

- Lenguaje: C#
- Framework: .NET Framework 3.5
- Entorno: Visual Studio 2026
- Newtonsoft.Json
- Almacenamiento: Archivo .json local

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

🛠️ Instalación y ejecución

El proyecto estará empaquetado en un archivo ejecutable, para su uso dentro del sistema operativo Windows.
Opcionalmente, para mayor detalle o cambio de versión de .NET a una versión posterior:
-Clonar repositorio.
-Abrir archivo .slnx
-Verifique que efectivamente esté en .NET Framework 3.5
-Restaurar dependencias (Newtonsoft.Json)
-Compilar y ejecutar

---

⚠️ Consideraciones y limitaciones

-Proyecto basado en .NET Framework 3.5
-No incluye concurrencia ni control de acceso
-Persistencia limitada a archivos locales
-No recomendado para entornos de producción (a menos que se desee implementar la parte de .json)

---

### Notas finales

Este proyecto es un plano para realizar la gestión de datos en .json, por lo que si es necesario hacer otro proyecto para hacer lo mismo, considera estos planos como una ayuda en cómo se construye un software de este estilo con C# y con Newtonsonft.Json
