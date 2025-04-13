# Final Engine 1.0

## Core

### Windowing and Input

- [x] Windowing System
- [x] Game Loop
- [x] Keyboard Handling
- [x] Mouse Handling
- [x] Game Pad Handling

### Scenes and Entities

- [ ] Scene Manager
- [x] ECS Framework
- [x] Resource Manager

### Audio

- [x] Playback

## Rendering

### Rendering Techniques

- [x] Pipeline
  - [x] Forward Rendering

### Geometry and Models

- [x] Mesh Rendering
- [x] Model Loading (using Assimp)
- [x] 2D Sprite Batching

### Camera and View Optimization

- [x] Camera System
- [ ] View Frustum Culling

### Material and Shading

- [ ] Material System
  - [x] Diffuse Mapping
  - [x] Specular Mapping
  - [x] Normal Mapping
  - [x] Emissive Mapping
  - [ ] Parallax Mapping

### Lighting and Shadows

- [x] Lighting
  - [x] Blinn-Phong Lighting Model
- [x] Light Sources
  - [x] Directional
  - [x] Point
  - [x] Spot
- [x] Global Illumination (Ambient Lighting)

### Visual Effects

- [x] Skyboxes
- [ ] Blending

### Post Processing

- [ ] Post Processing
  - [ ] Gamma Correction
  - [x] High Dynamic Range (HDR)
  - [x] Anti-Aliasing
  - [ ] Fog
    - [ ] Linear
    - [ ] Exponential
    - [ ] Exponential Squared

## Editor

### General

- [ ] Menu bar with basic actions (File, Edit, View)
- [x] Resizable and dockable panels
- [ ] Consistent visual styling/theme
- [ ] Undo and Redo of common functionally

---

### Projects

- [ ] Create new project
- [ ] Open existing project
- [ ] Display current project name

---

### Scenes

- [ ] Create new scene
- [ ] Save current scene to file
- [ ] Open existing scene from file
- [ ] Indicate unsaved changes (e.g. with asterisk in tab or title)

---

### Entities

- [x] Display list of entities in the current scene
- [x] Select entities
- [x] Add new entity
- [x] Delete entity
- [ ] Rename entity

### Components

- [x] Display selected entity's components
- [x] Add new component to entity
- [x] Remove component from entity
- [x] Edit component values (e.g. Transform, Sprite)
- [x] Reflect real-time changes in the scene

---

### Systems

- [ ] View list of systems in the current scene
- [ ] Add system to scene
- [ ] Remove system from scene

---

### Viewport

- [x] Render scene with all visible entities
- [ ] Select entities by clicking in the viewport
- [ ] Camera controls (pan and zoom)
- [ ] Display and interact with transform gizmos (position, rotation, scale)

---

### Resources

- [ ] Import assets (e.g. textures, sounds)
- [ ] Display list/grid of imported resources
- [ ] Preview basic asset information
- [ ] Assign resources to components (e.g. SpriteRenderer)

---

### Console and Logs

- [ ] Display error, warning, and info messages
- [ ] Filter logs by type
- [ ] Clear logs