# Task 05 - Debug & Refactor Pack

## Overview

This project refactors a messy Order Calculator written by a junior developer.

The goal of the task is to improve code quality, readability, validation, and maintainability without changing the original business rules.

---

## Business Rules

The application follows these rules:

- Price must be positive.
- Quantity must be positive.
- Customer name cannot be empty.
- Product name cannot be empty.
- Tax is 14%.
- Shipping is 50 if the amount after discount is below 1000.
- Shipping is free if the amount after discount is 1000 or more.
- Discount is applied before tax.
- Tax is applied after discount.
- Shipping is added after tax.

---

# Project Structure

```text
OrderCalculator
│
├── Models
│   ├── Customer.cs
│   ├── CustomerType.cs
│   └── Order.cs
│
├── Services
│   └── OrderCalculatorService.cs
│
├── UI
│   └── ConsoleMenu.cs
│
├── Original
│   └── Program.cs
│
├── Program.cs
└── README.md
