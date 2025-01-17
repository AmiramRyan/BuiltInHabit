import axios from 'axios';

const API_URL = 'https://localhost:7227/api/Habit';

export const getHabits = async () => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error('Error fetching habits:', error);
    throw error;
  }
};

export const addHabit = async (habit) => {
  try {
    const response = await axios.post(API_URL, habit);
    return response.data;
  } catch (error) {
    console.error('Error adding habit:', error);
    throw error;
  }
};

export const updateHabit = async (id, habit) => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, habit);
    return response.data;
  } catch (error) {
    console.error('Error updating habit:', error);
    throw error;
  }
};

// Ensure deleteHabit is properly exported
export const deleteHabit = async (id) => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error) {
    console.error('Error deleting habit:', error);
    throw error;
  }
};
