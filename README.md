# AssetRipper

![](Images/AssetRipperLogoBackground.png)

[![](https://img.shields.io/github/downloads/ds5678/AssetRipper/total.svg)](https://github.com/ds5678/AssetRipper/releases)
[![](https://img.shields.io/github/downloads/ds5678/AssetRipper/latest/total.svg)](https://github.com/ds5678/AssetRipper/releases/latest)
[![](https://img.shields.io/github/v/release/ds5678/AssetRipper)](https://github.com/ds5678/AssetRipper/releases/latest)

AssetRipper is a tool for extracting assets from serialized files (*CAB-*\*, *\*.assets*, *\*.sharedAssets*, etc.) and assets bundles (*\*.unity3d*, *\*.assetbundle*, etc.) and converting them into the native Unity engine format.

> Important note: This project is currently in an experimental state. Expect bugs and many changes.

Current supported versions: `1.x` to `2019.x` (support for later versions is in progress)

## Export features
* Scenes
* Prefabs (GameObjects with transform components)
* AnimationClips (legacy, generic, humanoid)
* Meshes
* Shaders (native listing)
* Textures
* Audio
* Fonts
* Movie textures
* Materials
* AnimatorControllers
* Avatars
* Terrains
* TextAssets
* Components:
  * MeshRenderer
  * SkinnedMeshRenderer
  * Animation
  * Animator
  * Canvas
  * Light
  * ParticleSystem
  * Colliders
  * Rigidbody
  * AudioSource
  * Camera
  * MonoBehaviour (Mono and Il2Cpp)
  * MonoScript (Mono and Il2Cpp)

## Downloads

Download links can be found on the [latest release page](https://github.com/ds5678/AssetRipper/releases/latest).

## Structure

* [*AssetRipperCore*](AssetRipperCore/README.md) (Cross-Platform)

   Core library. It's designed as an single module without any third party dependencies.
   
* [*AssetRipperLibrary*](AssetRipperLibrary/README.md) (Cross-Platform)

   This is an expansion library for AssetRipperCore. It includes some third party dependencies and has some extra converters, so on Windows it additionally exports:
   * AudioClip .wav export
   * Texture2D .png export (with Sprites)
   * References to build-in Engine assets
   * Shader DirectX blob export

* [*AssetRipperGUI*](AssetRipperGUI/README.md) (Windows-only)

   Basic graphic interface application utilizing the AssetRipperLibrary.
   
* [*AssetRipperConsole*](AssetRipperConsole/README.md) (Cross-Platform)

   Command line equivalent of AssetRipperGUI. Since it has no GUI, it can be cross-platform compatible.
   
* [*UnitTester*](UnitTester/README.md) (Cross-Platform)

   Automated tester to verify project integrity while making changes.


## Requirements:

If you want to build a solution, you'll need:

 * [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)

 * Compiler with C# 9 syntax support, such as [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)


If you want to run binary files, you need to install:

 * [Unity 2017.3.0f3 or greater](https://unity3d.com/get-unity/download/archive) (NOTE: your editor version must be no less than the game's version)
 

## Discord

The development of this project has a dedicated [Discord server](https://discord.gg/XqXa53W2Yh). Feel free to come say hi. This is also an alternative location for people without Github accounts to post issues.


## To Do
 * Set up proper build actions
 * Predetermined GUID support


## Goals
 * Unity 2020 and 2021 support
 * Dummy shader implementation
 * NAudio implementation for exporting other audio formats
 * More Unity Component Exporters


## License

AssetRipper is licensed under the GNU General Public License v3.0


## Copyright Issues

Please be aware that distributing the output from this software may be against copyright legislation in your jurisdiction. You are responsible for ensuring that you're not breaking any laws.


## Credits

This began as a fork of [mafaca](https://github.com/mafaca)'s [uTinyRipper](https://github.com/mafaca/UtinyRipper) project licensed under the [MIT license](Licenses/uTinyRipper.md).

It has borrowed code from [Perfare](https://github.com/Perfare)'s [AssetStudio](https://github.com/Perfare/AssetStudio) project licensed under the [MIT license](Licenses/AssetStudio.md).

[Brotli](https://github.com/google/brotli) is licensed under the [MIT license](Licenses/Brotli.md).

[Cpp2IL](https://github.com/SamboyCoding/Cpp2IL) is licensed under the [MIT license](Licenses/Cpp2IL.md).

FMOD is licensed under a [non-commercial license](Licenses/FMOD.md).

[Lz4](https://github.com/lz4/lz4) is licensed under the [MIT license and the BSD 2-Clause license](Licenses/Lz4.md).

[Mono.Cecil](https://github.com/jbevain/cecil) is licensed under the [MIT license](Licenses/MonoCecil.md).

[Smolv](https://github.com/aras-p/smol-v) is licensed under the [MIT license](Licenses/Smolv.md).

[SpirV](https://github.com/Anteru/csspv) is licensed under the [BSD 2-Clause license](Licenses/SpirV.md).


## Disclaimer

This software is not sponsored by or affiliated with Unity Technologies or its affiliates. "Unity" is a registered trademark of Unity Technologies or its affiliates in the U.S. and elsewhere.