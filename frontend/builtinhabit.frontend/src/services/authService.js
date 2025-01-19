import axios from 'axios';

const API_URL = 'https://localhost:7227/api/auth';
export const registerUser = async (userData) => {
    const response = await axios.post(`${API_URL}/register`, userData);
    return response.data;
};

export const loginUser = async (userData) => {
    const response = await axios.post(`${API_URL}/login`, userData);
    return response.data;
};

export const logout = () => {
    localStorage.removeItem('userId');
    sessionStorage.removeItem('userId');
};
