# RoadCompany Personnel Management System

A WPF desktop application for HR personnel management, built on .NET Framework 4.7.2.

---

## Overview

**RoadCompany** is a desktop application designed for human resources departments to manage company personnel efficiently. It provides tools for managing employee records, organizational structure, and employee-related events such as trainings, vacations, and absences.

The application follows the **MVVM (Model-View-ViewModel)** pattern and uses **Entity Framework** for database access.

---

## Features

### Employee Management
| Feature | Description |
|---------|-------------|
| Create | Add new employees with personal and professional details |
| Edit | Update employee information |
| View | Comprehensive employee cards with all data |
| Dismiss | Remove employees with automatic cleanup of future events |

### Organizational Structure
| Feature | Description |
|---------|-------------|
| Departments | View and manage company departments |
| Positions | Define and assign job positions |
| Hierarchy | Set up manager and assistant relationships |

### Event Management
| Feature | Description |
|---------|-------------|
| Add | Assign trainings, absences, vacations, and time off |
| Filter | Filter events by past, current, or future status |
| Delete | Remove unwanted events |
| Conflict Detection | Automatic detection of overlapping events |

### Validation & Data Integrity
- Real-time field validation
- Email and phone format validation
- Business rule enforcement (e.g., no dismissal with upcoming trainings)
