# Simulation Academy — .NET Diploma • Cycle 1

## Assignment

This repository contains my solutions for the **Simulation Academy .NET Diploma — Cycle 1** graded assignment.

The assignment focuses on:
- Refactoring procedural C++ into object-oriented C#
- OOP design, encapsulation, and validation
- The Builder Design Pattern
- LeetCode problem solving using C#

## Repository Structure

```text
.
├── README.md
├── Part1_ProceduralToOOP/
│   ├── Critique.md
│   ├── src/
│   ├── Part1_ProceduralToOOP.csproj
│   └── ...
├── Part2_HotelReservationSystem/
│   ├── src/
│   ├── Part2_HotelReservationSystem.csproj
│   └── ...
├── Part3_BuilderPattern/
│   ├── Answers.md
│   ├── src/
│   ├── Part3_BuilderPattern.csproj
│   └── ...
└── Part4_LeetCode/
    └── 1679_MaxNumberOfKSumPairs/
        ├── Solution.cs
        └── accepted_screenshot.png
```

## Part 1 — Procedural C++ → OOP C#

The original system manages Customers, Products, Orders, and Order Lines.

The C# version redesigns these as proper classes, removes global state, and preserves the main functionality of the original system.

`Critique.md` contains the design critique of the original procedural C++ implementation.

## Part 2 — Hotel Reservation System

This part implements a hotel reservation domain based on the manager's requirements.

The design focuses on:
- Encapsulation
- Get-only properties
- Private setters
- Read-only reservation collections
- Valid reservation status transitions
- Room maintenance rules
- Positive room pricing
- Date validation
- Preventing double-booking

## Part 3 — Builder Pattern

This part addresses the construction of an invoice with many properties.

It includes:
- Analysis of the problems with a large constructor
- A fluent `InvoiceBuilder`
- Mandatory and optional property validation
- Composed builders such as `AddressBuilder` and `OrderBuilder`

The Builder implementation uses normal C# methods and fluent chaining without LINQ or delegate types.

## Part 4 — LeetCode

### Problem 1679 — Max Number of K-Sum Pairs

The solution uses the required **Two Pointers** technique:
1. Sort the array.
2. Start one pointer at the beginning and one at the end.
3. Compare their sum with `k`.
4. Move the appropriate pointer(s) until they meet.

An accepted-submission screenshot is included in the problem folder.

## Technologies

- C#
- .NET
- C++
- Object-Oriented Programming
- Builder Design Pattern
- Git & GitHub
- LeetCode

## Notes

- Each assignment part is kept in its own project/folder.
- `Critique.md` and `Answers.md` contain the written answers.
- Build output such as `bin/` and `obj/` is not included.
- The repository is intended to follow the required Git branch and Pull Request workflow.
