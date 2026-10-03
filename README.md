# Vehicles C# WPF Application

## Project Overview
This project is built for the **Vehicles practical assignment**. The main goal is to create a WPF application where Object-Oriented Programming (OOP) and collections work together. 

---

## Project Structure & Architecture
As required, the solution is split into two separate parts:
1. **Vehicles.Core**: Contains all classes, business logic, rules, and validation. It does not know anything about WPF and can be tested separately.
2. **Vehicles.WpfApp**: The user interface (UI) responsible for input, events, and displaying results. It uses the Core project.

---

## Key OOP Concepts (Based on Assignment Requirements)

### 1. Abstract Base Class and Polymorphism (`Vehicle`)
- **Assignment requirement**: Show inheritance, polymorphism, and an abstract `Vehicle` class.
- **Why we did it**: The `Vehicle` class is abstract so you cannot create it directly. It holds shared properties (`Make`, `Model`, `Odometer`) which are read-only from the outside. 
- It also has an abstract method `Move(double km)`, which works polymorphically in all child classes.

### 2. Role-Based Interfaces (`IDriveable` & `ISwimmable`)
- **Assignment requirement**: Use at least two different interfaces. `Car` and `Boat` implement different roles, while `AmphibiousCar` supports both.
- **Why we did it**: Instead of a strict hierarchy, we use interfaces for specific roles:
  - `IDriveable`: For vehicles that drive (implemented by `Car`).
  - `ISwimmable`: For watercraft (implemented by `Boat`).
  - `AmphibiousCar`: Implements **both** interfaces because it can both drive and swim.

### 3. Encapsulation and Validation
- **Assignment requirement**: Zero, negative, or invalid input must not break the object state.
- **Why we did it**: Inside the `Move(double km)` method, we check if the distance is greater than zero. If the input is invalid, it throws an exception and the odometer does not change.

### 4. Single Collection (`ObservableCollection<Vehicle>`)
- **Assignment requirement**: All objects must be in one `ObservableCollection<Vehicle>` collection.
- **Why we did it**: We store cars, boats, and amphibious cars together in one list, which makes it easy to display them in the WPF user interface.

### 5. Modern WPF User Interface & Dynamic Interaction (`Vehicles.WpfApp`)
- **UI Design**: Built with a sleek, modern **Dark Theme** featuring deep slate/indigo gradients, rounded containers (`CornerRadius`), and smooth visual hierarchy.
- **Dynamic Interface-Driven Controls**: 
  - When a vehicle is selected in the list (`VehicleListBox`), the application dynamically checks which interfaces it implements.
  - **`IDriveable` vehicles** (like Cars and Amphibious Cars) enable the **Drive** button.
  - **`ISwimmable` vehicles** (like Boats and Amphibious Cars) enable the **Swim** button.
  - Non-supported actions automatically disable their respective buttons, turning them into a clean, muted state with visual feedback.
- **Activity Log & Validation**: All actions (driving, sailing, errors, or removals) are recorded in real-time in the scrollable activity log box with input error handling.

---

## How to Run
1. Open the solution file (`.sln`) in Visual Studio.
2. Set `Vehicles.WpfApp` as the startup project.
3. Build and run the application (`F5`).