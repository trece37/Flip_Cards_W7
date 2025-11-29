# Flip_Cards-W7 v1.0

<div align="center">

**Recreación moderna del legendario Windows 7 Flip 3D**

[![GitHub](https://img.shields.io/badge/GitHub-trece37%2FFlip__Cards__W7-7FFF00?logo=github)](https://github.com/trece37/Flip_Cards_W7)
[![License](https://img.shields.io/badge/License-MIT-7FFF00)](#licencia)
[![.NET](https://img.shields.io/badge/.NET-6.0-7FFF00?logo=dotnet)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows%2010%2F11-7FFF00?logo=windows)](https://www.microsoft.com/windows)

</div>

---

## 🎯 Descripción

**Flip_Cards-W7** es una aplicación nativa de Windows que recrea el icónico efecto **Flip 3D** de Windows 7, donde las ventanas se apilaban como cartas en el espacio tridimensional.

Esta versión moderna está construida con **WPF (Windows Presentation Foundation)** y **C# .NET 6**, ofreciendo:

- ✅ **Efecto 3D auténtico** con renderizado en tiempo real
- ✅ **Atajo de teclado global** configurable (**Ctrl+Shift+Tab** por defecto)
- ✅ **Compatible** con Windows 10 y Windows 11
- ✅ **Consumo mínimo** de recursos del sistema
- ✅ **Código 100% open source** y extensible

---

## 🚀 Instalación Rápida

### Opción 1: Compilar desde el código fuente

#### Requisitos previos:
- **Visual Studio 2022** o superior (con carga de trabajo .NET Desktop Development)
- **.NET 6 SDK** o superior → [Descargar](https://dotnet.microsoft.com/download)
- **Windows 10/11** (arquitectura x64)

#### Pasos:

1. **Clonar el repositorio:**
   ```powershell
   git clone https://github.com/trece37/Flip_Cards_W7.git
   cd Flip_Cards_W7
   ```

2. **Compilar con un solo comando:**
   ```powershell
   .\BUILD.ps1
   ```
   
   O usar el script batch:
   ```cmd
   COMPILE.bat
   ```

3. **Ejecutar la aplicación:**
   ```powershell
   .\publish\Flip_Cards_W7.exe
   ```

### Opción 2: Compilar manualmente con Visual Studio

1. Abrir `Flip_Cards_W7.csproj` en Visual Studio
2. Build → Build Solution (`Ctrl+Shift+B`)
3. Ejecutar: `bin\Release\net6.0-windows\Flip_Cards_W7.exe`

---

## ⌨️ Uso

### Activar Flip 3D:
Presiona `Ctrl + Shift + Tab` (atajo por defecto)

La ventana Flip 3D aparecerá mostrando todas las ventanas abiertas en un efecto 3D tipo baraja de cartas.

### Controles:

| Tecla | Acción |
|-------|--------|
| `←` / `→` | Navegar entre ventanas |
| `Enter` | Activar ventana seleccionada |
| `Esc` | Cerrar Flip 3D |
| `Ctrl+Shift+Tab` | Alternar Flip 3D (mostrar/ocultar) |

---

## 🛠️ Configuración Avanzada

### Cambiar atajo de teclado:

Edita el código en `MainWindow.xaml.cs`, método `Window_Loaded`:

```csharp
// Ejemplo 1: Cambiar a Ctrl+Alt+Tab
GlobalHotkey.RegisterHotkey(this, ModifierKeys.Control | ModifierKeys.Alt, Key.Tab, OnHotkeyPressed);

// Ejemplo 2: Cambiar a Win+Space
GlobalHotkey.RegisterHotkey(this, ModifierKeys.Windows, Key.Space, OnHotkeyPressed);

// Ejemplo 3: Cambiar a Ctrl+Shift+Supr
GlobalHotkey.RegisterHotkey(this, ModifierKeys.Control | ModifierKeys.Shift, Key.Delete, OnHotkeyPressed);
```

Luego recompila el proyecto.

---

## 📁 Estructura del Proyecto

```
Flip_Cards_W7/
├── Program.cs                  # Entry point de la aplicación
├── App.xaml                    # Definición de la aplicación WPF
├── App.xaml.cs                 # Code-behind de App
├── MainWindow.xaml             # Ventana principal con Viewport3D
├── MainWindow.xaml.cs          # Lógica de renderizado 3D y eventos
├── Core/
│   ├── WindowManager.cs        # Enumeración de ventanas (EnumWindows API)
│   ├── WindowInfo.cs           # Modelo de datos de ventana
│   └── GlobalHotkey.cs         # Hook de atajo global (RegisterHotKey)
├── Resources/
│   ├── Trece37Logo.png         # Logo de Trece37 Tech Solutions
│   └── app.ico                 # Icono de la aplicación
├── Flip_Cards_W7.csproj        # Archivo de proyecto .NET
├── BUILD.ps1                   # Script de compilación automática (PowerShell)
├── COMPILE.bat                 # Script de compilación rápida (Batch)
└── README.md                   # Este archivo
```

---

## 🧪 Tecnologías Utilizadas

| Tecnología | Propósito |
|------------|-----------|
| **C# .NET 6** | Lenguaje y framework principal |
| **WPF** | Windows Presentation Foundation (UI framework) |
| **Viewport3D** | Renderizado 3D nativo de WPF |
| **Win32 API** | `EnumWindows`, `RegisterHotKey`, `SetForegroundWindow` |
| **DWM API** | Desktop Window Manager (capturas de ventanas) |
| **XAML** | Lenguaje de marcado declarativo para UI |

---

## 🤝 Contribuir

¡Las contribuciones son bienvenidas! Si tienes ideas para mejorar el proyecto:

1. **Fork** el repositorio
2. Crea una **rama** para tu feature: `git checkout -b feature/AmazingFeature`
3. **Commit** tus cambios: `git commit -m 'Add some AmazingFeature'`
4. **Push** a la rama: `git push origin feature/AmazingFeature`
5. Abre un **Pull Request**

---

## 📜 Licencia

Este proyecto está bajo la licencia **MIT**.

---

## 👨‍💻 Autor

**Trece37 Tech Solutions**

- 🌐 GitHub: [@trece37](https://github.com/trece37)
- 📂 Proyecto: [Flip_Cards_W7](https://github.com/trece37/Flip_Cards_W7)
- 💚 **Powered by Trece37 & Perply**

---

<div align="center">

**¿Te gusta el proyecto? ⭐ Dale una estrella en GitHub!**

**Flip_Cards-W7 © 2025 Trece37 Tech Solutions**

Desarrollado con ❤️ por **Trece37 & Perply**

</div>
