import React, { useState, useEffect } from 'react';
import { getHabits, deleteHabit } from '../services/habitService';

const HabitList = () => {
  const [habits, setHabits] = useState([]);

  useEffect(() => {
    const fetchHabits = async () => {
      try {
        const data = await getHabits();
        setHabits(data);
      } catch (error) {
        console.error('Error fetching habits:', error);
      }
    };

    fetchHabits();
  }, []); // Empty dependency array ensures it runs only once on mount

  const handleDelete = async (id) => {
    try {
      await deleteHabit(id);
      setHabits(habits.filter(habit => habit.id !== id));
    } catch (error) {
      console.error('Error deleting habit:', error);
    }
  };

  return (
    <div>
      <h2>Habit List</h2>
      <ul>
        {habits.map((habit) => (
          <li key={habit.id}>
            {habit.name}
            <button onClick={() => handleDelete(habit.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default HabitList;
