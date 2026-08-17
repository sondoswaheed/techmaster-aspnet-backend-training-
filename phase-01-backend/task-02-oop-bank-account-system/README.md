# TechMaster Bank Account System

A small console-based banking system developed as part of **TechMaster Academy – ASP.NET Backend Career Training, Phase 01**.

The project demonstrates core **Object-Oriented Programming (OOP)** concepts, encapsulation, business validation, service-layer logic, and transaction tracking using an in-memory collection.

---

## 📌 Project Overview

The system simulates an internal banking application where employees can:

- Create customer bank accounts
- Deposit money
- Withdraw money
- Transfer money between accounts
- View account details
- View transaction history
- View all accounts

Every financial operation is validated, and every successful financial operation creates a transaction record.

---

## 🎯 Learning Objectives

This project demonstrates:

- Encapsulation of sensitive data
- Using methods for object behavior
- Separation of Models, Services, and UI
- Business rule validation
- Working with Classes and Objects
- Enums
- Collections
- LINQ
- Exception-safe input handling
- Console-based user interaction

---

## 🏗️ Project Structure

```text
task-02-bank-account-system/
│
├── README.md
│
└── BankAccountSystem/
    │
    ├── Models/
    │   ├── Customer.cs
    │   ├── BankAccount.cs
    │   ├── Transaction.cs
    │   ├── AccountType.cs
    │   └── TransactionType.cs
    │
    ├── Services/
    │   └── BankService.cs
    │
    ├── UI/
    │   └── ConsoleMenu.cs
    │
    └── Program.cs
