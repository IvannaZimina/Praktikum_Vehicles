# Vehicles C# WPF Application

## Project Overview
This project is developed for the **Vehicles practical assignment**. The primary objective is to implement a WPF application integrating Object-Oriented Programming (OOP) principles with collection management.

---

## Project Structure & Architecture
The solution adheres to a strict separation of concerns utilizing the **MVVM (Model-View-ViewModel)** architectural pattern:

1. **Vehicles.Core**: Contains all domain classes, interfaces, business logic, rules, and validation. This layer remains completely independent of the user interface and supports isolated unit testing.
2. **Vehicles.WpfApp.ViewModels**: Contains `MainViewModel` implementing `INotifyPropertyChanged`. This layer manages application state, garage collections, data binding properties, and action execution without directly referencing UI elements.
3. **Vehicles.WpfApp**: The user interface (**View**) constructed with XAML and code-behind. MVVM and data binding implementation keeps the code-behind minimal, restricting its responsibility strictly to visual elements and animations.

---

## Key OOP Concepts (Based on Assignment Requirements)

### 1. Abstract Base Class and Polymorphism (`Vehicle`)
- **Assignment requirement**: Demonstrate inheritance, polymorphism, and an abstract `Vehicle` class.
- **Implementation details**: The `Vehicle` class is defined as abstract to prevent direct instantiation. It exposes shared read-only properties (`Make`, `Model`, `Odometer`) and declares an abstract method `Move(double km)` implemented polymorphically across all derived classes.

### 2. Role-Based Interfaces (`IDriveable` & `ISwimmable`)
- **Assignment requirement**: Implement at least two distinct interfaces. `Car` and `Boat` represent separate behavioral roles, while `AmphibiousCar` supports both.
- **Implementation details**: Role-based interfaces decouple behavior from rigid inheritance hierarchies:
  - `IDriveable`: Designed for land-based vehicles (implemented by `Car`).
  - `ISwimmable`: Designed for watercraft (implemented by `Boat`).
  - `AmphibiousCar`: Implements both interfaces to support dual-mode operation (driving and swimming).

### 3. Encapsulation and Validation
- **Assignment requirement**: Zero, negative, or invalid input values must not corrupt object state.
- **Implementation details**: Movement methods validate distance parameters prior to execution. Invalid inputs trigger exceptions, preventing unauthorized odometer modifications.

### 4. Multiple Garage Collections (`VehicleGarage`)
- **Assignment requirement**: Manage vehicles grouped within specialized collection structures.
- **Implementation details**: Two independent `VehicleGarage` instances (`CarGarage` and `BoatGarage`) manage separate `ObservableCollection<Vehicle>` lists for cars and boats within the view model layer.

### 5. Event-Driven Notifications & Visual Feedback
- **Assignment requirement**: Implement automated event-driven behaviors (such as acoustic notifications upon addition and headlight flashing upon removal) accompanied by comprehensive activity logging.
- **Implementation details**: 
  - Vehicle addition triggers automated notification events logged via `OnLogMessage`.
  - Vehicle removal invokes visual headlight flashing across remaining vehicles.
- **Visual Indicator Animation**: UI list items feature interactive headlight elements (`Ellipse`) dynamically illuminating in bright yellow for a duration of 2 seconds upon vehicle removal.

### 6. Modern WPF User Interface & MVVM Dynamic Interaction (`Vehicles.WpfApp`)
- **UI Design**: Developed using a modern **Dark Theme** featuring deep slate and indigo gradients, rounded containers (`CornerRadius`), and a symmetrical two-column layout.
- **Left Panel**: Houses primary action buttons, followed by a structured "Selected Vehicle" control panel and operational input fields.
- **Right Panel**: Contains side-by-side garage lists (`Car Garage` and `Boat Garage`) positioned above a full-width "Activity Log" occupying one-third of the vertical layout.
- **Code Optimization**: Single-line methods in the code-behind leverage modern C# expression-bodied member syntax (`=>`) for clean and maintainable event delegation to the view model.

---

## How to Run
1. Open the solution file (`.sln`) in Visual Studio.
2. Set `Vehicles.WpfApp` as the startup project.
3. Build and execute the application (`F5`).

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



