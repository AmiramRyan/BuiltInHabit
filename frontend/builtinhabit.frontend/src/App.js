import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import HabitForm from './components/HabitForm';
import HabitList from './components/HabitList';
import Login from './components/Login';
import Register from './components/Register';
import { useAuth } from './context/AuthContext';
import 'bootstrap/dist/css/bootstrap.min.css';

const App = () => {
    const { userId, logout } = useAuth();
    const [showHabitList, setShowHabitList] = useState(true);

    return (
        <Router>
            <div className="container">
                <h1>Habit Tracker</h1>
                <div className="btn-group">
                    <button
                        className="btn btn-primary"
                        onClick={() => setShowHabitList(true)} // Show Habit List
                    >
                        My Habits
                    </button>
                    <button
                        className="btn btn-primary"
                        onClick={() => setShowHabitList(false)} // Show New Habit Form
                    >
                        New Habit
                    </button>
                </div>
                {userId ? (
                    <>
                        {showHabitList ? (
                            <HabitList userId={userId} showAlert={(msg, type) => alert(msg)} />
                        ) : (
                            <HabitForm userId={userId} showAlert={(msg, type) => alert(msg)} />
                        )}
                        <button onClick={logout} className="btn btn-danger mt-3">
                            Log Out
                        </button>
                    </>
                ) : (
                    <Navigate to="/login" />
                )}
            </div>
        </Router>
    );
};

export default App;
