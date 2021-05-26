# HabitatExplorer
A tool for exploring Probe Entertainment "Habitat" P3D files, used during the development of Re-Volt

![The tool in action](preview.png)

**About the Project**

This project contains two C# projects within it. The viewer application, and libhabitat. libhabitat can be used  to work with Habitat files in any .NET application.

**Requirements**

.NET Framework 4.7.1 or greater

**Features**

- Bitmap, palette, and texture mapping viewers
- Exports OBJ models
- Export textures and palettes
- Allows the copy of the raw data to the clipboard

**Project Status**

As far as I'm aware all the types that exist in the P3D files available to us are implemented. The rest haven't been seen in the wild.

**Known Issues**

The 3D viewer has a memory leak in it somewhere.
