# Habit Tracker Project

## Overview
The Habit Tracker is a productivity application designed to help users build and maintain habits by tracking their daily progress.
This project demonstrates my fullstack development skills, including backend API creation, frontend UI development, database management, and data visualization.

---

## Features
- **User Authentication:** Register, login, and manage personal accounts.
- **Habit Management:** Create, edit, delete, and view habits.
- **Progress Tracking:** Track daily habit completion with streaks and badges.
- **Data Visualization:** Graphs and charts for habit trends.
- **Mobile-Friendly Design:** Responsive interface for all devices.

---

## Tech Stack
- **Frontend:** JavaScript, Bootstrap.
- **Backend:** C# with ASP.NET.
- **Database:** MongoDB.
- **Other Tools:** Visual Studio, Git, Chart.js.

---

## Project Architecture
### App Flow
1. **Frontend:**
   - Displays the user interface for habit management and visualization.
   - Communicates with the backend via REST APIs.
2. **Backend:**
   - Handles API requests for user authentication, habit CRUD operations, and data retrieval.
   - Implements business logic for tracking streaks and analytics.
3. **Database:**
   - Stores user data, habits, and progress logs.

### API Endpoints
| Endpoint               | Method | Description                 |
|------------------------|--------|-----------------------------|
| `/api/auth/register`   | POST   | Register a new user         |
| `/api/auth/login`      | POST   | Authenticate a user         |
| `/api/habits`          | GET    | Fetch all habits for a user |
| `/api/habits`          | POST   | Create a new habit          |
| `/api/habits/:id`      | PUT    | Update an existing habit    |
| `/api/habits/:id`      | DELETE | Delete a habit              |

---

## Usage Instructions
1. Register an account and log in.
2. Create new habits and mark them as completed daily.
3. View streaks and analytics on the dashboard.
4. Edit or delete habits as needed.

---
