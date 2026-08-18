# Interview Answers - Phase 01

## Overview

This document contains answers and explanations for the main technical concepts covered during Phase 01.

The questions focus on C#, OOP, LINQ, SQL, databases, GitHub, and basic software engineering practices.

---

# C# & OOP

## 1. What is the difference between class and object?

A **class** is a blueprint that defines properties and behaviors.

An **object** is an actual instance created from that class.

Example:

```csharp
class Product
{
    public string Name { get; set; }
}

Product product = new Product();
