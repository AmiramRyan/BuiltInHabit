import React, { useState } from 'react';
import { createHabit } from '../services/habitService';
import { useNavigate } from 'react-router-dom';

// Frequency mapping
const frequencyOptions = [
    { value: 0, label: 'Daily' },
    { value: 1, label: 'Weekly' },
    { value: 2, label: 'Monthly' }
];

const HabitForm = ({ userId, showAlert }) => {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [habitFrequency, setHabitFrequency] = useState(0);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        const habitData = {
            id: "",
            name,
            description,
            userId,
            habitFrequency
        };

        try {
            await createHabit(habitData);
            showAlert('Habit created successfully!', 'success');
            navigate('/');
        } catch (error) {
            console.error(error);
            showAlert('Failed to create habit.', 'danger');
        }
    };

    return (
        <div>
            <h2>Create a New Habit</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Habit Name</label>
                    <input
                        type="text"
                        className="form-control"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label>Description</label>
                    <textarea
                        className="form-control"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        required
                    ></textarea>
                </div>
                <div className="form-group">
                    <label>Frequency</label>
                    <select
                        className="form-control"
                        value={habitFrequency}
                        onChange={(e) => setHabitFrequency(Number(e.target.value))}
                    >
                        {frequencyOptions.map((option) => (
                            <option key={option.value} value={option.value}>
                                {option.label}
                            </option>
                        ))}
                    </select>
                </div>
                <button type="submit" className="btn btn-primary mt-3">
                    Create Habit
                </button>
            </form>
        </div>
    );
};

export default HabitForm;
