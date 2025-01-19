import React, { useState } from 'react';
import { Button, Form, Container } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

const Register = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleRegister = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch('https://localhost:7227/api/auth/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ username, email, password })
            });

            if (!response.ok) throw new Error('User already exists or invalid data');
            navigate('/login');
        } catch (error) {
            setError('Registration failed. Try again later.');
        }
    };

    return (
        <Container className="mt-5">
            <h2>Register</h2>
            <Form onSubmit={handleRegister}>
                <Form.Group className="mb-3" controlId="formUsername">
                    <Form.Label>Username</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                    />
                </Form.Group>

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
                    Register
                </Button>
            </Form>
        </Container>
    );
};

export default Register;
