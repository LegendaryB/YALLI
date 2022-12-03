<div align="center">

<h1>YALLI — Yet another LoadLibrary injector</h1>

[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)

[![GitHub license](https://img.shields.io/github/license/LegendaryB/YALLI.svg)](https://github.com/LegendaryB/YALLI/blob/master/LICENSE.md)

Yet another LoadLibrary injector. Nothing fancy, but here you go.
</div><br>

## 📝 Usage

Reference the NuGet package in your application. After that you can use the library.

```csharp
var processName = "notepad";
var dllPath = "C:\\Users\\LegendaryB\\Desktop\\Test.dll";

// Injects into the first found notepad process
Injector.Inject(processName, dllPath);

// Injects into the matching process
Injector.Inject(processName, (processes) => {
    foreach (var process in processes) {
       if (process.MainModule.FileName.Contains("example")) {
          return process;
       }
    }

    return null;
},
dllPath);

// Injects into the process with the given id
Injector.Inject(3212, dllPath);

// Injects into the process referenced by the given process object
var process = Process.GetProcessesByName(processName);
Injector.Inject(process[0], dllPath);
```

## 📝 Usage of the console application

The console application uses the library under the hood to inject your dll into a target process. To do so just invoke the application like this:

```
YALLI.ConsoleApp.exe -pn processName -dll pathToDll
```
