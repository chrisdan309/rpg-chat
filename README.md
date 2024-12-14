# Proyecto de Interacción con NPC en Unity

## Descripción
Esta aplicación en Unity permite al jugador interactuar con una PC para ajustar una emoción, seleccionar un modelo (ruta del endpoint) en otra PC y luego comunicarse con un NPC, quien le devolverá un mensaje basado en el resultado de una API.

## Requisitos
- Unity 2020.3 o superior.
- Conexión a Internet para realizar las solicitudes API.
- Los siguientes scripts de Unity:
    - `NPC`: Controla la interacción con el NPC.
    - `ApiService`: Maneja las solicitudes API.

## Funcionamiento

### Interacción con el NPC
1. El jugador se acerca a una PC en el juego donde puede seleccionar una de las 6 emociones disponibles.
2. Luego, el jugador selecciona el modelo a utilizar (ruta del endpoint de la API).
3. Posteriormente, interactúa con un NPC, quien realiza una solicitud a la API y devuelve un mensaje con la respuesta de la API seleccionada.

### Componentes
#### NPC.cs
- **Responsabilidad**: Gestiona la interacción del jugador con el NPC y la consulta de la API correspondiente según la emoción y el modelo seleccionado.
- **Métodos clave**:
    - `Interact()`: Se activa cuando el jugador interactúa con el NPC, lo que inicia la solicitud a la API.
    - `OnFetchSuccess()`: Muestra el mensaje recibido de la API.
    - `OnFetchError()`: Muestra un mensaje de error si la solicitud falla.

#### ApiService.cs
- **Responsabilidad**: Realiza las solicitudes a las APIs configuradas, ya sea mediante un GET simple o un POST con un cuerpo de solicitud que contiene la emoción seleccionada.
- **Métodos clave**:
    - `FetchMessage()`: Realiza una solicitud GET para obtener el mensaje de la API.
    - `FetchMessageOpenAiModel()`: Realiza una solicitud POST, enviando la emoción seleccionada por el jugador.

### API
La API debe estar configurada en el objeto `ApiConfig`. Dependiendo de la selección del jugador, el NPC interactuará con diferentes endpoints API para obtener el mensaje correspondiente.

#### Configuración de la API
- La URL de la API debe estar definida en el objeto `ApiConfig`, ya sea para un endpoint de tipo GET o POST.
- El jugador selecciona un modelo de API desde la interfaz de usuario, lo que determina qué endpoint utilizar.

### Ejemplo de Uso
1. El jugador selecciona una emoción de las opciones disponibles (ej. "joy", "anger", etc.).
2. Luego selecciona la URL del modelo (por ejemplo, un endpoint para OpenAI o uno estándar).
3. Al interactuar con el NPC, se hace una solicitud API y el NPC muestra el resultado en un cuadro de diálogo.

### Requisitos del Sistema
- Unity 2020.3 o superior.
- API externa que reciba las solicitudes (puede ser una API simulada o una API real de emociones).

## Estructura de Archivos
- `Assets/`
    - `Scripts/`
        - `ApiConfig.cs`
        - `ApiService.cs`
        - `DialogManager.cs`
        - `Interactable.cs`
        - `MenuController.cs`
        - `ModelController.cs`
        - `ModelPC.cs`
        - `Movement.cs`
        - `NPC.cs`
        - `PC.cs`
    - `Prefabs/`
        - `NPCPrefab`
        - `PlayerPCPrefab`
    - `Scenes/`
        - `SampleScene.unity`
