import React, { useState } from 'react';
import { Button, Form, Container } from 'react-bootstrap';
import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const { setUserId } = useAuth();
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('https://localhost:7227/api/auth/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email, password })
            });
            if (!response.ok) throw new Error('Invalid credentials');
            const data = await response.json();
            setUserId(data.userId);
            navigate('/');
        } catch (error) {
            setError('Failed to log in. Please check your credentials.');
        }
    };

    const handleRegisterRedirect = () => {
        navigate('/register');
    };

    return (
        <Container className="mt-5">
            <h2>Login</h2>
            <Form onSubmit={handleLogin}>
                <Form.Group className="mb-3" controlId="formEmail">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control
                        type="email"
                        placeholder="Enter email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control
                        type="password"
                        placeholder="Enter password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </Form.Group>

                {error && <div className="alert alert-danger">{error}</div>}

                <Button variant="primary" type="submit">
                    Log In
                </Button>
            </Form>
            <div className="mt-3">
                <p>Do not have an account?</p>
                <Button variant="primary" onClick={handleRegisterRedirect}>
                    Register
                </Button>
            </div>
        </Container>
    );
};

export default Login;
