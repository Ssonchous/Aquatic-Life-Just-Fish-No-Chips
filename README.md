# 🐠 Aquatic Life: Just Fish (No Chips)  
**A dynamic aquarium simulator with AI-driven fish behaviors, built in C#/WPF** 
<img width="974" height="548" alt="image" src="https://github.com/user-attachments/assets/f67e1ec4-0b06-4f9a-851d-05ea0230107c" />
## 🌟 Features  
### Smart Fish AI:  
- 🍽️ **Seek Food**: Fish hunt for food when hungry (nutrition system)  
- 🦈 **Predator-Prey Dynamics**: Sharks/Piranhas attack smaller fish; prey flees  
- 🐟 **Schooling Behavior**: Fish form groups with leaders (Decorator pattern)  
- 💨 **Playful Moods**: Fish chase bubbles when bored!  

### Ecosystem:  
- Randomly generated food and bubbles  
- Dynamic fish interactions  

### Architecture:  
- Extensible OOP design  
- Easy to add new fish types/behaviors  

## 🚀 Installation  
**Prerequisite**: Move the `Img` folder to `bin/Debug` before running  

## 🛠 Tech Stack
**Language**: C#
**UI**: WPF
**Patterns**:
Decorator (dynamic behavior changes)
Strategy (interchangeable behaviors)
Observer (state updates)
📂 **Project Structure**
Src/  
├── Classes/  
│   ├── ActiveAquaSitting/      # Fish AI behaviors (hunting, fleeing, etc.)  
│   ├── AquaSitting/            # Food, bubbles, and interfaces  
│   └── Decorators/             # Fish modifiers (e.g., HunterDecorator)  
└── Main/                       # Entry point and aquarium logic  
