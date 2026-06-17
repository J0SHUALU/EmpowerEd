# EmpowerEd — A Skills-Reform Education Simulation

An interactive Unity simulation built for the Grand Challenges and Global Opportunities (GCGO) assignment. The player explores a virtual classroom in first person and completes short, interactive life-skills lessons that teach the practical skills people need to rebuild their lives.

---

## GCGO Statement

**Grand Challenge:** Homelessness and rehabilitation in Nigeria.

**Mission Statement:** To help reform and empower homeless people by teaching them practical life skills, and to use technology to make that skills-based education engaging, accessible, and easy to deliver.

---

## Problem Context

**The problem being addressed:** Many homeless people in Nigeria remain trapped in poverty not only because of a lack of shelter, but because of limited access to the practical skills needed to find work and manage their lives, budgeting, job applications, health and hygiene, and literacy.

**Why it matters:** Lasting reform comes from empowerment, not just charity. Teaching skills helps people regain independence and dignity. But skills training is difficult to scale and is often inaccessible to the people who need it most.

**How it connects to my GCGO:** This simulation demonstrates how an interactive, gamified classroom can teach core life skills in a simple, repeatable, low-cost way. By stepping into the role of a learner and working through real lessons, the player experiences how skills-based education can support rehabilitation. The goal is awareness, empathy, and a model for engaging training.

---

## Simulation Overview

**What the simulation does:** Starting from a main menu, the player enters a virtual classroom and walks around it in first person. Four skill stations, each represented by a book, are placed around the room: Budgeting, Job & CV, Hygiene & Health, and Reading & Literacy. Looking at a station and clicking opens a short lesson, a question with multiple-choice answers. A correct answer plays a confirmation sound, teaches a key point, and marks the station complete; a wrong answer plays an error sound and lets the player try again. A glowing guide line points to the next station. Completing all four "graduates" the learner, triggering a win screen with a cheer sound and confetti.

**Target users:** Learners in rehabilitation and skills programs, educators, NGOs, and anyone learning about skills-based reform. The simulation is also suitable for awareness campaigns and classroom demonstration.

**Key interactions:**
- Start the experience from the main menu (Play / Quit).
- Walk around the classroom in first person and look around.
- Look at a skill station (book) and click or tap to open its lesson.
- Choose an answer from the lesson popup.
- Receive immediate colour-coded feedback, a sound, and a short teaching note.
- Follow the guide line to the next station.
- Complete all four stations to finish and see the win screen.

---

## Unity Mechanics Implemented

**User Interface (UI):**
Built with a Canvas and TextMeshPro. The UI includes a live progress counter ("Skills Learned: X / 4"), a context-sensitive hint prompt, a lesson popup (question, three answer buttons, and feedback text), and a win panel with restart and menu buttons. The `UIManager` script drives every piece, updating it in response to game events. Feedback text turns green for a correct answer and red for a wrong one. A separate main-menu Canvas provides the Play and Quit buttons.

**Scripting:**
The simulation is built from focused C# scripts, each with a single responsibility:
- `GameManager` — tracks completed stations and the win condition (singleton pattern).
- `PlayerInteraction` — raycasts forward from the first-person camera to detect the station the player is looking at, and opens it on click.
- `SkillStation` — holds one station's lesson content, checks the player's answer, and plays the correct/wrong sounds.
- `GuideLineRenderer` — draws the guide line to the next incomplete station.
- `UIManager` — all UI display, lesson logic, freeing the cursor during lessons, and triggering the win cheer and confetti.
- `MainMenu` — loads the simulation scene and quits the app.

**Collision:**
Each skill station (book) has a solid collider so the raycast can detect it, plus a second collider set as a trigger to represent its interactive zone. This demonstrates both a solid collider (for raycast hits) and a trigger (for proximity). The first-person controller also physically collides with the classroom floor and walls.

**Raycasting:**
`PlayerInteraction` casts a ray forward from the centre of the first-person camera every frame. If it hits a station within range, a hint appears; on click, it opens that station's lesson. Raycasting is what lets the player select the specific object they are looking at.

**Line Renderer:**
`GuideLineRenderer` draws a glowing line from a guide point to the nearest station that has not yet been completed. It updates every frame and hides itself once all stations are done, visually guiding the learner through the lessons in order.

---

## New Unity Input System

All interactions use the new Unity Input System (the default in Unity 6).

- An Input Actions asset (`EmpowerEdControls`) defines the actions: **Move** (Vector2), **Look** (Vector2), and **Interact** (Button — left click / E / tap).
- A `PlayerInput` component on the First Person Controller, set to **Invoke Unity Events**, routes the Interact action to `PlayerInteraction.OnInteract`. Movement and look drive the first-person controller.
- The Event System uses the **Input System UI Input Module** so UI buttons work correctly in builds.
- Touch bindings are included so interaction works on mobile.

---

## Additional Features (Beyond Module Scope)

- **Audio system:** Background music in both the menu and game scenes, distinct correct-answer and wrong-answer sound effects on each skill station, and a cheer sound on the win screen.
- **Particle system:** A confetti burst plays on the win screen when the learner completes all four stations.
- **Main menu and scene management:** A separate Main Menu scene with Play and Quit buttons loads the simulation using Unity's `SceneManager`, demonstrating multi-scene flow.
- **Cross-platform touch input:** A touch tap binding allows the same Interact action to work on mobile as well as desktop.

---

## Build Information

**WebGL deployment link:** _https://play.unity.com/en/games/5e18bff2-7064-4b17-8b19-408d8010bb83/webgl-build_

**Android build (APK) link:** _https://drive.google.com/drive/folders/1VHYGWuo-S6t-b1LylDafyMmhXMrFDEhi?usp=sharing_

**Instructions for running the project:**

*To play the WebGL build:* open the deployment link in a desktop browser (Chrome recommended). On the main menu, click Play. Click the game window once to lock the mouse, then use WASD to move, the mouse to look, and left-click or E to interact with a book. Click an answer to respond.

*To play on Android:* download and install the APK, open the app, tap Play on the menu, then tap books and answers.

*To open the project in Unity:*
1. Open the project in Unity 6 (6000.x) with the Built-In Render Pipeline.
2. Import the free **School assets** and **Mini First Person Controller** packages from the Asset Store.
3. Open `Assets/Scenes/MainMenu.unity`.
4. Press Play.

---

## Demo Link
_https://youtu.be/zrbIsY8YsWA_

---

## Controls

| Action | Keyboard / Mouse | Touch |
|--------|------------------|-------|
| Move | WASD | (on-screen, if added) |
| Look | Mouse | Drag |
| Interact / answer | Left click or E | Tap |

---

## Credits / Third-Party Assets

- **School assets** by A.R.S|T. — free low-poly school environment pack, Unity Asset Store (Standard Unity Asset Store EULA). Used for the classroom environment only.
- **Mini First Person Controller** — free Unity Asset Store package, used for first-person movement and camera look.
- Sound effects and music: _https://www.myinstants.com/_.

All scripts, mechanics, scene logic, lessons, and UI were created by me. Third-party assets are used for the environment, basic player movement, and audio only.

---


