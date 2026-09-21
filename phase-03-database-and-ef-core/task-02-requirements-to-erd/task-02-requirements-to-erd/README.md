# TechMaster Academy - Entity Design

## Overview
Database design for managing students, instructors,
training tracks, enrollments, and payments.

## Entities
- Student
- Instructor
- TrainingTrack
- Enrollment
- Payment

## Design Decisions
- Enrollment is used as the relationship between Student and TrainingTrack.
- Email is unique for Student and Instructor.
- TrainingTrack Code is unique.
- Payment is linked to Enrollment.
- Enums are used for statuses and payment methods.

## Relationships
Student 1 → Many Enrollments
Instructor 1 → Many TrainingTracks
TrainingTrack 1 → Many Enrollments
Enrollment 1 → Many Payments
