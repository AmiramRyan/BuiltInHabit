import React, { useState } from 'react';
import { addHabit } from '../services/habitService';

const HabitForm = () => {
  const [habitName, setHabitName] = useState('');
  const [habitDescription, setHabitDescription] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!habitName || !habitDescription) {
      alert('Please fill in all fields');
      return;
    }

    const newHabit = {
      name: habitName,
      description: habitDescription,
    };

    try {
      await addHabit(newHabit);
      alert('Habit added!');
      setHabitName('');
      setHabitDescription('');
    } catch (error) {
      console.error('Error adding habit:', error);
    }
  };

  return (
    <div>
      <h2>Add New Habit</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Habit Name"
          value={habitName}
          onChange={(e) => setHabitName(e.target.value)}
        />
        <input
          type="text"
          placeholder="Habit Description"
          value={habitDescription}
          onChange={(e) => setHabitDescription(e.target.value)}
        />
        <button type="submit">Add Habit</button>
      </form>
    </div>
  );
};

export default HabitForm;
