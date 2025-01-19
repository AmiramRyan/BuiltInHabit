import axios from 'axios';

const API_BASE_URL = 'https://localhost:7227/api/habits';
export const createHabit = async (habit) => {
    const response = await axios.post(`${API_BASE_URL}/create`, habit);
    return response.data;
};

export const getHabitsByUserId = async (userId) => {
    const response = await axios.get(`${API_BASE_URL}/user/${userId}`);
    return response.data;
};

export const updateHabit = async (habitId, fieldName, newValue) => {
    const response = await axios.patch(`${API_BASE_URL}/${habitId}`, { fieldName, newValue });
    return response.data;
};

export const deleteHabit = async (habitId) => {
    await axios.delete(`${API_BASE_URL}/${habitId}`);
};