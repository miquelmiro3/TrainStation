# Train Station

Repositorio con el proyecto Unity de Train Station. La versión de Unity utilizada es la 2019.3.0f6

## Trabajar con git

Una vez instalado git, para poder añadir cosas al respositorio se tienen que utilizar unos comandos desde la consola de Windows, que se puede abrir buscando "cmd" (aunque también existen programas con interfaz visual para usar git, pero yo aquí hablaré de la versión de comandos en consola).

Cuando el repositorio ha sido descargado y preparado, con el comando ```git branch``` puedes ver en qué rama estás trabajando. Hay que tener cuidado de no hacerlo en "master", sino que siempre se trabaje en una rama distinta y luego ya hacer merges de esa rama con master. Puedes crear una nueva rama con ```git branch nombre``` y luego moverte a esa nueva rama con ```git checkout nombre```, y asi trabajar tranquilamente en ese entorno. Cuando quieras subir tu trabajo, puedes hacer ```git add .``` para añadir a la "build" todo lo que has cambiado, o ```git add nombre_archivo``` para añadir un unico archivo especifico, y puedes repetir ese comando varias veces para seleccionar qué quieres añadir. Cuando hayas añadido todo lo que quieres, haces ```git commit``` y se abrirá algún editor de texto para que escribas una frase descriptiva de los cambios, por ejemplo, "Añade un modelo nuevo de una mesa". Una vez el commit está hecho, puedes hacer ```git push``` para subirlo al repositorio. Una vez ahí, se puede mirar la rama en la que lo has subido y, si todo está correcto, mergearlo con la rama master. Si en algún momento haces un push desde un PC distinto, con ```git pull``` te bajarás todo lo que haya en el repositorio para mantenerlo actualizado en tu PC.
