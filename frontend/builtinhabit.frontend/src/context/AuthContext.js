import React, { createContext, useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { logout as authLogout } from '../services/authService';


const AuthContext = createContext();

export const useAuth = () => {
    return useContext(AuthContext);
};

export const AuthProvider = ({ children }) => {
    const [userId, setUserId] = useState(localStorage.getItem('userId') || null);

    const login = (id) => {
        setUserId(id);
        localStorage.setItem('userId', id);
    };

    const logout = () => {
        authLogout();
        setUserId(null);
        localStorage.removeItem('userId');
    };

    return (
        <AuthContext.Provider value={{ userId, setUserId: login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};
