import React, { useEffect, useState } from 'react';
import { getHabitsByUserId, deleteHabit, updateHabit } from '../services/habitService';
import { Modal, Button } from 'react-bootstrap';

const frequencyMap = {
    0: 'Daily',
    1: 'Weekly',
    2: 'Monthly'
};

const HabitList = ({ userId, showAlert }) => {
    const [habits, setHabits] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [habitToDelete, setHabitToDelete] = useState(null);

    const fetchHabits = async () => {
        try {
            const data = await getHabitsByUserId(userId);
            setHabits(data);
        } catch (error) {
            console.error(error);
            showAlert('Failed to fetch habits.', 'danger');
        }
    };

    const handleDelete = (id) => {
        setHabitToDelete(id);
        setShowModal(true);
    };

    const confirmDelete = async () => {
        try {
            await deleteHabit(habitToDelete);
            showAlert('Habit deleted successfully.', 'success');
            fetchHabits();
            setShowModal(false);
        } catch (error) {
            console.error(error);
            showAlert('Failed to delete habit.', 'danger');
            setShowModal(false);
        }
    };

    const cancelDelete = () => {
        setShowModal(false);
    };

    const handleToggleCompleted = async (habit) => {
        try {
            const compleatedStatus = (!habit.completed).toString();
            await updateHabit(habit.id, 'Completed', compleatedStatus);
            showAlert('Habit status updated.', 'success');
            fetchHabits();
        } catch (error) {
            console.error(error);
            showAlert('Failed to update habit.', 'danger');
        }
    };

    useEffect(() => {
        fetchHabits();
    }, []);

    const getFrequencyLabel = (frequency) => {
        switch (frequency) {
            case 0:
                return 'Daily';
            case 1:
                return 'Weekly';
            case 2:
                return 'Monthly';
            default:
                return 'Daily';
        }
    };
    const getStatusColor = (completed) => { return completed ? 'bg-success' : 'bg-danger'; };

    const getFrequencyColor = (frequency) => {
        switch (frequency) {
            case 0:
                return 'bg-success'; //Daily
            case 1:
                return 'bg-primary'; //Weekly
            case 2:
                return 'bg-info'; //Monthly
            default:
                return 'bg-secondary';
        }
    };

    
    return (
        <div>
            <h2>Your Habits</h2>
            <ul className="list-group">
                {habits.map((habit) => (
                    <li key={habit.id} className="list-group-item d-flex justify-content-between align-items-center">
                        <span>
                            <strong>{habit.name}</strong>: {habit.description} -{' '}
                            <span className={`badge rounded-pill ${getStatusColor(habit.completed)}`}>
                                {habit.completed ? 'Completed' : 'Incomplete'}
                            </span>
                            <span className="ms-2">
                                <span className={`badge rounded-pill ${getFrequencyColor(habit.habitFrequency)}`}>
                                    {getFrequencyLabel(habit.habitFrequency)}
                                </span>
                            </span>
                        </span>
                        <div>
                            <button
                                className="btn btn-success btn-sm me-2"
                                onClick={() => handleToggleCompleted(habit)}
                            >
                                Toggle Status
                            </button>
                            <button
                                className="btn btn-danger btn-sm"
                                onClick={() => handleDelete(habit.id)}
                            >
                                Delete
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default HabitList;
