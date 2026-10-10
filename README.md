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
- **Why we did it**: Inside movement methods, we check if the distance is greater than zero. If the input is invalid, it throws an exception and the odometer does not change.

### 4. Multiple Garage Collections (`VehicleGarage`)
- **Assignment requirement**: Manage vehicles grouped in separate specialized structures/garages.
- **Why we did it**: We implemented two independent `VehicleGarage` instances (`CarGarage` and `BoatGarage`) managing separate `ObservableCollection<Vehicle>` lists for cars and boats.

### 5. Event-Driven Notifications & Visual Feedback
- **Assignment requirement**: Automated event-driven behaviors (honking on arrival, flashing headlights on removal) backed by clear activity log records.
- **Why we did it**: 
  - When a vehicle is added to a garage, existing vehicles automatically "honk" (logged via `OnLogMessage`).
  - When a vehicle is removed from a garage, remaining vehicles automatically trigger headlight flashing.
  - **Visual Indicator Animation**: Each vehicle in the UI lists features interactive headlight elements (`Ellipse`) that dynamically light up in bright yellow for 2 seconds whenever a vehicle leaves the garage.

### 6. Modern WPF User Interface & Dynamic Interaction (`Vehicles.WpfApp`)
- **UI Design**: Built with a sleek, modern **Dark Theme** featuring deep slate/indigo gradients, rounded containers (`CornerRadius`), and a symmetrical 2-column layout.
  - **Left Panel**: Top action buttons, followed by a neatly docked "Selected Vehicle" control panel and operational inputs.
  - **Right Panel**: Side-by-side garage lists (`Car Garage` and `Boat Garage`) at the top, and a full-width bottom "Activity Log" taking up 1/3 height.
- **Dynamic Interface-Driven Controls**: 
  - When a vehicle is selected, the application dynamically checks implemented interfaces.
  - `IDriveable` vehicles enable the **Drive** button; `ISwimmable` vehicles enable the **Swim** button.
  - Non-supported actions automatically disable their respective buttons with visual feedback.

---

## How to Run
1. Open the solution file (`.sln`) in Visual Studio.
2. Set `Vehicles.WpfApp` as the startup project.
3. Build and run the application (`F5`).

---

## View Version 1.0
<img width="698" height="442" alt="image" src="https://github.com/user-attachments/assets/d775595b-f6b3-4320-8e8e-83ce92083d21" />
<img width="698" height="468" alt="image" src="https://github.com/user-attachments/assets/2d2fb23e-6cce-4aff-99da-6b5aca4b017c" />
<img width="703" height="434" alt="image" src="https://github.com/user-attachments/assets/8f127a56-6dcb-46ec-a84e-8ac36e92b600" />
<img width="695" height="437" alt="image" src="https://github.com/user-attachments/assets/f33c814b-c630-4ba9-95ab-51d3425858cb" />

## View Version 2.0
<img width="835" height="551" alt="image" src="https://github.com/user-attachments/assets/3032ba73-8cf1-4ebf-9490-06ccf992d67e" />
<img width="833" height="565" alt="image" src="https://github.com/user-attachments/assets/739613d8-729b-460a-8ea0-d6126b2b02a1" />
<img width="829" height="565" alt="image" src="https://github.com/user-attachments/assets/96aa8b9e-d902-4c4d-9f60-c7c78ec8fb71" />



